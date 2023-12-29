using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPTypeAssessmentObservationTypes
{
    public static IQueryable<TreatmentBMPTypeAssessmentObservationType> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.TreatmentBMPTypeAssessmentObservationTypes
                .Include(x => x.TreatmentBMPType)
                .Include(x => x.TreatmentBMPAssessmentObservationType)
            ;
    }

    public static TreatmentBMPTypeAssessmentObservationType GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int treatmentBMPTypeAssessmentObservationTypeID)
    {
        var treatmentBMPTypeAssessmentObservationType = GetImpl(dbContext)
            .SingleOrDefault(x => x.TreatmentBMPTypeAssessmentObservationTypeID == treatmentBMPTypeAssessmentObservationTypeID);
        Check.RequireNotNull(treatmentBMPTypeAssessmentObservationType,
            $"TreatmentBMPTypeAssessmentObservationType with ID {treatmentBMPTypeAssessmentObservationTypeID} not found!");
        return treatmentBMPTypeAssessmentObservationType;
    }

    public static TreatmentBMPTypeAssessmentObservationType GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        TreatmentBMPTypeAssessmentObservationTypePrimaryKey treatmentBMPTypeAssessmentObservationTypePrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, treatmentBMPTypeAssessmentObservationTypePrimaryKey.PrimaryKeyValue);
    }

    public static TreatmentBMPTypeAssessmentObservationType GetByID(NeptuneDbContext dbContext, int treatmentBMPTypeAssessmentObservationTypeID)
    {
        var treatmentBMPTypeAssessmentObservationType = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPTypeAssessmentObservationTypeID == treatmentBMPTypeAssessmentObservationTypeID);
        Check.RequireNotNull(treatmentBMPTypeAssessmentObservationType,
            $"TreatmentBMPTypeAssessmentObservationType with ID {treatmentBMPTypeAssessmentObservationTypeID} not found!");
        return treatmentBMPTypeAssessmentObservationType;
    }

    public static TreatmentBMPTypeAssessmentObservationType GetByID(NeptuneDbContext dbContext,
        TreatmentBMPTypeAssessmentObservationTypePrimaryKey treatmentBMPTypeAssessmentObservationTypePrimaryKey)
    {
        return GetByID(dbContext, treatmentBMPTypeAssessmentObservationTypePrimaryKey.PrimaryKeyValue);
    }

    public static List<TreatmentBMPTypeAssessmentObservationType> ListByTreatmentBMPTypeID(NeptuneDbContext dbContext, int treatmentBMPTypeID)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.TreatmentBMPTypeID == treatmentBMPTypeID).OrderBy(x => x.TreatmentBMPTypeAssessmentObservationTypeID).ToList();
    }

    public static List<TreatmentBMPTypeAssessmentObservationType> ListByTreatmentBMPTypeIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPTypeID)
    {
        return GetImpl(dbContext).Where(x => x.TreatmentBMPTypeID == treatmentBMPTypeID).OrderBy(x => x.TreatmentBMPTypeAssessmentObservationTypeID).ToList();
    }
}