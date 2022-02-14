using Hippocamp.Models.DataTransferObjects;
using NetTopologySuite.Geometries;
using System.Linq;

namespace Hippocamp.EFModels.Entities
{
    public partial class RegionalSubbasins
    {
        public static RegionalSubbasin GetFirstByContainsGeometry (HippocampDbContext dbContext, Geometry dBGeometry)
        {
            return dbContext.RegionalSubbasins.SingleOrDefault(x => x.CatchmentGeometry.Contains(dBGeometry));
        }

        public static Geometry GetUpstreamCatchmentGeometry4326 (HippocampDbContext dbContext, int regionalSubbasinID)
        {
            return dbContext.vRegionalSubbasinUpstreamCatchmentGeometry4326s.SingleOrDefault(x => x.PrimaryKey == regionalSubbasinID)?.UpstreamCatchmentGeometry4326;
        }

        public static string GetUpstreamCatchmentGeometry4326GeoJSON (HippocampDbContext dbContext, int regionalSubbasinID)
        {
            return dbContext.vRegionalSubbasinUpstreamCatchmentGeometry4326s.SingleOrDefault(x => x.PrimaryKey == regionalSubbasinID)?.UpstreamCatchGeometry4326GeoJson;
        }
    }
}