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

        public new static string JobName => "HRU Refresh";

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
            
            // collect the load generating units that require updates,
            // group them by LSPC basin so requests to the HRU service are spatially bounded,
            // and batch them for processing 25 at a time so requests are small.

            var loadGeneratingUnitsToUpdate = DbContext.LoadGeneratingUnits.Where(x => !(x.HRUCharacteristics.Any() || x.RegionalSubbasinID == null)).ToList();

            var loadGeneratingUnitsToUpdateGroupedByLSPCBasin = loadGeneratingUnitsToUpdate.GroupBy(x=>x.LSPCBasin);

            foreach (var group in loadGeneratingUnitsToUpdateGroupedByLSPCBasin)
            {
                var batches = group.Batch(50);

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