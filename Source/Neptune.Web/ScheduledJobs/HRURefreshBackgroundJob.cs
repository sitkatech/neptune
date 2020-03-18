﻿using System;
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

            List<HRUCharacteristic> newHRUCharacteristics = new List<HRUCharacteristic>();

            // loop through the LSPC basins until we find LGUs without HRUs and update those guys.
            foreach (var lspcBasin in DbContext.LSPCBasins)
            {
                var lspcBasinLoadGeneratingUnits = lspcBasin.LoadGeneratingUnits.Where(x => !x.HRUCharacteristics.Any()).ToList();

                if (lspcBasinLoadGeneratingUnits.Any())
                {
                    var batches = lspcBasinLoadGeneratingUnits.Batch(25);
                    
                    var batchHRUCharacteristics = HRUUtilities.RetrieveHRUCharacteristics(batches.First(), DbContext);
                    newHRUCharacteristics.AddRange(batchHRUCharacteristics);
                    break;
                }

            }

            DbContext.HRUCharacteristics.AddRange(newHRUCharacteristics);
            DbContext.SaveChangesWithNoAuditing();
        }
    }
}
