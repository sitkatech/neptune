using System;
using System.Net.Mail;
using System.Threading.Tasks;
using LtInfo.Common;
using System.Security.Claims;
using Keystone.Common.OpenID;
using Microsoft.Owin;
using Owin;
using LtInfo.Common.Email;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Neptune.Web;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using IdentityModel;
using log4net;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Notifications;
using Neptune.Web.ScheduledJobs;
using SameSiteMode = Microsoft.Owin.SameSiteMode;

[assembly: OwinStartup(typeof(Startup))]
namespace Neptune.Web
{
    public class Startup
    {
        private const string CookieDomain = ".ocstormwatertools.org";
        public static readonly ILog Logger = LogManager.GetLogger(typeof(Startup));

        /// <summary>
        /// Function required by <see cref="OwinStartupAttribute"/>
        /// </summary>
        public void Configuration(IAppBuilder app)
        {
            Logger.Info("Owin Startup");

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                CookieDomain = CookieDomain,
                CookieManager = new Microsoft.Owin.Host.SystemWeb.SystemWebChunkingCookieManager(),
                CookieName = $"{NeptuneWebConfiguration.KeystoneOpenIDClientId}_{NeptuneWebConfiguration.NeptuneEnvironment.NeptuneEnvironmentType}"
            });

            //Needed (at least in development) to allow Neptune to talk to upgraded keystone
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            //Most of the new openID for mvc pieces came from here https://www.scottbrady91.com/ASPNET/Refreshing-your-Legacy-ASPNET-IdentityServer-Client-Applications
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = NeptuneWebConfiguration.KeystoneOpenIDClientId,
                Authority = NeptuneWebConfiguration.KeystoneOpenIDUrl,
                RedirectUri = SitkaRoute<AccountController>.BuildAbsoluteUrlHttpsFromExpression(c => c.LogOn(), NeptuneWebConfiguration.CanonicalHostNameRoot), // this has to match the keystone client redirect uri
                PostLogoutRedirectUri = $"https://{NeptuneWebConfiguration.CanonicalHostNameRoot}/", // OpenID is super picky about this; url must match what Keystone has EXACTLY (Trailing slash and all)
                ResponseType = "code",
                Scope = "openid profile offline_access keystone",
                UseTokenLifetime = false,
                SignInAsAuthenticationType = "Cookies",
                //ClientSecret = NeptuneWebConfiguration.KeystoneOpenIDClientSecret,
                CallbackPath = new PathString("/Account/LogOn"),

                RequireHttpsMetadata = false,
                RedeemCode = true,
                SaveTokens = true,
                ResponseMode = "query",

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthenticationFailed = (context) =>
                    {
                        if ((context.Exception.Message.StartsWith("OICE_20004") || context.Exception.Message.Contains("IDX10311")))
                        {
                            context.SkipToNextMiddleware();
                            return Task.FromResult(0);
                        }

                        return Task.FromResult(0);
                    },
                    SecurityTokenValidated = n =>
                    {
                        var claimsIdentity = n.AuthenticationTicket.Identity;
                        claimsIdentity.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                        if (n.ProtocolMessage.Code != null)
                            claimsIdentity.AddClaim(new Claim("code", n.ProtocolMessage.Code));

                        if (n.ProtocolMessage.AccessToken != null)
                            claimsIdentity.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

                        //map name claim to default name type
                        claimsIdentity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", claimsIdentity.FindFirst(KeystoneOpenIDClaimTypes.Name).Value.ToString()));

                        if (claimsIdentity.IsAuthenticated) // we have a token and we can determine the person.
                        {
                            KeystoneOpenIDUtilities.OpenIDClaimHandler(SyncLocalAccountStore, claimsIdentity);
                        }

                        return Task.FromResult(0);
                    },
                    RedirectToIdentityProvider = n =>
                    {
                        if (n.ProtocolMessage.RequestType == Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectRequestType.Authentication)
                        {
                            // generate code verifier and code challenge
                            var codeVerifier = CryptoRandom.CreateUniqueId(32);

                            string codeChallenge;
                            using (var sha256 = SHA256.Create())
                            {
                                var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
                                codeChallenge = Base64Url.Encode(challengeBytes);
                            }

                            // set code_challenge parameter on authorization request
                            n.ProtocolMessage.SetParameter("code_challenge", codeChallenge);
                            n.ProtocolMessage.SetParameter("code_challenge_method", "S256");

                            // remember code verifier in cookie (adapted from OWIN nonce cookie)
                            // see: https://github.com/scottbrady91/Blog-Example-Classes/blob/master/AspNetFrameworkPkce/ScottBrady91.BlogExampleCode.AspNetPkce/Startup.cs#L85
                            RememberCodeVerifier(n, codeVerifier);
                        }

                        //https://identityserver.github.io/Documentation/docsv2/overview/mvcGettingStarted.html#adding-logout
                        if (n.ProtocolMessage.RequestType == Microsoft.IdentityModel.Protocols.OpenIdConnect
                            .OpenIdConnectRequestType.Logout)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }
                        }

                        return Task.CompletedTask;
                    },
                    AuthorizationCodeReceived = n =>
                    {
                        // get code verifier from cookie
                        // see: https://github.com/scottbrady91/Blog-Example-Classes/blob/master/AspNetFrameworkPkce/ScottBrady91.BlogExampleCode.AspNetPkce/Startup.cs#L102
                        var codeVerifier = RetrieveCodeVerifier(n);

                        // attach code_verifier on token request
                        n.TokenEndpointRequest.SetParameter("code_verifier", codeVerifier);

                        return Task.CompletedTask;
                    }
                }

            });

            ScheduledBackgroundJobBootstrapper.ConfigureHangfireAndScheduledBackgroundJobs(app);
        }

        private void RememberCodeVerifier(RedirectToIdentityProviderNotification<Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> n, string codeVerifier)
        {
            var properties = new AuthenticationProperties();
            properties.Dictionary.Add("cv", codeVerifier);
            n.Options.CookieManager.AppendResponseCookie(
                n.OwinContext,
                GetCodeVerifierKey(n.ProtocolMessage.State),
                Convert.ToBase64String(Encoding.UTF8.GetBytes(n.Options.StateDataFormat.Protect(properties))),
                new CookieOptions
                {
                    SameSite = SameSiteMode.None,
                    HttpOnly = true,
                    Secure = n.Request.IsSecure,
                    Expires = DateTime.UtcNow + n.Options.ProtocolValidator.NonceLifetime
                });
        }

        private string GetCodeVerifierKey(string state)
        {
            using (var hash = SHA256.Create())
            {
                return OpenIdConnectAuthenticationDefaults.CookiePrefix + "cv." + Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(state)));
            }
        }

        private string RetrieveCodeVerifier(AuthorizationCodeReceivedNotification n)
        {
            string key = GetCodeVerifierKey(n.ProtocolMessage.State);

            string codeVerifierCookie = n.Options.CookieManager.GetRequestCookie(n.OwinContext, key);
            if (codeVerifierCookie != null)
            {
                var cookieOptions = new CookieOptions
                {
                    SameSite = SameSiteMode.None,
                    HttpOnly = true,
                    Secure = n.Request.IsSecure
                };

                n.Options.CookieManager.DeleteCookie(n.OwinContext, key, cookieOptions);
            }

            var cookieProperties = n.Options.StateDataFormat.Unprotect(Encoding.UTF8.GetString(Convert.FromBase64String(codeVerifierCookie)));
            cookieProperties.Dictionary.TryGetValue("cv", out var codeVerifier);

            return codeVerifier;
        }

        public static IKeystoneUser SyncLocalAccountStore(IKeystoneUserClaims keystoneUserClaims, IIdentity userIdentity)
        {
            Logger.DebugFormat("In SyncLocalAccountStore - User '{0}', Authenticated = '{1}'", userIdentity.Name, userIdentity.IsAuthenticated);

            var sendNewUserNotification = false;
            var sendNewOrganizationNotification = false;
            var person = HttpRequestStorage.DatabaseEntities.People.GetPersonByPersonGuid(keystoneUserClaims.UserGuid);

            if (person == null)
            {
                // new user - provision with limited role
                Logger.DebugFormat("In SyncLocalAccountStore - creating local profile for User '{0}'", keystoneUserClaims.UserGuid);
                var unknownOrganization = HttpRequestStorage.DatabaseEntities.Organizations.GetUnknownOrganization();
                person = new Person(keystoneUserClaims.UserGuid,
                    keystoneUserClaims.FirstName,
                    keystoneUserClaims.LastName,
                    keystoneUserClaims.Email,
                    Role.Unassigned.RoleID,
                    DateTime.Now,
                    true,
                    unknownOrganization.OrganizationID,
                    false,
                    keystoneUserClaims.LoginName, false, Guid.NewGuid(),
                    false);
                HttpRequestStorage.DatabaseEntities.People.Add(person);
                sendNewUserNotification = true;
            }
            else
            {
                // existing user - sync values
                Logger.DebugFormat("In SyncLocalAccountStore - syncing local profile for User '{0}'", keystoneUserClaims.UserGuid);
            }

            person.FirstName = keystoneUserClaims.FirstName;
            person.LastName = keystoneUserClaims.LastName;
            person.Email = keystoneUserClaims.Email;
            person.Phone = keystoneUserClaims.PrimaryPhone?.ToPhoneNumberString();
            person.LoginName = keystoneUserClaims.LoginName;

            // handle the organization
            if (keystoneUserClaims.OrganizationGuid.HasValue)
            {
                // first look by guid, then by name; if not available, create it on the fly since it is a person org
                var organization =
                (HttpRequestStorage.DatabaseEntities.Organizations.GetOrganizationByOrganizationGuid(keystoneUserClaims
                     .OrganizationGuid.Value) ??
                 HttpRequestStorage.DatabaseEntities.Organizations.GetOrganizationByOrganizationName(keystoneUserClaims
                     .OrganizationName));

                if (organization == null)
                {
                    var defaultOrganizationType =
                        HttpRequestStorage.DatabaseEntities.OrganizationTypes.GetDefaultOrganizationType();
                    organization = new Organization(keystoneUserClaims.OrganizationName, true, defaultOrganizationType);
                    HttpRequestStorage.DatabaseEntities.Organizations.Add(organization);
                    sendNewOrganizationNotification = true;
                }

                organization.OrganizationName = keystoneUserClaims.OrganizationName;

                if (!organization.OrganizationGuid.HasValue)
                {
                    organization.OrganizationGuid = keystoneUserClaims.OrganizationGuid;
                }
                person.Organization = organization;
                person.OrganizationID = organization.OrganizationID;
            }
            else
            {
                var unknownOrganization = HttpRequestStorage.DatabaseEntities.Organizations.GetUnknownOrganization();
                person.OrganizationID = unknownOrganization.OrganizationID;
                //Assign user to magic Unkown Organization ID
            }

            person.UpdateDate = DateTime.Now;
            HttpRequestStorage.Person = person;
            HttpRequestStorage.DatabaseEntities.SaveChanges(person);

            if (sendNewUserNotification)
            {
                SendNewUserCreatedMessage(person, keystoneUserClaims.LoginName);
            }

            if (sendNewOrganizationNotification)
            {
                SendNewOrganizationCreatedMessage(person, keystoneUserClaims.LoginName);
            }

            return HttpRequestStorage.Person;

        }


        private static void SendNewUserCreatedMessage(Person person, string loginName)
        {
            var subject = $"User added: {person.GetFullNameFirstLastAndOrg()}";
            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>OC Stormwater Tools User added:</strong> {person.GetFullNameFirstLast()}<br />
    <strong>Added on:</strong> {DateTime.Now}<br />
    <strong>Email:</strong> {person.Email}<br />
    <strong>Phone:</strong> {person.Phone.ToPhoneNumberString()}<br />
    <br />
    <p>
        You may want to <a href=""{
                    SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Detail(person.PersonID))
                }"">assign this user a role</a> and associate them with a jurisdiction to allow them to use the site. Or you can leave the user with Unassigned roles if they don't need special privileges.
    </p>
    <br />
    <br />
    <div style='font-size: 10px; color: gray'>
    OTHER DETAILS:<br />
    LOGIN: {loginName}<br />
    <br />
    </div>
    <div>You received this email because you are set up as a point of contact for support - if that's not correct, let us know: {
                    NeptuneWebConfiguration.SitkaSupportEmail
                }.</div>
</div>
";

            SendMessageImpl(person, subject, message);
        }

        private static void SendNewOrganizationCreatedMessage(Person person, string loginName)
        {
            var organization = person.Organization;
            var subject = $"{FieldDefinitionType.Organization.GetFieldDefinitionLabel()} added: {person.Organization.GetDisplayName()}";

            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>{FieldDefinitionType.Organization.GetFieldDefinitionLabel()} created:</strong> {organization.GetDisplayNameAsUrl()}<br />
    <strong>Created on:</strong> {DateTime.Now}<br />
    <strong>Created because:</strong> New user logged in<br />
    <strong>New user:</strong> {person.GetFullNameFirstLast()} ({person.Email})<br />
    <br />
    <p>
        You may want to <a href=""{
                    SitkaRoute<OrganizationController>.BuildAbsoluteUrlFromExpression(x => x.Detail(organization
                        .OrganizationID))
                }"">add detail for this {FieldDefinitionType.Organization.GetFieldDefinitionLabel()}</a> such as its abbreviation, {
                    FieldDefinitionType.OrganizationType.GetFieldDefinitionLabel()
                }, website, logo, etc. This will make its {FieldDefinitionType.Organization.GetFieldDefinitionLabel()} summary page display better.
    </p>
    <br />
    <br />
    <div style='font-size: 10px; color: gray'>
    OTHER DETAILS:<br />
    LOGIN: {loginName}<br />
    <br />
    </div>
    <div>You received this email because you are set up as a point of contact for support - if that's not correct, let us know: {
                    NeptuneWebConfiguration.SitkaSupportEmail
                }</div>.
</div>
";

            SendMessageImpl(person, subject, message);
        }

        private static void SendMessageImpl(Person person, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(NeptuneWebConfiguration.DoNotReplyEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            // Reply-To Header
            mailMessage.ReplyToList.Add(person.Email);

            // TO field
            var supportPersons = HttpRequestStorage.DatabaseEntities.People.GetPeopleWhoReceiveSupportEmails();
            foreach (var supportPerson in supportPersons)
            {
                mailMessage.To.Add(supportPerson.Email);
            }

            SitkaSmtpClient.Send(mailMessage);
        }
    }
}
