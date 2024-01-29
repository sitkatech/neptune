using Hangfire;
using Neptune.EFModels.Entities;
using Neptune.Jobs.Hangfire;
using NetTopologySuite.Geometries;

namespace Neptune.WebMvc.Common
{
    public static class ModelingEngineUtilities
    {
        public static async Task QueueLGURefreshForArea(Geometry? oldShape, Geometry? newShape, NeptuneDbContext dbContext)
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
                ProcessDate = DateTime.UtcNow
            };

            await dbContext.LoadGeneratingUnitRefreshAreas.AddAsync(loadGeneratingUnitRefreshArea);
            await dbContext.SaveChangesAsync();

            BackgroundJob.Enqueue<LoadGeneratingUnitRefreshJob>(x => x.RunJob(loadGeneratingUnitRefreshArea.LoadGeneratingUnitRefreshAreaID));
        }
    }
}
