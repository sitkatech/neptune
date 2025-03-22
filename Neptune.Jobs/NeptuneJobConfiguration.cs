using Neptune.Common.Email;

namespace Neptune.Jobs
{
    public class NeptuneJobConfiguration : SendGridConfiguration
    {
        public string PlanningModuleBaseUrl { get; set; }
        public string TrashModuleBaseUrl { get; set; }
        public string AzureBlobStorageConnectionString { get; set; }
    }
}
