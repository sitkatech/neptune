using Neptune.Common.Email;

namespace Neptune.Jobs
{
    public class NeptuneJobConfiguration : SendGridConfiguration
    {
        public string PlanningModuleBaseUrl { get; set; }
        public string DatabaseServerName { get; set; }
        public string DatabaseName { get; set; }

        public string PathToPyqgisTestScript { get; set; }
        public string PyqgisWorkingDirectory { get; set; }
        public string PathToPyqgisLauncher { get; set; }
        public string PathToPyqgisProjData { get; set; }
        public string PyqgisUsername { get; set; }
        public string PyqgisPassword { get; set; }
    }
}
