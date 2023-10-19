namespace Neptune.QGISAPI.Services
{
    public class QGISAPIConfiguration
    {
        public string DatabaseConnectionString { get; set; }
        public string AzureBlobStorageConnectionString { get; set; }
        public string DatabaseServerName { get; set; }
        public string DatabaseName { get; set; }
        public string PyqgisUsername { get; set; }
        public string PyqgisPassword { get; set; }
    }
}