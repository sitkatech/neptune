using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class TreatmentBMPBenchmarkAndThresholds
{
    private static IQueryable<TreatmentBMPBenchmarkAndThreshold> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.TreatmentBMPBenchmarkAndThresholds;
    }

    public static TreatmentBMPBenchmarkAndThreshold GetByIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPBenchmarkAndThresholdID)
    {
        var treatmentBMPBenchmarkAndThreshold = GetImpl(dbContext)
            .SingleOrDefault(x => x.TreatmentBMPBenchmarkAndThresholdID == treatmentBMPBenchmarkAndThresholdID);
        Check.RequireNotNull(treatmentBMPBenchmarkAndThreshold, $"TreatmentBMPBenchmarkAndThreshold with ID {treatmentBMPBenchmarkAndThresholdID} not found!");
        return treatmentBMPBenchmarkAndThreshold;
    }

    public static TreatmentBMPBenchmarkAndThreshold GetByIDWithChangeTracking(NeptuneDbContext dbContext, TreatmentBMPBenchmarkAndThresholdPrimaryKey treatmentBMPBenchmarkAndThresholdPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, treatmentBMPBenchmarkAndThresholdPrimaryKey.PrimaryKeyValue);
    }

    public static TreatmentBMPBenchmarkAndThreshold GetByID(NeptuneDbContext dbContext, int treatmentBMPBenchmarkAndThresholdID)
    {
        var treatmentBMPBenchmarkAndThreshold = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.TreatmentBMPBenchmarkAndThresholdID == treatmentBMPBenchmarkAndThresholdID);
        Check.RequireNotNull(treatmentBMPBenchmarkAndThreshold, $"TreatmentBMPBenchmarkAndThreshold with ID {treatmentBMPBenchmarkAndThresholdID} not found!");
        return treatmentBMPBenchmarkAndThreshold;
    }

    public static TreatmentBMPBenchmarkAndThreshold GetByID(NeptuneDbContext dbContext, TreatmentBMPBenchmarkAndThresholdPrimaryKey treatmentBMPBenchmarkAndThresholdPrimaryKey)
    {
        return GetByID(dbContext, treatmentBMPBenchmarkAndThresholdPrimaryKey.PrimaryKeyValue);
    }

    public static List<TreatmentBMPBenchmarkAndThreshold> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().ToList();
    }

    public static List<TreatmentBMPBenchmarkAndThreshold> ListByTreatmentBMPID(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => x.TreatmentBMPID == treatmentBMPID).ToList();
    }

    public static List<TreatmentBMPBenchmarkAndThreshold> ListByTreatmentBMPIDWithChangeTracking(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        return GetImpl(dbContext).Where(x => x.TreatmentBMPID == treatmentBMPID).ToList();
    }
}