using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class StormwaterJurisdictions
{
    public static List<StormwaterJurisdiction> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().OrderBy(ht => ht.Organization.OrganizationName).ToList();
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
        if (person.IsAdministrator() || person.IsAnonymousOrUnassigned())
        {
            return GetImpl(dbContext).AsNoTracking().ToList();
        }

        var stormwaterJurisdictionIDsViewable  = dbContext.StormwaterJurisdictionPeople.AsNoTracking().Where(x => x.PersonID == person.PersonID).Select(x => x.StormwaterJurisdictionID).ToList();
        return GetImpl(dbContext).AsNoTracking().Where(x => stormwaterJurisdictionIDsViewable.Contains(x.StormwaterJurisdictionID)).ToList();
    }

    public static IEnumerable<int> ListViewableIDsByPerson(NeptuneDbContext dbContext, Person person)
    {
        if (person.IsAdministrator() || person.IsAnonymousOrUnassigned())
        {
            return dbContext.StormwaterJurisdictions.AsNoTracking().Select(x => x.StormwaterJurisdictionID).ToList();
        }

        return dbContext.StormwaterJurisdictionPeople.AsNoTracking().Where(x => x.PersonID == person.PersonID).Select(x => x.StormwaterJurisdictionID).ToList();
    }

}