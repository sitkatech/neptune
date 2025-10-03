﻿using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Models.DataTransferObjects;

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

    public static async Task<List<TreatmentBMPBenchmarkAndThresholdDto>> ListByTreatmentBMPIDAsDtoAsync(NeptuneDbContext dbContext, int treatmentBMPID)
    {
        var entities = await dbContext.TreatmentBMPBenchmarkAndThresholds
            .Where(x => x.TreatmentBMPID == treatmentBMPID)
            .ToListAsync();
        return entities.Select(x => x.AsDto()).ToList();
    }

    public static async Task<TreatmentBMPBenchmarkAndThresholdDto?> GetByIDAsync(NeptuneDbContext dbContext, int treatmentBMPBenchmarkAndThresholdID)
    {
        var entity = await dbContext.TreatmentBMPBenchmarkAndThresholds
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TreatmentBMPBenchmarkAndThresholdID == treatmentBMPBenchmarkAndThresholdID);
        return entity?.AsDto();
    }

    public static async Task<TreatmentBMPBenchmarkAndThresholdDto> CreateAsync(NeptuneDbContext dbContext, int treatmentBMPID, TreatmentBMPBenchmarkAndThresholdUpsertDto dto)
    {
        var entity = dto.AsEntity(treatmentBMPID);
        dbContext.TreatmentBMPBenchmarkAndThresholds.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.AsDto();
    }

    public static async Task<TreatmentBMPBenchmarkAndThresholdDto?> UpdateAsync(NeptuneDbContext dbContext, int id, TreatmentBMPBenchmarkAndThresholdUpsertDto dto)
    {
        var entity = await dbContext.TreatmentBMPBenchmarkAndThresholds.FindAsync(id);
        if (entity == null)
        {
            return null;
        }
        entity.UpdateFromUpsertDto(dto);
        await dbContext.SaveChangesAsync();
        return entity.AsDto();
    }

    public static async Task<bool> DeleteAsync(NeptuneDbContext dbContext, int id)
    {
        var deletedCount = await dbContext.TreatmentBMPBenchmarkAndThresholds
            .Where(x => x.TreatmentBMPBenchmarkAndThresholdID == id)
            .ExecuteDeleteAsync();
        if (deletedCount == 0)
        {
            return false;
        }
        return true;
    }
}