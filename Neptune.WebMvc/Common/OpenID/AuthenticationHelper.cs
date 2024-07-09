using System.Net.Mail;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Common.OpenID;

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

    public static async Task ProcessLoginFromKeystone(TokenValidatedContext tokenValidatedContext, NeptuneDbContext dbContext, WebConfiguration configuration, ILogger _logger, SitkaSmtpClientService sitkaSmtpClientService)
    {
        var sendNewUserNotification = false;
        var sendNewOrganizationNotification = false;
        var claimsIdentity = (ClaimsIdentity)tokenValidatedContext.Principal.Identity;
        var keystoneGuid = new Guid(claimsIdentity.GetClaimValue("sub"));
        _logger.LogInformation($"ocstormwatertools.org: In {nameof(ProcessLoginFromKeystone)} - Processing Keystone login for user with Keystone guid {keystoneGuid}".ToString());
        var person = People.GetByGuid(dbContext, keystoneGuid);
        var firstName = claimsIdentity.GetClaimValue("given_name");
        var lastName = claimsIdentity.GetClaimValue("family_name");
        var email = claimsIdentity.GetClaimValue("email");
        var loginName = claimsIdentity.GetClaimValue("login_name");
        if (person == null)
        {
            _logger.LogInformation($"ocstormwatertools.org: In {nameof(ProcessLoginFromKeystone)} - Creating a new user for {firstName} {lastName} from Keystone login".ToString());
            // new user - provision with limited role
            var unknownOrganization = Organizations.GetUnknownOrganization(dbContext);
            person = new Person()
            {
                PersonGuid = keystoneGuid,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                RoleID = Role.Unassigned.RoleID,
                CreateDate = DateTime.UtcNow,
                IsActive = true,
                OrganizationID = unknownOrganization.OrganizationID,
                LoginName = loginName
            };
            await dbContext.People.AddAsync(person);
            sendNewUserNotification = true;
        }
        else
        {
            _logger.LogInformation($"ocstormwatertools.org: In {nameof(ProcessLoginFromKeystone)} - Signing in user {firstName} {lastName} from Keystone login".ToString());
            if (person.FirstName != firstName || person.LastName != lastName || person.Email != email || person.LoginName != loginName)
            {
                _logger.LogInformation($"ocstormwatertools.org: In {nameof(ProcessLoginFromKeystone)} - Creating a new user for {firstName} {lastName} from Keystone login".ToString());
                person.FirstName = firstName;
                person.LastName = lastName;
                person.Email = email;
                // person.Phone = primaryPhone?.ToPhoneNumberString();
                person.LoginName = loginName;
                person.UpdateDate = DateTime.UtcNow;
            }
        }

        // handle the organization
        if (claimsIdentity.TryGetClaimValue("organization_identifier", out var keystoneOrganizationGuidString))
        {
            if (!string.IsNullOrWhiteSpace(keystoneOrganizationGuidString))
            {
                var keystoneOrganizationGuid = new Guid(keystoneOrganizationGuidString);
                var keystoneOrganizationName = claimsIdentity.GetClaimValue("organization_name");
                var keystoneOrganizationShortName = claimsIdentity.GetClaimValue("organization_shortname");
                // first look by guid, then by name; if not available, create it on the fly since it is a person org
                var organization = Organizations.GetByGuid(dbContext, keystoneOrganizationGuid) ??
                                   Organizations.GetByName(dbContext, keystoneOrganizationName);

                if (organization == null)
                {
                    _logger.LogInformation(
                        $"ocstormwatertools.org: In {nameof(ProcessLoginFromKeystone)} - Creating a new Organization {keystoneOrganizationName} based on Keystone login by user {firstName} {lastName}"
                            .ToString());
                    var defaultOrganizationType = OrganizationTypes.GetDefaultOrganizationType(dbContext);
                    organization = new Organization()
                    {
                        OrganizationName = keystoneOrganizationName,
                        IsActive = true,
                        OrganizationTypeID = defaultOrganizationType.OrganizationTypeID,
                        OrganizationShortName = keystoneOrganizationShortName,
                        OrganizationGuid = keystoneOrganizationGuid
                    };
                    await dbContext.Organizations.AddAsync(organization);
                    sendNewOrganizationNotification = true;
                }

                organization.OrganizationName = keystoneOrganizationName;

                if (!organization.OrganizationGuid.HasValue)
                {
                    _logger.LogInformation(
                        $"ocstormwatertools.org: In {nameof(ProcessLoginFromKeystone)} - Setting the KeystoneGuid field for existing Organization {keystoneOrganizationName} based on Keystone login by user {firstName} {lastName}"
                            .ToString());
                    organization.OrganizationGuid = keystoneOrganizationGuid;
                }

                person.Organization = organization;
                person.OrganizationID = organization.OrganizationID;
            }
        }
        else
        {
            var unknownOrganization = Organizations.GetUnknownOrganization(dbContext);
            person.Organization = unknownOrganization;
        }

        person.LastActivityDate = DateTime.UtcNow;
        await dbContext.SaveChangesAsync();

        if (sendNewUserNotification)
        {
            await SendNewUserCreatedMessage(dbContext, configuration, person, loginName, sitkaSmtpClientService);
        }

        if (sendNewOrganizationNotification)
        {
            await SendNewOrganizationCreatedMessage(dbContext, configuration, person, loginName, sitkaSmtpClientService);
        }
    }


    // Match user's email to an organizations email domain, return unknown organization if no match found

    public static async Task SendNewUserCreatedMessage(NeptuneDbContext dbContext, WebConfiguration configuration, Person person, string loginName, SitkaSmtpClientService sitkaSmtpClientService)
    {
        var subject = $"User added: {person.GetFullNameFirstLastAndOrg()}";
        var message = $@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>User added:</strong> {person.GetFullNameFirstLast()}<br />
    <strong>Added on:</strong> {DateTime.UtcNow.ToStringDateTime()}<br />
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

        await SendMessageImpl(dbContext, configuration, person, subject, message, sitkaSmtpClientService);
    }

    private static async Task SendNewOrganizationCreatedMessage(NeptuneDbContext dbContext, WebConfiguration configuration, Person person, string loginName, SitkaSmtpClientService sitkaSmtpClientService)
    {
        var organization = person.Organization;
        var fieldDefinitionOrganizationLabel = "Organization"; //FieldDefinitionEnum.Organization.ToType().GetFieldDefinitionLabel(); TODO: Need to fix FieldDefinition generated files - hybrid-enum-list entities
        var subject = $"{fieldDefinitionOrganizationLabel} added: {person.Organization.GetDisplayName()}";

        var message = $@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>{fieldDefinitionOrganizationLabel} created:</strong> {organization.GetDisplayName() //organization.GetDisplayNameAsUrl() - TODO:
    }<br />
    <strong>Created on:</strong> {DateTime.UtcNow.ToStringDateTime()}<br />
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

        await SendMessageImpl(dbContext, configuration, person, subject, message, sitkaSmtpClientService);
    }

    private static async Task SendMessageImpl(NeptuneDbContext dbContext, SendGridConfiguration configuration, Person person, string subject, string message, SitkaSmtpClientService sitkaSmtpClientService)
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
        var supportPersonEmails = People.GetEmailAddressesForAdminsThatReceiveSupportEmails(dbContext);
        foreach (var supportPersonEmail in supportPersonEmails)
        {
            mailMessage.To.Add(supportPersonEmail);
        }
        await sitkaSmtpClientService.Send(mailMessage);
    }
}