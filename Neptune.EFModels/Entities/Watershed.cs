using Microsoft.EntityFrameworkCore;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public partial class Watershed
    {
        public static List<WatershedDto> List(NeptuneDbContext dbContext)
        {
            return GetWatershedsImpl(dbContext).Select(x => x.AsDto()).ToList();
        }

        public static WatershedDto GetByWatershedID(NeptuneDbContext dbContext, int watershedID)
        {
            return GetWatershedsImpl(dbContext).SingleOrDefault(x => x.WatershedID == watershedID)?.AsDto();
        }

        public static List<WatershedDto> GetByWatershedID(NeptuneDbContext dbContext, List<int> watershedIDs)
        {
            return GetWatershedsImpl(dbContext).Where(x => watershedIDs.Contains(x.WatershedID)).Select(x => x.AsDto()).ToList();
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