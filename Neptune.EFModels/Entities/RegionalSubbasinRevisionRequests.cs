using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class RegionalSubbasinRevisionRequests
{
    public static IQueryable<RegionalSubbasinRevisionRequest> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.RegionalSubbasinRevisionRequests
                .Include(x => x.TreatmentBMP)
                .ThenInclude(x => x.TreatmentBMPType)
                .Include(x => x.RequestPerson)
                .Include(x => x.ClosedByPerson)
            ;
    }

    public static RegionalSubbasinRevisionRequest GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int regionalSubbasinRevisionRequestID)
    {
        var regionalSubbasinRevisionRequest = GetImpl(dbContext)
            .SingleOrDefault(x => x.RegionalSubbasinRevisionRequestID == regionalSubbasinRevisionRequestID);
        Check.RequireNotNull(regionalSubbasinRevisionRequest,
            $"RegionalSubbasinRevisionRequest with ID {regionalSubbasinRevisionRequestID} not found!");
        return regionalSubbasinRevisionRequest;
    }

    public static RegionalSubbasinRevisionRequest GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        RegionalSubbasinRevisionRequestPrimaryKey regionalSubbasinRevisionRequestPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, regionalSubbasinRevisionRequestPrimaryKey.PrimaryKeyValue);
    }

    public static RegionalSubbasinRevisionRequest GetByID(NeptuneDbContext dbContext, int regionalSubbasinRevisionRequestID)
    {
        var regionalSubbasinRevisionRequest = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.RegionalSubbasinRevisionRequestID == regionalSubbasinRevisionRequestID);
        Check.RequireNotNull(regionalSubbasinRevisionRequest,
            $"RegionalSubbasinRevisionRequest with ID {regionalSubbasinRevisionRequestID} not found!");
        return regionalSubbasinRevisionRequest;
    }

    public static RegionalSubbasinRevisionRequest GetByID(NeptuneDbContext dbContext,
        RegionalSubbasinRevisionRequestPrimaryKey regionalSubbasinRevisionRequestPrimaryKey)
    {
        return GetByID(dbContext, regionalSubbasinRevisionRequestPrimaryKey.PrimaryKeyValue);
    }

    public static List<RegionalSubbasinRevisionRequest> List(NeptuneDbContext dbContext, Person person)
    {
        return GetImpl(dbContext).AsNoTracking().ToList().Where(x => x.TreatmentBMP.CanView(person))
            .OrderByDescending(x => x.RequestDate).ToList();
    }

    public static List<RegionalSubbasinRevisionRequest> ListByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).AsNoTracking()
            .Where(x => x.TreatmentBMPID == treatmentBMPID).OrderByDescending(x => x.RegionalSubbasinRevisionRequestID).ToList();
    }

    public static List<RegionalSubbasinRevisionRequest> ListByRegionalSubbasinRevisionRequestIDList(NeptuneDbContext dbContext, List<int> regionalSubbasinRevisionRequestIDList)
    {
        return GetImpl(dbContext).AsNoTracking()
            .Where(x => regionalSubbasinRevisionRequestIDList.Contains(x.RegionalSubbasinRevisionRequestID)).ToList();
    }

    public static List<RegionalSubbasinRevisionRequest> ListByRegionalSubbasinRevisionRequestIDListWithChangeTracking(NeptuneDbContext dbContext, List<int> regionalSubbasinRevisionRequestIDList)
    {
        return GetImpl(dbContext).Where(x => regionalSubbasinRevisionRequestIDList.Contains(x.RegionalSubbasinRevisionRequestID)).ToList();
    }
}