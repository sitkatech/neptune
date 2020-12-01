using System;
using Hangfire;
using MoreLinq;
using Neptune.Web.Common;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
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
            NeptuneEnvironmentType.Prod,
            NeptuneEnvironmentType.Qa,
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

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var loadGeneratingUnitsToUpdate = DbContext.LoadGeneratingUnits.Where(x => !(x.HRUCharacteristics.Any() || x.RegionalSubbasinID == null || x.IsEmptyResponseFromHRUService == true)).ToList();

            var loadGeneratingUnitsToUpdateGroupedByLSPCBasin = loadGeneratingUnitsToUpdate.GroupBy(x=>x.LSPCBasin);

            foreach (var group in loadGeneratingUnitsToUpdateGroupedByLSPCBasin)
            {
                var batches = group.Batch(25);

                foreach (var batch in batches)
                {
                    try
                    {
                        var batchHRUCharacteristics =
                            HRUUtilities.RetrieveHRUCharacteristics(batch.ToList(), Logger).ToList();

                        if (!batchHRUCharacteristics.Any())
                        {
                            foreach (var loadGeneratingUnit in batch)
                            {
                                loadGeneratingUnit.IsEmptyResponseFromHRUService = true;
                            }
                        }

                        DbContext.HRUCharacteristics.AddRange(batchHRUCharacteristics);
                        DbContext.SaveChangesWithNoAuditing();
                    }
                    catch (Exception ex)
                    {
                        // this batch failed, but we don't want to give up the whole job.
                        Logger.Warn(ex.Message);
                    }

                    if (stopwatch.Elapsed.Minutes > 20)
                    {
                        break;
                    }
                }

                if (stopwatch.Elapsed.Minutes > 20)
                {
                    break;
                }
            }


            ExecuteModelIfNeeded(loadGeneratingUnitsToUpdate.Any());

            stopwatch.Stop();
        }

        private void ExecuteModelIfNeeded(bool wereAnyLoadGeneratingUnitsToUpdate)
        {
            var updatedRegionalSubbasins = DbContext.RegionalSubbasins.Where(x=>x.LastUpdate != null).ToList();
            var lastRegionalSubbasinUpdateDate = updatedRegionalSubbasins.Any() ? updatedRegionalSubbasins.Max(x=>x.LastUpdate.Value) : DateTime.MinValue;
            var updatedNereidResults = DbContext.NereidResults.Where(x=>x.LastUpdate != null).ToList();
            var lastNereidResultUpdateDate = updatedNereidResults.Any() ? updatedNereidResults.Max(x=>x.LastUpdate.Value) : DateTime.MinValue;
            
            if (wereAnyLoadGeneratingUnitsToUpdate)
            {
                // if there was any work done, check if all the HRUs are populated and if so blast off with a new solve.
                DbContext.LoadGeneratingUnits.Load();

                // don't die if it takes longer than 30 seconds for this next query to come back
                DbContext.Database.CommandTimeout = 600;
                var loadGeneratingUnitsMissingHrus = DbContext.LoadGeneratingUnits.Any(x =>
                    !(x.HRUCharacteristics.Any() || x.RegionalSubbasinID == null ||
                      x.IsEmptyResponseFromHRUService == true));

                if (!loadGeneratingUnitsMissingHrus)
                {
                    if (lastRegionalSubbasinUpdateDate > lastNereidResultUpdateDate)
                    {
                        BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunTotalNetworkSolve());
                    }
                    else if(DbContext.DirtyModelNodes.Any())
                    {
                        BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunDeltaSolve());
                    }
                }
            }
            else
            {
                // if the job woke up and went immediately to sleep, then all HRUs are populated.
                if (lastRegionalSubbasinUpdateDate > lastNereidResultUpdateDate)
                {
                    BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunTotalNetworkSolve());
                }
                else if (DbContext.DirtyModelNodes.Any())
                {
                    BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunDeltaSolve());
                }
            }
        }
    }
}