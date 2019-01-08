//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrainingVideo]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TrainingVideo GetTrainingVideo(this IQueryable<TrainingVideo> trainingVideos, int trainingVideoID)
        {
            var trainingVideo = trainingVideos.SingleOrDefault(x => x.TrainingVideoID == trainingVideoID);
            Check.RequireNotNullThrowNotFound(trainingVideo, "TrainingVideo", trainingVideoID);
            return trainingVideo;
        }

        public static void DeleteTrainingVideo(this IQueryable<TrainingVideo> trainingVideos, List<int> trainingVideoIDList)
        {
            if(trainingVideoIDList.Any())
            {
                trainingVideos.Where(x => trainingVideoIDList.Contains(x.TrainingVideoID)).Delete();
            }
        }

        public static void DeleteTrainingVideo(this IQueryable<TrainingVideo> trainingVideos, ICollection<TrainingVideo> trainingVideosToDelete)
        {
            if(trainingVideosToDelete.Any())
            {
                var trainingVideoIDList = trainingVideosToDelete.Select(x => x.TrainingVideoID).ToList();
                trainingVideos.Where(x => trainingVideoIDList.Contains(x.TrainingVideoID)).Delete();
            }
        }

        public static void DeleteTrainingVideo(this IQueryable<TrainingVideo> trainingVideos, int trainingVideoID)
        {
            DeleteTrainingVideo(trainingVideos, new List<int> { trainingVideoID });
        }

        public static void DeleteTrainingVideo(this IQueryable<TrainingVideo> trainingVideos, TrainingVideo trainingVideoToDelete)
        {
            DeleteTrainingVideo(trainingVideos, new List<TrainingVideo> { trainingVideoToDelete });
        }
    }
}