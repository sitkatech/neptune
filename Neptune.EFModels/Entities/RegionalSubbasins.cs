using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Models.DataTransferObjects;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

public static class RegionalSubbasins
{
    private static IQueryable<RegionalSubbasin> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.RegionalSubbasins
            ;
    }

    public static RegionalSubbasin GetByIDWithChangeTracking(NeptuneDbContext dbContext, int regionalSubbasinID)
    {
        var regionalSubbasin = GetImpl(dbContext)
            .SingleOrDefault(x => x.RegionalSubbasinID == regionalSubbasinID);
        Check.RequireNotNull(regionalSubbasin, $"RegionalSubbasin with ID {regionalSubbasinID} not found!");
        return regionalSubbasin;
    }

    public static RegionalSubbasin GetByIDWithChangeTracking(NeptuneDbContext dbContext, RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, regionalSubbasinPrimaryKey.PrimaryKeyValue);
    }

    public static RegionalSubbasin GetByID(NeptuneDbContext dbContext, int regionalSubbasinID)
    {
        var regionalSubbasin = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.RegionalSubbasinID == regionalSubbasinID);
        Check.RequireNotNull(regionalSubbasin, $"RegionalSubbasin with ID {regionalSubbasinID} not found!");
        return regionalSubbasin;
    }

    public static RegionalSubbasin GetByID(NeptuneDbContext dbContext, RegionalSubbasinPrimaryKey regionalSubbasinPrimaryKey)
    {
        return GetByID(dbContext, regionalSubbasinPrimaryKey.PrimaryKeyValue);
    }

    public static RegionalSubbasin GetByOCSurveyCatchmentID(NeptuneDbContext dbContext, int ocSurveyCatchmentID)
    {
        var regionalSubbasin = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.OCSurveyCatchmentID == ocSurveyCatchmentID);
        Check.RequireNotNull(regionalSubbasin, $"RegionalSubbasin with OCSurveyCatchmentID {ocSurveyCatchmentID} not found!");
        return regionalSubbasin;
    }

    public static List<RegionalSubbasin> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().OrderBy(x => x.RegionalSubbasinID).ToList();
    }

    public static RegionalSubbasin GetFirstByContainsGeometry (NeptuneDbContext dbContext, Geometry dBGeometry)
    {
        return dbContext.RegionalSubbasins.SingleOrDefault(x => x.CatchmentGeometry.Contains(dBGeometry));
    }

    public static Geometry GetUpstreamCatchmentGeometry4326 (NeptuneDbContext dbContext, int regionalSubbasinID)
    {
        return dbContext.vRegionalSubbasinUpstreamCatchmentGeometry4326s.SingleOrDefault(x => x.PrimaryKey == regionalSubbasinID)?.UpstreamCatchmentGeometry4326;
    }

    public static GeometryGeoJSONAndAreaDto GetUpstreamCatchmentGeometry4326GeoJSONAndArea(
        NeptuneDbContext dbContext, int regionalSubbasinID, int treatmentBMPID, int? delineationID)
    {
        return dbContext.vRegionalSubbasinUpstreamCatchmentGeometry4326s.SingleOrDefault(x => x.PrimaryKey == regionalSubbasinID)?.AsGeometryGeoJSONAndAreaDto(treatmentBMPID, delineationID);
    }
}