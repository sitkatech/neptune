using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class FundingEvents
{
    private static IQueryable<FundingEvent> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.FundingEvents
                .Include(x => x.FundingEventFundingSources)
                .ThenInclude(x => x.FundingSource)
                .ThenInclude(x => x.Organization)
                .ThenInclude(x => x.OrganizationType)
            ;
    }

    public static List<FundingEvent> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().ToList().OrderBy(x => x.GetDisplayName()).ToList();
    }

    public static FundingEvent GetByIDWithChangeTracking(NeptuneDbContext dbContext, int fundingEventID)
    {
        var fundingEvent = GetImpl(dbContext)
            .SingleOrDefault(x => x.FundingEventID == fundingEventID);
        Check.RequireNotNull(fundingEvent, $"FundingEvent with ID {fundingEventID} not found!");
        return fundingEvent;
    }

    public static FundingEvent GetByIDWithChangeTracking(NeptuneDbContext dbContext, FundingEventPrimaryKey fundingEventPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, fundingEventPrimaryKey.PrimaryKeyValue);
    }

    public static FundingEvent GetByID(NeptuneDbContext dbContext, int fundingEventID)
    {
        var fundingEvent = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.FundingEventID == fundingEventID);
        Check.RequireNotNull(fundingEvent, $"FundingEvent with ID {fundingEventID} not found!");
        return fundingEvent;
    }

    public static FundingEvent GetByID(NeptuneDbContext dbContext, FundingEventPrimaryKey fundingEventPrimaryKey)
    {
        return GetByID(dbContext, fundingEventPrimaryKey.PrimaryKeyValue);
    }

    public static List<FundingEvent> ListByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.TreatmentBMPID == treatmentBMPID).ToList().OrderBy(x => x.GetDisplayName()).ToList();
    }
}