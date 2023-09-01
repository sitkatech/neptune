namespace Hippocamp.API.Services
{
    public class HippocampConfiguration
    {
        public string KEYSTONE_HOST { get; set; }
        public string DB_CONNECTION_STRING { get; set; }
        public string SMTP_HOST { get; set; }
        public int SMTP_PORT { get; set; }
        public string SITKA_EMAIL_REDIRECT { get; set; }
        public string WEB_URL { get; set; }
        public string KEYSTONE_REDIRECT_URL { get; set; }
        public string PlatformLongName { get; set; }
        public string PlatformShortName { get; set; }
        public string LeadOrganizationEmail { get; set; }
        public string APPINSIGHTS_INSTRUMENTATIONKEY { get; set; }
        public string OcStormwaterToolsBaseUrl { get; set; }
        public string OcStormwaterToolsModelingBaseUrl { get; set; }
        public string HangfireUsername { get; set; }
        public string HangfirePassword { get; set; }
        public string BlobStorageConnectionString {get; set; }
    }
}