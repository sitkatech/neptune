using System.Collections.Generic;
using Neptune.Web.Common;

namespace Neptune.Web.ScheduledJobs
{
    public class TrashGeneratingUnitRefreshScheduledBackgroundJob : ScheduledBackgroundJobBase
    {
        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };

        protected override void RunJobImplementation()
        {
            TrashGeneratingUnitRefreshImpl();
        }

        protected virtual void TrashGeneratingUnitRefreshImpl()
        {
            Logger.Info($"Processing '{JobName}'");

            HttpRequestStorage.DatabaseEntities.Database.CommandTimeout = 36000;
            HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand("execute dbo.pRebuildTrashGeneratingUnitTable");
        }
    }
}
