using System;
using Neptune.Web.Common;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Neptune.Web.Models;
using Exception = System.Exception;

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
            // this job assumes the LGUs are already correct but that for whatever reason, some are missing their HRUs
            
            // loop through the LSPC basins until we find LGUs without HRUs and update those guys.
            // also skip it if there are no RSBs present since that's a null case.
            foreach (var lspcBasin in DbContext.LSPCBasins.ToList())
            {
                var lspcBasinLoadGeneratingUnits = lspcBasin.LoadGeneratingUnits.Where(x => !(x.HRUCharacteristics.Any() || x.RegionalSubbasinID == null) ).ToList();

                if (lspcBasinLoadGeneratingUnits.Any())
                {
                    var batches = lspcBasinLoadGeneratingUnits.Batch(25).ToList();
                    foreach (var batch in batches)
                    {
                        try
                        {
                            var batchHRUCharacteristics =
                                HRUUtilities.RetrieveHRUCharacteristics(batch.ToList(), DbContext, Logger);

                            DbContext.HRUCharacteristics.AddRange(batchHRUCharacteristics);
                            DbContext.SaveChangesWithNoAuditing();
                        }
                        catch (Exception ex)
                        {
                            // this batch failed, but we don't want to give up the whole job.
                            Logger.Warn(ex.Message);
                        }
                    }
                }
            }
        }
    }
}