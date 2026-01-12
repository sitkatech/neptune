using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Neptune.Common.Email;
using Neptune.EFModels.Entities;
using Serilog.Core;
using System.Net.Mail;
using System.Security.Claims;
using System.Web;
using Microsoft.EntityFrameworkCore;

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
        var loginUrlToStrip = "/Account/LogOn?returnUrl=";
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

    public static void ProcessLoginFromAuth0(TokenValidatedContext tokenValidatedContext, NeptuneDbContext dbContext, WebConfiguration configuration, Logger _logger, SitkaSmtpClientService sitkaSmtpClientService)
    {
        var sendNewUserNotification = false;
        var claims = tokenValidatedContext.SecurityToken.Claims;
        var auth0ID = tokenValidatedContext.SecurityToken.Subject;
        _logger.Information($"ocstormwatertools.org: In {nameof(ProcessLoginFromAuth0)} - Processing Auth0 login for user with Auth0 guid {auth0ID}".ToString());
        var person = dbContext.People.FirstOrDefault(x => x.Auth0ID == auth0ID);
        var principal = tokenValidatedContext.Principal;

        // Retrieve the given_name and family_name claims
        var firstName = principal.FindFirst(ClaimTypes.GivenName)?.Value;
        var lastName = principal.FindFirst(ClaimTypes.Surname)?.Value;
        var email = principal.FindFirst(ClaimTypes.Email)?.Value;
        if (person == null)
        {
            _logger.Information($"ocstormwatertools.org: In {nameof(ProcessLoginFromAuth0)} - Creating a new user for {firstName} {lastName} from Keystone login".ToString());
            // new user - provision with limited role
            person = new Person()
            {
                Auth0ID = auth0ID,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                RoleID = Role.Unassigned.RoleID,
                CreateDate = DateTime.UtcNow,
                IsActive = true,
                OrganizationID = Organizations.OrganizationIDUnassigned,
                WebServiceAccessToken = Guid.NewGuid()
            };
            dbContext.People.Add(person);
            sendNewUserNotification = true;
        }
        else
        {
            _logger.Information($"ocstormwatertools.org: In {nameof(ProcessLoginFromAuth0)} - Signing in user {firstName} {lastName} from Keystone login".ToString());
            if (person.FirstName != firstName || person.LastName != lastName || person.Email != email)
            {
                _logger.Information($"ocstormwatertools.org: In {nameof(ProcessLoginFromAuth0)} - Creating a new user for {firstName} {lastName} from Keystone login".ToString());
                person.FirstName = firstName;
                person.LastName = lastName;
                person.Email = email;
                // person.Phone = primaryPhone?.ToPhoneNumberString();
                person.UpdateDate = DateTime.UtcNow;
            }
        }

        person.LastActivityDate = DateTime.UtcNow;
        dbContext.SaveChanges();

        if (sendNewUserNotification)
        {
            SendNewUserCreatedMessage(dbContext, configuration, person, email, sitkaSmtpClientService);
        }
    }


    // Match user's email to an organizations email domain, return unknown organization if no match found

    public static async Task SendNewUserCreatedMessage(NeptuneDbContext dbContext, WebConfiguration configuration, Person person, string loginName, SitkaSmtpClientService sitkaSmtpClientService)
    {
        var subject = $"User added: {person.GetFullNameFirstLast()}";
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