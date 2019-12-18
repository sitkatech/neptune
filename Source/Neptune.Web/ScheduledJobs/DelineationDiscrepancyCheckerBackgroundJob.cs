using Neptune.Web.Common;
using System.Collections.Generic;

namespace Neptune.Web.ScheduledJobs
{
    public class DelineationDiscrepancyCheckerBackgroundJob : ScheduledBackgroundJobBase
    {
        public new static string JobName => "Delineation Discrepancy Checker";

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            var dbContext = DbContext;
            dbContext.Database.CommandTimeout = 30000;
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pDelineationMarkThoseThatHaveDiscrepancies");
        }
    }
}