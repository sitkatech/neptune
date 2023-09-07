
using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPAssessmentObservationTypes
{
    public static List<TreatmentBMPAssessmentObservationType> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().OrderBy(x => x.TreatmentBMPAssessmentObservationTypeName).ToList();
    }

    public static TreatmentBMPAssessmentObservationType GetByIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPAssessmentObservationTypeID)
    {
        var treatmentBMPAssessmentObservationType = GetImpl(dbContext)
            .SingleOrDefault(x => x.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationTypeID);
        Check.RequireNotNull(treatmentBMPAssessmentObservationType, $"TreatmentBMPAssessmentObservationType with ID {treatmentBMPAssessmentObservationTypeID} not found!");
        return treatmentBMPAssessmentObservationType;
    }

    public static TreatmentBMPAssessmentObservationType GetByIDWithChangeTracking(NeptuneDbContext dbContext, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, treatmentBMPAssessmentObservationTypePrimaryKey.PrimaryKeyValue);
    }

    public static TreatmentBMPAssessmentObservationType GetByID(NeptuneDbContext dbContext, int treatmentBMPAssessmentObservationTypeID)
    {
        var treatmentBMPAssessmentObservationType = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationTypeID);
        Check.RequireNotNull(treatmentBMPAssessmentObservationType, $"TreatmentBMPAssessmentObservationType with ID {treatmentBMPAssessmentObservationTypeID} not found!");
        return treatmentBMPAssessmentObservationType;
    }

    public static TreatmentBMPAssessmentObservationType GetByID(NeptuneDbContext dbContext, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
    {
        return GetByID(dbContext, treatmentBMPAssessmentObservationTypePrimaryKey.PrimaryKeyValue);
    }


    private static IQueryable<TreatmentBMPAssessmentObservationType> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.TreatmentBMPAssessmentObservationTypes;
    }
}