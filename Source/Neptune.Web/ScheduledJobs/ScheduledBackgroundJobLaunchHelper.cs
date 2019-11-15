namespace Neptune.Web.ScheduledJobs
{
    public class ScheduledBackgroundJobLaunchHelper
    {
        public static void RunTrashGeneratingUnitRefreshScheduledBackgroundJob()
        {
            var trashGeneratingUnitRefreshScheduledBackgroundJob = new TrashGeneratingUnitRefreshScheduledBackgroundJob();
            trashGeneratingUnitRefreshScheduledBackgroundJob.RunJob();
        }

        public static void RunLandUseBlockUploadBackgroundJob(int personID)
        {
            var landUseBlockUploadBackgroundJob = new LandUseBlockUploadBackgroundJob(personID);
            landUseBlockUploadBackgroundJob.RunJob();
        }

        public static void RunNetworkCatchmentRefreshBackgroundJob(int currentPersonPersonID)
        {
            var networkCatchmentRefreshScheduledBackgroundJob = new NetworkCatchmentRefreshScheduledBackgroundJob(currentPersonPersonID);
            networkCatchmentRefreshScheduledBackgroundJob.RunJob();
        }
    }
}
