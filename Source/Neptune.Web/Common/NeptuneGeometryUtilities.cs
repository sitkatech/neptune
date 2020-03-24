using System;
using JetBrains.Annotations;
using Neptune.Web.Models;
using Neptune.Web.ScheduledJobs;
using System.Data.Entity.Spatial;
using Hangfire;

namespace Neptune.Web.Common
{
    public static class ModelingEngineUtilities
    {
        public static void QueueLGURefreshForArea(DbGeometry oldShape, DbGeometry newShape)
        {
            DbGeometry loadGeneratingUnitRefreshAreaGeometry;

            if (oldShape == null && newShape == null)
            {
                throw new InvalidOperationException("At least one input to QueueLGURefreshForArea must not be null.");
            }

            if (oldShape == null)
            {
                loadGeneratingUnitRefreshAreaGeometry = newShape;
            }
            else if (newShape == null)
            {
                loadGeneratingUnitRefreshAreaGeometry = oldShape;
            }
            else
            {
                loadGeneratingUnitRefreshAreaGeometry = oldShape.Union(newShape);
            }

            var loadGeneratingUnitRefreshArea = new LoadGeneratingUnitRefreshArea(loadGeneratingUnitRefreshAreaGeometry);

            HttpRequestStorage.DatabaseEntities.LoadGeneratingUnitRefreshAreas.Add(loadGeneratingUnitRefreshArea);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunLoadGeneratingUnitRefreshJob(loadGeneratingUnitRefreshArea.LoadGeneratingUnitRefreshAreaID));
        }
    }
}
