namespace Neptune.Common.Email;

public class SendGridConfiguration
{
    public string SendGridApiKey { get; set; }
    public string SitkaEmailRedirect { get; set; }
    public string MailLogBcc { get; set; }
    public string SitkaSupportEmail { get; set; }
    public string DoNotReplyEmail { get; set; }
}