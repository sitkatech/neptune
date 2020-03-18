using Neptune.Web.Common;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace Neptune.Web.ScheduledJobs
{
    public class HRURefreshBackgroundJob : ScheduledBackgroundJobBase
    {

        public HRURefreshBackgroundJob()
        {
        }

        public new static string JobName => "LGU Refresh";

        public override List<NeptuneEnvironmentType> RunEnvironments => new List<NeptuneEnvironmentType>
        {
            NeptuneEnvironmentType.Local,
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa
        };
        protected override void RunJobImplementation()
        {
            HRURefreshImpl();
        }

        private void HRURefreshImpl()
        {
            // assume the LGUs are already correct
            //var loadGeneratingUnitRefreshScheduledBackgroundJob = new LoadGeneratingUnitRefreshScheduledBackgroundJob(null);

            //loadGeneratingUnitRefreshScheduledBackgroundJob.RunJob();

            foreach (var lspcBasin in DbContext.LSPCBasins)
            {
                var lspcBasinLoadGeneratingUnits = lspcBasin.LoadGeneratingUnits.ToList();

                var batches = lspcBasinLoadGeneratingUnits.Batch(25);
                foreach (var batch in batches)
                {
                    HRUUtilities.RetrieveAndNotSaveHRUCharacteristics(batch, DbContext);
                }
            }
        }
    }
}
 