﻿using Neptune.Common.Email;

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

        public string PlanningModuleBaseUrl { get; set; }
        public string PyqgisWorkingDirectory { get; set; }

        public string GDALAPIBaseUrl { get; set; }
        public string OCGISBaseUrl { get; set; }
    }
}