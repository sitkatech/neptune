using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class FundingSources
{
    public static List<FundingSource> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().OrderBy(ht => ht.FundingSourceName).ToList();
    }

    public static FundingSource GetByIDWithChangeTracking(NeptuneDbContext dbContext, int fundingSourceID)
    {
        var fundingSource = GetImpl(dbContext)
            .SingleOrDefault(x => x.FundingSourceID == fundingSourceID);
        Check.RequireNotNull(fundingSource, $"FundingSource with ID {fundingSourceID} not found!");
        return fundingSource;
    }

    public static FundingSource GetByIDWithChangeTracking(NeptuneDbContext dbContext, FundingSourcePrimaryKey fundingSourcePrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, fundingSourcePrimaryKey.PrimaryKeyValue);
    }

    public static FundingSource GetByID(NeptuneDbContext dbContext, int fundingSourceID)
    {
        var fundingSource = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.FundingSourceID == fundingSourceID);
        Check.RequireNotNull(fundingSource, $"FundingSource with ID {fundingSourceID} not found!");
        return fundingSource;
    }

    public static FundingSource GetByID(NeptuneDbContext dbContext, FundingSourcePrimaryKey fundingSourcePrimaryKey)
    {
        return GetByID(dbContext, fundingSourcePrimaryKey.PrimaryKeyValue);
    }


    private static IQueryable<FundingSource> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.FundingSources
            .Include(x => x.FundingEventFundingSources)
            .Include(x => x.Organization)
            .ThenInclude(x => x.OrganizationType);
    }

    public static bool IsFundingSourceNameUnique(IEnumerable<FundingSource> fundingSources, string fundingSourceName, int currentFundingSourceID)
    {
        var fundingSource =
            fundingSources.SingleOrDefault(x => x.FundingSourceID != currentFundingSourceID && string.Equals(x.FundingSourceName, fundingSourceName, StringComparison.InvariantCultureIgnoreCase));
        return fundingSource == null;
    }
}