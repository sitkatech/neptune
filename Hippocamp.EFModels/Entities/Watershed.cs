using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Hippocamp.Models.DataTransferObjects;
using Hippocamp.Models.DataTransferObjects.Watershed;

namespace Hippocamp.EFModels.Entities
{
    public partial class Watershed
    {
        public static List<WatershedDto> List(HippocampDbContext dbContext)
        {
            return GetWatershedsImpl(dbContext).Select(x => x.AsDto()).ToList();
        }

        public static WatershedDto GetByWatershedID(HippocampDbContext dbContext, int watershedID)
        {
            return GetWatershedsImpl(dbContext).SingleOrDefault(x => x.WatershedID == watershedID)?.AsDto();
        }

        public static List<WatershedDto> GetByWatershedID(HippocampDbContext dbContext, List<int> watershedIDs)
        {
            return GetWatershedsImpl(dbContext).Where(x => watershedIDs.Contains(x.WatershedID)).Select(x => x.AsDto()).ToList();
        }

        private static IQueryable<Watershed> GetWatershedsImpl(HippocampDbContext dbContext)
        {
            return dbContext.Watersheds.AsNoTracking();
        }

        public static BoundingBoxDto GetBoundingBoxByWatershedIDs(HippocampDbContext dbContext, List<int> watershedIDs)
        {
            var watersheds = dbContext.Watersheds
                .AsNoTracking()
                .Where(x => watershedIDs.Contains(x.WatershedID));

            var geometries = watersheds.Select(x => x.WatershedGeometry4326).ToList();
            return new BoundingBoxDto(geometries);
        }
    }
}