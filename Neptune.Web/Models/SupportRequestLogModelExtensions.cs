using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using System.Net.Mail;
using Neptune.Common;

namespace Neptune.Web.Models;

public static class SupportRequestLogModelExtensions
{
    public static void SendMessage(this SupportRequestLog supportRequestLog, string ipAddress, string userAgent, string currentUrl, SupportRequestType supportRequestType)
    {
        var subject = $"Support Request for Neptune - {DateTime.Now.ToStringDateTime()}";
        var message = string.Format(@"
<div style='font-size: 12px; font-family: Arial'>
    <strong>{0}</strong><br />
    <br />
    <strong>From:</strong> {1} - {2}<br />
    <strong>Email:</strong> {3}<br />
    <strong>Phone:</strong> {4}<br />
    <br />
    <strong>Subject:</strong> {5}<br />
    <br />
    <strong>Description:</strong><br />
    {6}
    <br />
    <br />
    <br />
    <div style='font-size: 10px; color: gray'>
    OTHER DETAILS:<br />
    LOGIN: {7}<br />
    IP ADDRESS: {8}<br />
    USERAGENT: {9}<br />
    URL FROM: {10}<br />
    <br />
    </div>
    <div>You received this email because you are set up as a point of contact for support - if that's not correct, let us know: {11}</div>.
</div>
",
            subject,
            supportRequestLog.RequestPersonName,
            supportRequestLog.RequestPersonOrganization ?? "(not provided)",
            supportRequestLog.RequestPersonEmail,
            supportRequestLog.RequestPersonPhone ?? "(not provided)",
            supportRequestType.SupportRequestTypeDisplayName,
            supportRequestLog.RequestDescription.HtmlEncodeWithBreaks(),
            supportRequestLog.RequestPerson != null ? $"{supportRequestLog.RequestPerson.GetFullNameFirstLast()} (UserID {supportRequestLog.RequestPerson.PersonID})" : "(anonymous user)",
            ipAddress,
            userAgent,
            currentUrl,
            "support@sitkatech.com");
        // Create Notification
        var mailMessage = new MailMessage { From = new MailAddress("DoNotReplyEmail"), Subject = subject, Body = message, IsBodyHtml = true };

        // Reply-To Header
        mailMessage.ReplyToList.Add(supportRequestLog.RequestPersonEmail);

        // TO field
        //todo: SupportRequestType.SetEmailRecipientsOfSupportRequest(mailMessage);

        //TODO: SitkaSmtpClient.Send(mailMessage);
    }
}