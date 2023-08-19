using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class Organizations
{
    private static IQueryable<Organization> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.Organizations
            .Include(x => x.OrganizationType)
            .Include(x => x.LogoFileResource)
            .Include(x => x.FundingSources)
            .Include(x => x.People)
            .Include(x => x.PrimaryContactPerson);
    }

    public static Organization GetByIDWithChangeTracking(NeptuneDbContext dbContext, int organizationID)
    {
        var organization = GetImpl(dbContext)
            .SingleOrDefault(x => x.OrganizationID == organizationID);
        Check.RequireNotNull(organization, $"Organization with ID {organizationID} not found!");
        return organization;
    }

    public static Organization GetByIDWithChangeTracking(NeptuneDbContext dbContext, OrganizationPrimaryKey organizationPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, organizationPrimaryKey.PrimaryKeyValue);
    }

    public static Organization GetByID(NeptuneDbContext dbContext, int organizationID)
    {
        var organization = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.OrganizationID == organizationID);
        Check.RequireNotNull(organization, $"Organization with ID {organizationID} not found!");
        return organization;
    }

    public static Organization GetByID(NeptuneDbContext dbContext, OrganizationPrimaryKey organizationPrimaryKey)
    {
        return GetByID(dbContext, organizationPrimaryKey.PrimaryKeyValue);
    }

    public static List<Organization> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).ToList().OrderBy(x => x.GetDisplayName()).ToList();
    }
}