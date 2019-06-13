using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.ScheduledJobs
{
    public class ScheduledBackgroundJobLaunchHelper
    {
        public static void RunTrashGeneratingUnitRefreshScheduledBackgroundJob()
        {
            var trashGeneratingUnitRefreshScheduledBackgroundJob = new TrashGeneratingUnitRefreshScheduledBackgroundJob();
            trashGeneratingUnitRefreshScheduledBackgroundJob.RunJob();
        }
        public static void RunTrashGeneratingUnitAdjustmentScheduledBackgroundJob()
        {
            var trashGeneratingUnitAdjustmentScheduledBackgroundJob = new TrashGeneratingUnitAdjustmentScheduledBackgroundJob();
            trashGeneratingUnitAdjustmentScheduledBackgroundJob.RunJob();
        }

        public static void RunLandUseBlockUploadBackgroundJob()
        {
            var landUseBlockUploadBackgroundJob = new LandUseBlockUploadBackgroundJob();
            landUseBlockUploadBackgroundJob.RunJob();
        }
    }

    public class LandUseBlockUploadBackgroundJob : ScheduledBackgroundJobBase
    {
        public override List<NeptuneEnvironmentType> RunEnvironments { get; }
        protected override void RunJobImplementation()
        {
            //todo: process data from stagging table reporitng errors: on success send successful email, on failue send email of errors in upload file
            throw new NotImplementedException();
        }
    }
}
