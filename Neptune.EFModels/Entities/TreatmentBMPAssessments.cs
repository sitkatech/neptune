using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPAssessments
{
    private static IQueryable<TreatmentBMPAssessment> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.TreatmentBMPAssessments
            .Include(x => x.FieldVisit)
            .ThenInclude(x => x.PerformedByPerson)
            .Include(x => x.TreatmentBMP)
            .ThenInclude(x => x.TreatmentBMPType)
            .Include(x => x.TreatmentBMP)
            .ThenInclude(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            .Include(x => x.TreatmentBMPType)
            .ThenInclude(x => x.TreatmentBMPTypeAssessmentObservationTypes)
            .ThenInclude(x => x.TreatmentBMPAssessmentObservationType)
            .Include(x => x.TreatmentBMPObservations)
            .ThenInclude(x => x.TreatmentBMPAssessmentObservationType);
    }

    public static TreatmentBMPAssessment GetByIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPAssessmentID)
    {
        var treatmentBMPAssessment = GetImpl(dbContext)
            .SingleOrDefault(x => x.TreatmentBMPAssessmentID == treatmentBMPAssessmentID);
        Check.RequireNotNull(treatmentBMPAssessment, $"TreatmentBMPAssessment with ID {treatmentBMPAssessmentID} not found!");
        return treatmentBMPAssessment;
    }

    public static TreatmentBMPAssessment GetByIDWithChangeTracking(NeptuneDbContext dbContext, TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, treatmentBMPAssessmentPrimaryKey.PrimaryKeyValue);
    }

    public static TreatmentBMPAssessment GetByID(NeptuneDbContext dbContext, int treatmentBMPAssessmentID)
    {
        var treatmentBMPAssessment = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPAssessmentID == treatmentBMPAssessmentID);
        Check.RequireNotNull(treatmentBMPAssessment, $"TreatmentBMPAssessment with ID {treatmentBMPAssessmentID} not found!");
        return treatmentBMPAssessment;
    }

    public static TreatmentBMPAssessment GetByID(NeptuneDbContext dbContext, TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
    {
        return GetByID(dbContext, treatmentBMPAssessmentPrimaryKey.PrimaryKeyValue);
    }

    public static List<TreatmentBMPAssessment> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().ToList();
    }

    public static TreatmentBMPAssessment GetByIDForFeatureContextCheck(NeptuneDbContext dbContext, int treatmentBMPAssessmentID)
    {
        var treatmentBMPAssessment = dbContext.TreatmentBMPAssessments
            .Include(x => x.TreatmentBMP)
            .ThenInclude(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            .AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPAssessmentID == treatmentBMPAssessmentID);
        Check.RequireNotNull(treatmentBMPAssessment, $"TreatmentBMPAssessment with ID {treatmentBMPAssessmentID} not found!");
        return treatmentBMPAssessment;
    }
}