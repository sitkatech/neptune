using Neptune.Common.Email;

namespace Neptune.API.Services
{
    public class NeptuneConfiguration : SendGridConfiguration
    {
        public string DatabaseConnectionString { get; set; }
        public string AzureBlobStorageConnectionString { get; set; }
        public string KeystoneOpenIDUrl { get; set; }
        public string APPINSIGHTS_INSTRUMENTATIONKEY { get; set; }
        public string OcStormwaterToolsBaseUrl { get; set; }
        public string NereidUrl { get; set; }
    }
}