using Microsoft.EntityFrameworkCore;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public partial class Watershed
    {
        public static List<WatershedSimpleDto> List(NeptuneDbContext dbContext)
        {
            return GetWatershedsImpl(dbContext).Select(x => x.AsSimpleDto()).ToList();
        }

        private static IQueryable<Watershed> GetWatershedsImpl(NeptuneDbContext dbContext)
        {
            return dbContext.Watersheds.AsNoTracking();
        }

        public static BoundingBoxDto GetBoundingBoxByWatershedIDs(NeptuneDbContext dbContext, List<int> watershedIDs)
        {
            var watersheds = dbContext.Watersheds
                .AsNoTracking()
                .Where(x => watershedIDs.Contains(x.WatershedID));

            var geometries = watersheds.Select(x => x.WatershedGeometry4326).ToList();
            return new BoundingBoxDto(geometries);
        }
    }
}