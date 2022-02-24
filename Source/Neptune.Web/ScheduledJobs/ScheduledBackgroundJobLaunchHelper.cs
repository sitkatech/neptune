using Neptune.Web.Areas.Modeling.Controllers;
using Neptune.Web.Models;

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

        public static void RunRegionalSubbasinRefreshBackgroundJob(int personID, bool queueLGURefresh)
        {
            var regionalSubbasinRefreshScheduledBackgroundJob = new RegionalSubbasinRefreshScheduledBackgroundJob(personID, queueLGURefresh);
            regionalSubbasinRefreshScheduledBackgroundJob.RunJob();
        }

        public static void RunModelBasinRefreshBackgroundJob(int personID)
        {
            var modelBasinRefreshScheduledBackgroundJob = new ModelBasinRefreshScheduledBackgroundJob(personID);
            modelBasinRefreshScheduledBackgroundJob.RunJob();
        }

        public static void RunPrecipitationZoneRefreshBackgroundJob(int personID)
        {
            var precipitationZoneRefreshScheduledBackgroundJob = new PrecipitationZoneRefreshScheduledBackgroundJob(personID);
            precipitationZoneRefreshScheduledBackgroundJob.RunJob();
        }

        public static void RunDelineationDiscrepancyCheckerJob()
        {
            var delineationDiscrepancyCheckerBackgroundJob = new DelineationDiscrepancyCheckerBackgroundJob();
            delineationDiscrepancyCheckerBackgroundJob.RunJob();
        }

        public static void RunLoadGeneratingUnitRefreshJob(int? loadGeneratingUnitRefreshAreaID)
        {
            var loadGeneratingUnitRefreshScheduledBackgroundJob = new LoadGeneratingUnitRefreshScheduledBackgroundJob(loadGeneratingUnitRefreshAreaID);
            loadGeneratingUnitRefreshScheduledBackgroundJob.RunJob();
        }
        
        public static void RunHRURefreshJob()
        {
            var hruRefreshBackgroundJob = new HRURefreshBackgroundJob();
            hruRefreshBackgroundJob.RunJob();
        }

        // TODO: remove this after it's run once in PROD
        public static void RunRefreshAssessmentScoreJob(int personID)
        {
            var refreshAssessmentScoreJob = new RefreshAssessmentScoreJob(personID);
            refreshAssessmentScoreJob.RunJob();
        }

        public static void RunTotalNetworkSolve()
        {
            var totalNetworkSolveJob = new TotalNetworkSolveJob();
            totalNetworkSolveJob.RunJob();
        }

        public static void RunDeltaSolve()
        {
            var deltaSolveJob = new DeltaSolveJob();
            deltaSolveJob.RunJob();
        }
    }
}
