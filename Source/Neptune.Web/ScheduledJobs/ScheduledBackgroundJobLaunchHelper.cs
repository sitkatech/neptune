namespace Neptune.Web.ScheduledJobs
{
    public static class ScheduledBackgroundJobLaunchHelper
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

        public static void RunNetworkCatchmentRefreshBackgroundJob(int personID)
        {
            var networkCatchmentRefreshScheduledBackgroundJob = new NetworkCatchmentRefreshScheduledBackgroundJob(personID);
            networkCatchmentRefreshScheduledBackgroundJob.RunJob();
        }

        public static void RunDelineationDiscrepancyCheckerJob()
        {
            var delineationDiscrepancyCheckerBackgroundJob = new DelineationDiscrepancyCheckerBackgroundJob();
            delineationDiscrepancyCheckerBackgroundJob.RunJob();
        }
    }
}
