using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class StormwaterJurisdictionGeometries
{
    public static List<StormwaterJurisdictionGeometry> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().OrderBy(ht => ht.StormwaterJurisdiction.Organization.OrganizationName).ToList();
    }

    public static StormwaterJurisdictionGeometry GetByIDWithChangeTracking(NeptuneDbContext dbContext, int stormwaterJurisdictionGeometryID)
    {
        var stormwaterJurisdictionGeometry = GetImpl(dbContext)
            .SingleOrDefault(x => x.StormwaterJurisdictionGeometryID == stormwaterJurisdictionGeometryID);
        Check.RequireNotNull(stormwaterJurisdictionGeometry, $"StormwaterJurisdictionGeometry with ID {stormwaterJurisdictionGeometryID} not found!");
        return stormwaterJurisdictionGeometry;
    }

    public static StormwaterJurisdictionGeometry GetByIDWithChangeTracking(NeptuneDbContext dbContext, StormwaterJurisdictionGeometryPrimaryKey stormwaterJurisdictionGeometryPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, stormwaterJurisdictionGeometryPrimaryKey.PrimaryKeyValue);
    }

    public static StormwaterJurisdictionGeometry GetByID(NeptuneDbContext dbContext, int stormwaterJurisdictionGeometryID)
    {
        var stormwaterJurisdictionGeometry = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.StormwaterJurisdictionGeometryID == stormwaterJurisdictionGeometryID);
        Check.RequireNotNull(stormwaterJurisdictionGeometry, $"StormwaterJurisdictionGeometry with ID {stormwaterJurisdictionGeometryID} not found!");
        return stormwaterJurisdictionGeometry;
    }

    public static StormwaterJurisdictionGeometry GetByID(NeptuneDbContext dbContext, StormwaterJurisdictionGeometryPrimaryKey stormwaterJurisdictionGeometryPrimaryKey)
    {
        return GetByID(dbContext, stormwaterJurisdictionGeometryPrimaryKey.PrimaryKeyValue);
    }

    private static IQueryable<StormwaterJurisdictionGeometry> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.StormwaterJurisdictionGeometries
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            .ThenInclude(x => x.OrganizationType);
    }

    public static List<StormwaterJurisdictionGeometry> ListByStormwaterJurisdictionIDList(NeptuneDbContext dbContext, IEnumerable<int> stormwaterJurisdictionGeometryIDs)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => stormwaterJurisdictionGeometryIDs.Contains(x.StormwaterJurisdictionGeometryID)).ToList();
    }
}