﻿using Hangfire;
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
            //NeptuneEnvironmentType.Local
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


            // if there was any work done, 
            if (loadGeneratingUnitsToUpdate.Any())
            {
                DbContext.LoadGeneratingUnits.Load();

                // check if all the HRUs are populated and if so blast off with a new solve.
                var loadGeneratingUnitsMissingHrus = DbContext.LoadGeneratingUnits.Where(x =>
                    !x.HRUCharacteristics.Any() && 
                    x.RegionalSubbasinID != null &&
                    x.IsEmptyResponseFromHRUService != true);

                if (!loadGeneratingUnitsMissingHrus.Any())
                {
                    BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunTotalNetworkSolve());
                }
            }

            stopwatch.Stop();
        }
    }
}