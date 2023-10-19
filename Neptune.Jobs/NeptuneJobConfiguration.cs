using Neptune.Common.Email;

namespace Neptune.Jobs
{
    public class NeptuneJobConfiguration : SendGridConfiguration
    {
        public string PlanningModuleBaseUrl { get; set; }
    }
}
