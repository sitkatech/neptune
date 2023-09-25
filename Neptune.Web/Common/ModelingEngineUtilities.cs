using Neptune.EFModels.Entities;
using NetTopologySuite.Geometries;

namespace Neptune.Web.Common
{
    public static class ModelingEngineUtilities
    {
        public static void QueueLGURefreshForArea(Geometry? oldShape, Geometry? newShape, NeptuneDbContext dbContext)
        {
            Geometry loadGeneratingUnitRefreshAreaGeometry;

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

            var loadGeneratingUnitRefreshArea = new LoadGeneratingUnitRefreshArea()
            {
                LoadGeneratingUnitRefreshAreaGeometry = loadGeneratingUnitRefreshAreaGeometry,
                ProcessDate = DateTime.Now
            };

            dbContext.LoadGeneratingUnitRefreshAreas.Add(loadGeneratingUnitRefreshArea);
            dbContext.SaveChanges();

            //BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunLoadGeneratingUnitRefreshJob(loadGeneratingUnitRefreshArea.LoadGeneratingUnitRefreshAreaID));
        }
    }
}
