using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
