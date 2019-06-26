﻿using System;
using System.Net.Mail;
using System.Threading.Tasks;
using LtInfo.Common;
using System.Security.Claims;
using System.Web;
using Keystone.Common.OpenID;
using Microsoft.Owin;
using Owin;
using LtInfo.Common.Email;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Neptune.Web;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Security.Principal;
using Neptune.Web.ScheduledJobs;

[assembly: OwinStartup(typeof(Startup))]
namespace Neptune.Web
{
    public class Startup
    {
        private const string CookieDomain = ".ocstormwatertools.org";

        /// <summary>
        /// Function required by <see cref="OwinStartupAttribute"/>
        /// </summary>
        public void Configuration(IAppBuilder app)
        {
            SitkaHttpApplication.Logger.Info("Owin Startup");

            System.IdentityModel.Tokens.JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                CookieDomain = CookieDomain,
                CookieManager = new Microsoft.Owin.Host.SystemWeb.SystemWebChunkingCookieManager(),
                CookieName = $"{NeptuneWebConfiguration.KeystoneOpenIDClientId}_{NeptuneWebConfiguration.NeptuneEnvironment.NeptuneEnvironmentType}"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = NeptuneWebConfiguration.KeystoneOpenIDClientId,
                Authority = NeptuneWebConfiguration.KeystoneOpenIDUrl,
                RedirectUri = SitkaRoute<AccountController>.BuildAbsoluteUrlHttpsFromExpression(c => c.LogOn(), NeptuneWebConfiguration.CanonicalHostNameRoot), // this has to match the keystone client redirect uri
                PostLogoutRedirectUri = $"https://{NeptuneWebConfiguration.CanonicalHostNameRoot}/", // OpenID is super picky about this; url must match what Keystone has EXACTLY (Trailing slash and all)
                ResponseType = "id_token token",
                Scope = "openid all_claims keystone",
                UseTokenLifetime = false,
                SignInAsAuthenticationType = "Cookies",
                ClientSecret = NeptuneWebConfiguration.KeystoneOpenIDClientSecret,
                CallbackPath = new PathString("/Account/LogOn"),

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
                        //n.ProtocolMessage.RedirectUri = GetHomePage(); // dynamic home page for multiple subdomains
                        //n.ProtocolMessage.PostLogoutRedirectUri = GetOuterPage(); // dynamic landing page for multiple subdomains
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }
                        }
                        else if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.AuthenticationRequest)
                        {
                            HttpContextBase context = (HttpContextBase)n.OwinContext.Environment["System.Web.HttpContextBase"];

                            var postLogonDestination = NeptuneHelpers.PostLogonDestination(context.Request);

                            if (postLogonDestination != null && NeptuneWebConfiguration.CanonicalHostNames.Contains(postLogonDestination.Host))
                            {
                                n.Response.Cookies.Append("NeptuneReturnURL", postLogonDestination.ToString(), new CookieOptions() { Domain = CookieDomain});
                            }

                        }


                        return Task.FromResult(0);
                    }
                }

            });

            ScheduledBackgroundJobBootstrapper.ConfigureHangfireAndScheduledBackgroundJobs(app);
        }

        public static IKeystoneUser SyncLocalAccountStore(IKeystoneUserClaims keystoneUserClaims, IIdentity userIdentity)
        {
            SitkaHttpApplication.Logger.DebugFormat("In SyncLocalAccountStore - User '{0}', Authenticated = '{1}'", userIdentity.Name, userIdentity.IsAuthenticated);

            var sendNewUserNotification = false;
            var sendNewOrganizationNotification = false;
            var person = HttpRequestStorage.DatabaseEntities.People.GetPersonByPersonGuid(keystoneUserClaims.UserGuid);

            if (person == null)
            {
                // new user - provision with limited role
                SitkaHttpApplication.Logger.DebugFormat(
                    "In SyncLocalAccountStore - creating local profile for User '{0}'", keystoneUserClaims.UserGuid);
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
                    keystoneUserClaims.LoginName) {DroolToolRoleID = DroolToolRole.Unassigned.DroolToolRoleID};
                HttpRequestStorage.DatabaseEntities.People.Add(person);
                sendNewUserNotification = true;
            }
            else
            {
                // existing user - sync values
                SitkaHttpApplication.Logger.DebugFormat(
                    "In SyncLocalAccountStore - syncing local profile for User '{0}'", keystoneUserClaims.UserGuid);
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
            var subject = $"{FieldDefinition.Organization.GetFieldDefinitionLabel()} added: {person.Organization.GetDisplayName()}";

            var message = $@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>{FieldDefinition.Organization.GetFieldDefinitionLabel()} created:</strong> {organization.GetDisplayNameAsUrl()}<br />
    <strong>Created on:</strong> {DateTime.Now}<br />
    <strong>Created because:</strong> New user logged in<br />
    <strong>New user:</strong> {person.GetFullNameFirstLast()} ({person.Email})<br />
    <br />
    <p>
        You may want to <a href=""{
                    SitkaRoute<OrganizationController>.BuildAbsoluteUrlFromExpression(x => x.Detail(organization
                        .OrganizationID))
                }"">add detail for this {FieldDefinition.Organization.GetFieldDefinitionLabel()}</a> such as its abbreviation, {
                    FieldDefinition.OrganizationType.GetFieldDefinitionLabel()
                }, website, logo, etc. This will make its {FieldDefinition.Organization.GetFieldDefinitionLabel()} summary page display better.
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
