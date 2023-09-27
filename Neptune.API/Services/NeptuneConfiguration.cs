namespace Neptune.API.Services
{
    public class NeptuneConfiguration
    {
        public string KEYSTONE_HOST { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string SMTP_HOST { get; set; }
        public int SMTP_PORT { get; set; }
        public string SITKA_EMAIL_REDIRECT { get; set; }
        public string HippocampWebUrl { get; set; }
        public string KeystoneOpenIDUrl { get; set; }
        public string PlatformLongName { get; set; }
        public string PlatformShortName { get; set; }
        public string LeadOrganizationEmail { get; set; }
        public string APPINSIGHTS_INSTRUMENTATIONKEY { get; set; }
        public string OcStormwaterToolsBaseUrl { get; set; }
        public string OcStormwaterToolsModelingBaseUrl { get; set; }
        public string AzureBlobStorageConnectionString {get; set; }
    }
}