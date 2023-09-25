using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class StormwaterJurisdictions
{
    public static List<StormwaterJurisdiction> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().OrderBy(ht => ht.Organization.OrganizationName).ToList();
    }

    public static List<StormwaterJurisdictionSimpleDto> ListByIDsAsSimpleDto(NeptuneDbContext dbContext, List<int> stormwaterJurisdictionIDs)
    {
        return GetImpl(dbContext)
            .Where(x => stormwaterJurisdictionIDs.Contains(x.StormwaterJurisdictionID))
            .OrderBy(x => x.Organization.OrganizationName)
            .Select(x => x.AsSimpleDto())
            .ToList();
    }

    public static BoundingBoxDto GetBoundingBoxDtoByJurisdictionID(NeptuneDbContext dbContext, int stormwaterJurisdictionID)
    {
        var stormwaterJurisdictionGeometry = dbContext.StormwaterJurisdictionGeometries
            .Where(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID)
            .Select(x => x.Geometry4326);

        return new BoundingBoxDto(stormwaterJurisdictionGeometry);
    }

    public static BoundingBoxDto GetBoundingBoxDtoByPersonID(NeptuneDbContext dbContext, int personID)
    {
        var person = People.GetByID(dbContext, personID);
        var jurisdictions = dbContext.StormwaterJurisdictionGeometries;
        if (person.RoleID != (int)RoleEnum.Admin || person.RoleID != (int)RoleEnum.SitkaAdmin)
        {
            var jurisdictionIDs = People.ListStormwaterJurisdictionIDsByPersonID(dbContext, personID);
            jurisdictions.Where(x => jurisdictionIDs.Contains(x.StormwaterJurisdictionID));
        }
        return new BoundingBoxDto(jurisdictions.Select(x => x.Geometry4326).ToList());
    }

    public static StormwaterJurisdiction GetByIDWithChangeTracking(NeptuneDbContext dbContext, int stormwaterJurisdictionID)
    {
        var stormwaterJurisdiction = GetImpl(dbContext)
            .SingleOrDefault(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID);
        Check.RequireNotNull(stormwaterJurisdiction, $"StormwaterJurisdiction with ID {stormwaterJurisdictionID} not found!");
        return stormwaterJurisdiction;
    }

    public static StormwaterJurisdiction GetByIDWithChangeTracking(NeptuneDbContext dbContext, StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, stormwaterJurisdictionPrimaryKey.PrimaryKeyValue);
    }

    public static StormwaterJurisdiction GetByID(NeptuneDbContext dbContext, int stormwaterJurisdictionID)
    {
        var stormwaterJurisdiction = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID);
        Check.RequireNotNull(stormwaterJurisdiction, $"StormwaterJurisdiction with ID {stormwaterJurisdictionID} not found!");
        return stormwaterJurisdiction;
    }

    public static StormwaterJurisdiction GetByID(NeptuneDbContext dbContext, StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey)
    {
        return GetByID(dbContext, stormwaterJurisdictionPrimaryKey.PrimaryKeyValue);
    }

    private static IQueryable<StormwaterJurisdiction> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.StormwaterJurisdictions
            .Include(x => x.Organization)
            .ThenInclude(x => x.OrganizationType);
    }

    public static List<StormwaterJurisdiction> ListViewableByPerson(NeptuneDbContext dbContext, Person person)
    {
        var stormwaterJurisdictionIDsViewable  = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPerson(dbContext, person);
        return GetImpl(dbContext).AsNoTracking().Where(x => stormwaterJurisdictionIDsViewable.Contains(x.StormwaterJurisdictionID)).ToList();
    }
}