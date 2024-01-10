using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class StormwaterJurisdictionPeople
{
    private static IQueryable<StormwaterJurisdictionPerson> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.StormwaterJurisdictionPeople;
    }

    public static StormwaterJurisdictionPerson GetByIDWithChangeTracking(NeptuneDbContext dbContext, int stormwaterJurisdictionPersonID)
    {
        var stormwaterJurisdictionPerson = GetImpl(dbContext)
            .SingleOrDefault(x => x.StormwaterJurisdictionPersonID == stormwaterJurisdictionPersonID);
        Check.RequireNotNull(stormwaterJurisdictionPerson, $"StormwaterJurisdictionPerson with ID {stormwaterJurisdictionPersonID} not found!");
        return stormwaterJurisdictionPerson;
    }

    public static StormwaterJurisdictionPerson GetByIDWithChangeTracking(NeptuneDbContext dbContext, StormwaterJurisdictionPersonPrimaryKey stormwaterJurisdictionPersonPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, stormwaterJurisdictionPersonPrimaryKey.PrimaryKeyValue);
    }

    public static StormwaterJurisdictionPerson GetByID(NeptuneDbContext dbContext, int stormwaterJurisdictionPersonID)
    {
        var stormwaterJurisdictionPerson = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.StormwaterJurisdictionPersonID == stormwaterJurisdictionPersonID);
        Check.RequireNotNull(stormwaterJurisdictionPerson, $"StormwaterJurisdictionPerson with ID {stormwaterJurisdictionPersonID} not found!");
        return stormwaterJurisdictionPerson;
    }

    public static StormwaterJurisdictionPerson GetByID(NeptuneDbContext dbContext, StormwaterJurisdictionPersonPrimaryKey stormwaterJurisdictionPersonPrimaryKey)
    {
        return GetByID(dbContext, stormwaterJurisdictionPersonPrimaryKey.PrimaryKeyValue);
    }

    public static IEnumerable<int> ListViewableStormwaterJurisdictionIDsByPersonForBMPs(NeptuneDbContext dbContext, Person person)
    {
        if (person.IsAdministrator())
        {
            return GetImpl(dbContext).AsNoTracking().Select(x => x.StormwaterJurisdictionID).ToList();
        }

        if (person.IsAnonymousOrUnassigned())
        {
            return StormwaterJurisdictions.List(dbContext)
                .Where(x => x.StormwaterJurisdictionPublicBMPVisibilityTypeID ==
                            (int)StormwaterJurisdictionPublicBMPVisibilityTypeEnum.VerifiedOnly)
                .Select(x => x.StormwaterJurisdictionID);
        }

        return GetImpl(dbContext).AsNoTracking().Where(x => x.PersonID == person.PersonID).Select(x => x.StormwaterJurisdictionID).ToList();
    }

    public static IEnumerable<int> ListViewableStormwaterJurisdictionIDsByPersonForWQMPs(NeptuneDbContext dbContext, Person person)
    {
        if (person.IsAdministrator())
        {
            return GetImpl(dbContext).AsNoTracking().Select(x => x.StormwaterJurisdictionID).ToList();
        }

        if (person.IsAnonymousOrUnassigned())
        {
            return StormwaterJurisdictions.List(dbContext)
                .Where(x => x.StormwaterJurisdictionPublicWQMPVisibilityTypeID !=
                            (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.None)
                .Select(x => x.StormwaterJurisdictionID);
        }

        return GetImpl(dbContext).AsNoTracking().Where(x => x.PersonID == person.PersonID).Select(x => x.StormwaterJurisdictionID).ToList();
    }

    public static Dictionary<int, int> ListCountByStormwaterJurisdiction(NeptuneDbContext dbContext)
    {
        return dbContext.StormwaterJurisdictionPeople.AsNoTracking().GroupBy(x => x.StormwaterJurisdictionID).Select(x => new { x.Key, Count = x.Count() })
            .ToDictionary(x => x.Key, x => x.Count);
    }

    public static List<StormwaterJurisdictionPerson> ListByStormwaterJurisdictionID(NeptuneDbContext dbContext, int stormwaterJurisdictionID)
    {
        return dbContext.StormwaterJurisdictionPeople
            .Include(x => x.Person)
            .ThenInclude(x => x.Organization)
            .AsNoTracking().Where(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID).ToList();
    }
}