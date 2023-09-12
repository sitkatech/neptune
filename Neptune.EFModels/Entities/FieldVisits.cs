using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class FieldVisits
{
    public static IQueryable<FieldVisit> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.FieldVisits
            .Include(x => x.TreatmentBMP)
            .ThenInclude(x => x.TreatmentBMPType)
            .ThenInclude(x => x.TreatmentBMPTypeCustomAttributeTypes)
            .ThenInclude(x => x.CustomAttributeType)
            .Include(x => x.MaintenanceRecord)
            .Include(x => x.TreatmentBMPAssessments)
                .ThenInclude(x => x.TreatmentBMPAssessmentPhotos).ThenInclude(x => x.FileResource)
            .Include(x => x.TreatmentBMP)
                .ThenInclude(x => x.CustomAttributes)
                    .ThenInclude(x => x.CustomAttributeValues)
            .Include(x => x.TreatmentBMP)
                .ThenInclude(x => x.CustomAttributes)
                    .ThenInclude(x => x.CustomAttributeType);
    }

    public static FieldVisit GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int fieldVisitID)
    {
        var fieldVisit = GetImpl(dbContext)
            .SingleOrDefault(x => x.FieldVisitID == fieldVisitID);
        Check.RequireNotNull(fieldVisit,
            $"FieldVisit with ID {fieldVisitID} not found!");
        return fieldVisit;
    }

    public static FieldVisit GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        FieldVisitPrimaryKey fieldVisitPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, fieldVisitPrimaryKey.PrimaryKeyValue);
    }

    public static FieldVisit GetByID(NeptuneDbContext dbContext, int fieldVisitID)
    {
        var fieldVisit = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.FieldVisitID == fieldVisitID);
        Check.RequireNotNull(fieldVisit,
            $"FieldVisit with ID {fieldVisitID} not found!");
        return fieldVisit;
    }

    public static FieldVisit GetByID(NeptuneDbContext dbContext,
        FieldVisitPrimaryKey fieldVisitPrimaryKey)
    {
        return GetByID(dbContext, fieldVisitPrimaryKey.PrimaryKeyValue);
    }

    public static List<FieldVisit> ListByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).AsNoTracking()
            .Where(x => x.TreatmentBMPID == treatmentBMPID).ToList();
    }

    public static FieldVisit? GetInProgressForTreatmentBMPIfAny(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).SingleOrDefault(x =>
            x.TreatmentBMPID == treatmentBMPID &&
            x.FieldVisitStatusID == FieldVisitStatus.InProgress.FieldVisitStatusID);
    }
}