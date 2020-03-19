using System;
using Neptune.Web.Common;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Neptune.Web.Models;

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
            //NeptuneEnvironmentType.Local,
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

            List<HRUCharacteristic> newHRUCharacteristics = new List<HRUCharacteristic>();

            // ignore this comment for now, it's about a way that I'm not currently doing things
            // loop through the LSPC basins until we find LGUs without HRUs and update those guys.
            // also skip it if there are no RSBs present since that's a null case.
            foreach (var lspcBasin in DbContext.LSPCBasins)
            {
                var lspcBasinLoadGeneratingUnits = lspcBasin.LoadGeneratingUnits.Where(x => !(x.HRUCharacteristics.Any() || x.RegionalSubbasinID == null) ).ToList();

                if (lspcBasinLoadGeneratingUnits.Any())
                {
                    var batches = lspcBasinLoadGeneratingUnits.Batch(25).ToList();
                    foreach (var batch in batches)
                    {
                        var batchHRUCharacteristics =
                            HRUUtilities.RetrieveHRUCharacteristics(batch.ToList(), DbContext, Logger);
                        newHRUCharacteristics.AddRange(batchHRUCharacteristics);
                    }
                }

            }

            DbContext.HRUCharacteristics.AddRange(newHRUCharacteristics);
            DbContext.SaveChangesWithNoAuditing();
        }
    }
}
