using System.Net.Mail;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Common.OpenID;

public static class AuthenticationHelper
{
    private const string AuthenticationApplicationCookieName = "NeptuneCookieIdentity";

    // We don't want to return users to the login page so need to pull out return url parameter from current url
    public static string SanitizeReturnUrlForLogin(string rawReturnUrlString, string homeUrl)
    {
        // Decode url encoded string
        string decodedUrlString = HttpUtility.UrlDecode(rawReturnUrlString);
        if (string.IsNullOrWhiteSpace(rawReturnUrlString) || rawReturnUrlString == "/")
        {
            return homeUrl;
        }
        // Strip the main url to get only the value of the returnUrl parameter
        var loginUrlToStrip = "/Account/Login?returnUrl=";
        var parameterOnly = decodedUrlString.Replace(loginUrlToStrip, "");
        // Now decode content of returnUrl argument
        return HttpUtility.UrlDecode(parameterOnly);
    }

    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Keystone", builder => builder.RequireAuthenticatedUser().AddAuthenticationSchemes("Keystone").Build());
        });

        return services;
    }

    public static async Task KeystoneAndAADAuthenticationMiddleware(HttpContext context, Func<Task> next)
    {
        var principal = new ClaimsPrincipal();

        // var result1 = await context.AuthenticateAsync("Keystone");
        var result1 = await context.AuthenticateAsync("Keystone");
        if (result1?.Principal != null)
        {
            // 
            principal.AddIdentities(result1.Principal.Identities);
        }

        var result2 = await context.AuthenticateAsync("AAD");
        if (result2?.Principal != null)
        {
            principal.AddIdentities(result2.Principal.Identities);
        }

        context.User = principal;

        await next();
    }

    public static void ProcessLoginFromKeystone(TokenValidatedContext tokenValidatedContext, NeptuneDbContext dbContext, WebConfiguration configuration)
    {
        var applicationDomain = configuration.ApplicationDomain;
        var sendNewUserNotification = false;
        var sendNewOrganizationNotification = false;
        var claimsIdentity = (ClaimsIdentity)tokenValidatedContext.Principal.Identity;
        var keystoneGuid = new Guid(claimsIdentity.GetClaimValue("sub"));
//        _logger.LogInformation($"{applicationDomain}: In {nameof(ProcessLoginFromKeystone)} - Processing Keystone login for user with Keystone guid {keystoneGuid}".ToString());
        var person = People.GetByGuid(dbContext, keystoneGuid);
        // var firstName = claimsIdentity.GetClaimValue("FirstName");
        //
        // var lastName = claimsIdentity.GetClaimValue("LastName");
        // var email = claimsIdentity.GetClaimValue("Email");
        // var loginName = claimsIdentity.GetClaimValue("LoginName");            
        var firstName = claimsIdentity.GetClaimValue("given_name");
        var lastName = claimsIdentity.GetClaimValue("family_name");
        var email = claimsIdentity.GetClaimValue("email");
        var loginName = claimsIdentity.GetClaimValue("login_name");
        if (person == null)
        {
//            _logger.LogInformation($"{applicationDomain}: In {nameof(ProcessLoginFromKeystone)} - Creating a new user for {firstName} {lastName} from Keystone login".ToString());
            // new user - provision with limited role
            var unknownOrganization = Organizations.GetUnknownOrganization(dbContext);
            person = new Person()
            {
                PersonGuid = keystoneGuid,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                RoleID = Role.Unassigned.RoleID,
                CreateDate = DateTime.Now,
                IsActive = true,
                OrganizationID = unknownOrganization.OrganizationID,
                LoginName = loginName
            };
            dbContext.People.Add(person);
            sendNewUserNotification = true;
        }
        else
        {
//            _logger.LogInformation($"{applicationDomain}: In {nameof(ProcessLoginFromKeystone)} - Signing in user {firstName} {lastName} from Keystone login".ToString());
            //var primaryPhone = claimsIdentity.GetClaimValue("PrimaryPhone");
            // if (person.FirstName != firstName || person.LastName != lastName || person.Email != email || person.Phone != primaryPhone || person.LoginName != loginName)
            if (person.FirstName != firstName || person.LastName != lastName || person.Email != email || person.LoginName != loginName)
            {
 //               _logger.LogInformation($"{applicationDomain}: In {nameof(ProcessLoginFromKeystone)} - Creating a new user for {firstName} {lastName} from Keystone login".ToString());
                person.FirstName = firstName;
                person.LastName = lastName;
                person.Email = email;
                // person.Phone = primaryPhone?.ToPhoneNumberString();
                person.LoginName = loginName;
                person.UpdateDate = DateTime.Now;
            }
        }

        // handle the organization
        Organization organization = null;
        // var keystoneOrganizationGuidString  = claimsIdentity.GetClaimValue("OrganizationGuid");
        var keystoneOrganizationGuidString = claimsIdentity.GetClaimValue("organization_identifier");
        if (keystoneOrganizationGuidString != null)
        {
            var keystoneOrganizationGuid = new Guid(keystoneOrganizationGuidString);
            var keystoneOrganizationName = claimsIdentity.GetClaimValue("organization_name");
            var keystoneOrganizationShortName = claimsIdentity.GetClaimValue("organization_shortname");
            // first look by guid, then by name; if not available, create it on the fly since it is a person org
            organization = Organizations.GetByGuid(dbContext, keystoneOrganizationGuid) ??
                 Organizations.GetByName(dbContext, keystoneOrganizationName);

            if (organization == null)
            {
//                _logger.LogInformation($"{applicationDomain}: In {nameof(ProcessLoginFromKeystone)} - Creating a new Organization {keystoneOrganizationName} based on Keystone login by user {firstName} {lastName}".ToString());
                var defaultOrganizationType = OrganizationTypes.GetDefaultOrganizationType(dbContext);
                organization = new Organization()
                {
                    OrganizationName = keystoneOrganizationName, 
                    IsActive = true, 
                    OrganizationTypeID = defaultOrganizationType.OrganizationTypeID,
                    OrganizationShortName = keystoneOrganizationShortName,
                    OrganizationGuid = keystoneOrganizationGuid // TODO: Get OrganizationUrl from Keystone
                };
                dbContext.Organizations.Add(organization);
                sendNewOrganizationNotification = true;
            }

            organization.OrganizationName = keystoneOrganizationName;

            if (!organization.OrganizationGuid.HasValue)
            {
//                _logger.LogInformation($"{applicationDomain}: In {nameof(ProcessLoginFromKeystone)} - Setting the KeystoneGuid field for existing Organization {keystoneOrganizationName} based on Keystone login by user {firstName} {lastName}".ToString());
                organization.OrganizationGuid = keystoneOrganizationGuid;
            }
            person.Organization = organization;
            person.OrganizationID = organization.OrganizationID;
        }
        else
        {
            var unknownOrganization = Organizations.GetUnknownOrganization(dbContext);
            person.OrganizationID = unknownOrganization.OrganizationID;
        }

        person.LastActivityDate = DateTime.Now;
        // databaseEntities.SaveChanges(person); // TODO: Need to enable this for audit logging
        dbContext.SaveChanges();

        if (sendNewUserNotification)
        {
            SendNewUserCreatedMessage(dbContext, configuration, person, loginName);
        }

        if (sendNewOrganizationNotification)
        {
            SendNewOrganizationCreatedMessage(dbContext, configuration, person, loginName);
        }
    }


    // Match user's email to an organizations email domain, return unknown organization if no match found

    public static void SendNewUserCreatedMessage(NeptuneDbContext dbContext, WebConfiguration configuration, Person person, string loginName)
    {
        var subject = $"User added: {person.GetFullNameFirstLastAndOrg()}";
        var message = $@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>User added:</strong> {person.GetFullNameFirstLast()}<br />
    <strong>Added on:</strong> {DateTime.Now}<br />
    <strong>Email:</strong> {person.Email}<br />
    <strong>Phone:</strong> {person.Phone.ToPhoneNumberString()}<br />
    <br />
    <p>
        You may want to <a href=""{person.GetFullNameFirstLast() // TODO: Replace with User Detail url
                                                                 //SitkaRoute<UserController>.BuildAbsoluteUrlFromExpression(x => x.Detail(person.PersonID))
        }"">assign this user a role</a> to allow them to use the site. Or you can leave the user with Unassigned roles if they don't need special privileges.
    </p>
    <br />
    <br />
    <div style='font-size: 10px; color: gray'>
    OTHER DETAILS:<br />
    LOGIN: {loginName}<br />
    <br />
    </div>
    <div>You received this email because you are set up as a point of contact for support - if that's not correct, let us know: {configuration.SitkaSupportEmail}.</div>
</div>
";

        SendMessageImpl(dbContext, configuration, person, subject, message);
    }

    private static void SendNewOrganizationCreatedMessage(NeptuneDbContext dbContext, WebConfiguration configuration, Person person, string loginName)
    {
        var organization = person.Organization;
        var fieldDefinitionOrganizationLabel = "Organization"; //FieldDefinitionEnum.Organization.ToType().GetFieldDefinitionLabel(); TODO: Need to fix FieldDefinition generated files - hybrid-enum-list entities
        var subject = $"{fieldDefinitionOrganizationLabel} added: {person.Organization.GetDisplayName()}";

        var message = $@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>{fieldDefinitionOrganizationLabel} created:</strong> {organization.GetDisplayName() //organization.GetDisplayNameAsUrl() - TODO:
    }<br />
    <strong>Created on:</strong> {DateTime.Now}<br />
    <strong>Created because:</strong> New user logged in<br />
    <strong>New user:</strong> {person.GetFullNameFirstLast()} ({person.Email})<br />
    <br />
    <p>
        You may want to <a href=""{person.GetFullNameFirstLast() //SitkaRoute<OrganizationController>.BuildAbsoluteUrlFromExpression(x => x.Detail(organization.OrganizationID)) TODO: Use Organization Detail url
        }"">add detail for this {fieldDefinitionOrganizationLabel}</a> such as its abbreviation, {"Organization" //FieldDefinitionEnum.OrganizationType.ToType().GetFieldDefinitionLabel() - TODO:
        }, website, logo, etc. This will make its {fieldDefinitionOrganizationLabel} summary page display better.
    </p>
    <br />
    <br />
    <div style='font-size: 10px; color: gray'>
    OTHER DETAILS:<br />
    LOGIN: {loginName}<br />
    <br />
    </div>
    <div>You received this email because you are set up as a point of contact for support - if that's not correct, let us know: {configuration.SitkaSupportEmail}</div>.
</div>
";

        SendMessageImpl(dbContext, configuration, person, subject, message);
    }

    private static void SendMessageImpl(NeptuneDbContext dbContext, WebConfiguration configuration, Person person, string subject, string message)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(configuration.DoNotReplyEmail),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        // Reply-To Header
        mailMessage.ReplyToList.Add(person.Email);

        // TO field
        //var supportPersons = NeptuneArea.OCStormwaterTools.GetSupportRequestRecipients(dbContext);
        //foreach (var supportPerson in supportPersons)
        //{
        //    mailMessage.To.Add(supportPerson.Email);
        //}
        // TODO: Actually send the email
        // SitkaSmtpClient.Send(mailMessage);
    }

    // public static void AuthenticateUserFromSamlResponse(ADFSSamlResponse adfsSamlResponse, IAuthenticationManager authentication, string applicationDomain)
    // {
    //     var firstName = adfsSamlResponse.GetFirstName();
    //     var lastName = adfsSamlResponse.GetLastName();
    //     var email = adfsSamlResponse.GetEmail();
    //     SitkaHttpApplication.Logger.Info($"{applicationDomain}: Successfully decrypted SAML response for {firstName} {lastName} - ready to process login");
    //
    //     var person = ProcessLogin(firstName, lastName, email, applicationDomain);
    //     IdentitySignIn(authentication, person);
    //
    // }



    // Authentication helpers?
    // public static Person ProcessLogin(string firstName, string lastName, string email, string applicationDomain)
    // {
    //     var userDetailsStringForLogging = $"FirstName: {firstName} LastName: {lastName} Email: {email}";
    //     SitkaHttpApplication.Logger.Info($"{applicationDomain}: In {nameof(ProcessLogin)} - ADFS Authentication [{userDetailsStringForLogging}]");
    //     Check.RequireNotNullNotEmptyNotWhitespace(email, $"Cannot complete sign in with a blank or missing email address. Be sure that there is an email address for user in Washington State Active Directory.");
    //
    //     var sendNewUserCreatedMessage = false;
    //
    //     var person = HttpRequestStorage.DatabaseEntities.People.GetPersonByEmail(email, false);
    //     SitkaHttpApplication.Logger.Info($"{applicationDomain}: In {nameof(ProcessLogin)} - {(person != null ? "Found" : "Did NOT find")} by email address. [{userDetailsStringForLogging}]");
    //
    //     // If there's no Person already that corresponds to the Person who is logging in, we create Person
    //     if (person == null)
    //     {
    //         // new user - initially provision with limited Role.Unassigned
    //         SitkaHttpApplication.Logger.Info($"{applicationDomain}: In {nameof(ProcessLogin)} - Creating new Person. [{userDetailsStringForLogging}]");
    //
    //         // Do lookup of Organization from email domain
    //         var organizationID = GetOrganizationFromEmailDomain(email).OrganizationID;
    //
    //         // User is created without a KeystoneGuid since there is no known relationship to Keystone
    //         person = new Person(firstName,
    //             lastName,
    //             email,
    //             PsInfoRole.Unassigned.PsInfoRoleID,
    //             DateTime.Now,
    //             true,
    //             organizationID,
    //             email,
    //             VitalSignRole.Unassigned.VitalSignRoleID,
    //             NepAtlasRole.Unassigned.NepAtlasRoleID);
    //         HttpRequestStorage.DatabaseEntities.People.Add(person);
    //         HttpRequestStorage.DatabaseEntities.People.Add(person);
    //         sendNewUserCreatedMessage = true;
    //     }
    //     else
    //     {
    //         // existing user - sync values
    //         SitkaHttpApplication.Logger.InfoFormat($"{applicationDomain}: In {nameof(ProcessLogin)} - user record already exists -- syncing local profile. [{userDetailsStringForLogging}]");
    //         person.FirstName = firstName;
    //         person.LastName = lastName;
    //         //Update Organization if it's changed of it an Org is mapped that wasn't previously in system
    //         var organizationID = GetOrganizationFromEmailDomain(email).OrganizationID;
    //         person.OrganizationID = organizationID;
    //
    //         Check.RequireThrowUserDisplayable(person.IsActive, $"User account for {email} is not active and cannot login at this time. Contact support for more information.");
    //     }
    //     // TODO: should this be update date or last activity date?
    //     person.UpdateDate = DateTime.Now;
    //
    //     HttpRequestStorage.DatabaseEntities.SaveChanges(person);
    //
    //     if (sendNewUserCreatedMessage)
    //     {
    //         SitkaHttpApplication.Logger.InfoFormat($"{applicationDomain}: In {nameof(ProcessLogin)} - Sending new user created message. [{userDetailsStringForLogging}]");
    //         PsInfoOAuthStartup.SendNewUserCreatedMessage(person, email);
    //     }
    //
    //     return person;
    // }
    //
    // // Match user's email to an organizations email domain, return unknown organization if no match found
    // private static Organization GetOrganizationFromEmailDomain(string email)
    // {
    //     var emailDomain = email.Split('@')[1];
    //     return HttpRequestStorage.DatabaseEntities.Organizations.SingleOrDefault(x =>
    //             x.EmailDomains.Any(y => y.DomainName == emailDomain)) ??
    //         HttpRequestStorage.DatabaseEntities.Organizations.GetUnknownOrganization();
    // }
    //
    // public static void IdentitySignIn(IAuthenticationManager authenticationManager, Person person)
    // {
    //     HttpRequestStorage.Person = person;
    //     authenticationManager.SignIn(new AuthenticationProperties
    //     {
    //         AllowRefresh = true,
    //         IsPersistent = false,
    //         ExpiresUtc = DateTime.UtcNow.AddDays(7)
    //     }, ClaimsIdentityFromPerson(person));
    //     HttpContext.Current.Session["PersonID"] = person.PersonID;
    // }
    //
    // private static ClaimsIdentity ClaimsIdentityFromPerson(Person person)
    // {
    //     var claims = new List<Claim>
    //     {
    //         // Using ClaimTypes.Name to get data into field Principal.Identity.Name for parse out later
    //         new Claim(ClaimTypes.Name, person.PersonID.ToString()),
    //     };
    //     var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
    //     return identity;
    // }
    //
    // public static Person PersonFromClaimsIdentity(IAuthenticationManager authenticationManager)
    // {
    //     var claimsPrincipal = authenticationManager.User;
    //     if (claimsPrincipal?.Identity == null || !claimsPrincipal.Identity.IsAuthenticated || (claimsPrincipal.Identity.AuthenticationType != DefaultAuthenticationTypes.ApplicationCookie && claimsPrincipal.Identity.AuthenticationType != "Cookies"))
    //     {
    //         // User seems unauthenticated via Keystone approach
    //         // Check Session stored identity via ADFS approach
    //         if (HttpContext.Current.Session["PersonID"] != null)
    //         {
    //             // Get person from ID to return
    //             var personID = (int)HttpContext.Current.Session["PersonID"];
    //             var person = HttpRequestStorage.DatabaseEntities.People.GetPerson(personID);
    //             if (person != null)
    //             {
    //                 // Authenticate manually by assigning the retrieved identity to the current user 
    //                 var identity = new GenericIdentity(personID.ToString());
    //                 var principal = new GenericPrincipal(identity, new string[] { });
    //                 HttpContext.Current.User = principal;
    //                 return person;
    //             }
    //         }
    //         return Person.GetAnonymousSitkaUser();
    //     }
    //
    //
    //     try
    //     {
    //         // This parsing out of depends on the write of data into ClaimTypes.Name
    //         var person = FindPersonFromClaims(claimsPrincipal);
    //         Check.Require(person.IsActive, $"Account for {person.Email} is not active.");
    //         return person;
    //     }
    //     catch (Exception ex)
    //     {
    //         IdentitySignOut(authenticationManager);
    //         throw new SitkaDisplayErrorException("Something went wrong with your session or credentials. Please try logging in again. If this does not resolve the issue, please contact support.", ex);
    //     }
    // }
    //
    // private static Person FindPersonFromClaims(ClaimsPrincipal claimsPrincipal)
    // {
    //     Person person;
    //     // ADFS path
    //     if (claimsPrincipal.Identity.AuthenticationType == DefaultAuthenticationTypes.ApplicationCookie)
    //     {
    //         var personID = int.Parse(claimsPrincipal.Identity.Name);
    //         person = HttpRequestStorage.DatabaseEntities.People.GetPerson(personID);
    //     }
    //     // Keystone path
    //     else
    //     {
    //         // In Keystone claims, the Person GUID is stored in a property called 'sub' 
    //         var personGuid = Guid.Parse(claimsPrincipal.FindAll(x => x.Type == "sub").First().Value);
    //         person = HttpRequestStorage.DatabaseEntities.People.GetPersonByKeystoneGuid(personGuid);
    //     }
    //     return person;
    // }
    //
    // public static void IdentitySignOut(IAuthenticationManager authenticationManager)
    // {
    //     authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
    //     HttpContext.Current.Request.Cookies.Remove(AuthenticationApplicationCookieName);
    //     HttpRequestStorage.Person = Person.GetAnonymousSitkaUser();
    //     HttpContext.Current.Session["PersonID"] = null;
    // }

}