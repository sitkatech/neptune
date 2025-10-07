﻿using Microsoft.EntityFrameworkCore;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class vHRUCharacteristics
{
    public static Task<List<vHRUCharacteristic>> List(NeptuneDbContext dbContext)
    {
        return dbContext.vHRUCharacteristics.AsNoTracking().ToListAsync();
    }

    public static List<vHRUCharacteristic> ListByWaterQualityManagementPlanID(NeptuneDbContext dbContext,
        int waterQualityManagementPlanID)
    {
        var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(dbContext, waterQualityManagementPlanID);
        if (waterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID == (int) WaterQualityManagementPlanModelingApproachEnum.Simplified)
        {
            return dbContext.vHRUCharacteristics.AsNoTracking().Where(x =>
                x.WaterQualityManagementPlanID == waterQualityManagementPlanID).ToList();
        }

        var treatmentBMPIDs = waterQualityManagementPlan.TreatmentBMPs.Select(x => x.TreatmentBMPID).ToList();
        return dbContext.vHRUCharacteristics.AsNoTracking().Where(x =>
            x.TreatmentBMPID.HasValue &&
            treatmentBMPIDs.Contains(x.TreatmentBMPID.Value)).ToList();
    }

    public static List<vHRUCharacteristic> ListByRegionalSubbasinID(NeptuneDbContext dbContext, int regionalSubbasinID)
    {
        return dbContext.vHRUCharacteristics.AsNoTracking()
            .Where(x => x.RegionalSubbasinID == regionalSubbasinID).ToList();
    }

    public static List<vHRUCharacteristic> ListByLoadGeneratingUnitID(NeptuneDbContext dbContext,
        int loadGeneratingUnitID)
    {
        return dbContext.vHRUCharacteristics.AsNoTracking().Where(x => x.LoadGeneratingUnitID == loadGeneratingUnitID).ToList();
    }

    public static async Task<List<vHRUCharacteristic>> ListByTreatmentBMP(NeptuneDbContext dbContext, TreatmentBMP treatmentBMP, Delineation? delineation)
    {
        if (delineation == null)
        {
            return [];
        }

        if (delineation.DelineationType == DelineationType.Centralized && treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID.HasValue)
        {
            if (!treatmentBMP.RegionalSubbasinID.HasValue)
            {
                return [];
            }
            var catchmentRegionalSubbasins = vRegionalSubbasinUpstreams.ListUpstreamRegionalBasinIDs(dbContext, treatmentBMP.RegionalSubbasinID.Value);

            return await dbContext.vHRUCharacteristics.AsNoTracking().Where(x =>
                x.RegionalSubbasinID != null &&
                catchmentRegionalSubbasins.Contains(x.RegionalSubbasinID.Value)).ToListAsync();
        }

        return await dbContext.vHRUCharacteristics.AsNoTracking()
            .Where(x => x.TreatmentBMPID != null && x.TreatmentBMPID == treatmentBMP.TreatmentBMPID).ToListAsync();
    }

    public static async Task<List<HRUCharacteristicDto>> ListByTreatmentBMPAsDtoAsync(NeptuneDbContext dbContext, TreatmentBMP treatmentBMP, Delineation? delineation)
    {
        var entities = await ListByTreatmentBMP(dbContext, treatmentBMP, delineation);
        return entities.Select(x => x.AsDto()).ToList();
    }

    public static async Task<List<HRUCharacteristicDto>> ListAsDtoAsync(NeptuneDbContext dbContext)
    {
        var entities = await dbContext.vHRUCharacteristics.AsNoTracking().ToListAsync();
        return entities.Select(x => x.AsDto()).ToList();
    }

    public static async Task<List<HRUCharacteristicDto>> ListByLoadGeneratingUnitAsGridDtoAsync(NeptuneDbContext dbContext, int loadGeneratingUnitID)
    {
        var entities = await dbContext.vHRUCharacteristics.AsNoTracking().Where(x => x.LoadGeneratingUnitID == loadGeneratingUnitID).ToListAsync();
        return entities.Select(x => x.AsDto()).ToList();
    }

    public static async Task<List<HRUCharacteristicDto>> ListByRegionalSubbasinAsGridDtoAsync(NeptuneDbContext dbContext, int regionalSubbasinID)
    {
        var entities = await dbContext.vHRUCharacteristics.AsNoTracking().Where(x => x.RegionalSubbasinID == regionalSubbasinID).ToListAsync();
        return entities.Select(x => x.AsDto()).ToList();
    }
}