using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPModelingAttributes
{
    private static IQueryable<TreatmentBMPModelingAttribute> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.TreatmentBMPModelingAttributes
            ;
    }

    public static TreatmentBMPModelingAttribute GetByTreatmentBMPIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        var treatmentBMPModelingAttribute = GetImpl(dbContext)
            .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
        Check.RequireNotNull(treatmentBMPModelingAttribute, $"TreatmentBMPModelingAttribute with ID {treatmentBMPID} not found!");
        return treatmentBMPModelingAttribute;
    }

    public static TreatmentBMPModelingAttribute GetByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        var treatmentBMPModelingAttribute = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPID == treatmentBMPID);
        Check.RequireNotNull(treatmentBMPModelingAttribute, $"TreatmentBMPModelingAttribute with ID {treatmentBMPID} not found!");
        return treatmentBMPModelingAttribute;
    }
}