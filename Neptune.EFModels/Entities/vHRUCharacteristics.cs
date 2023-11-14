using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class vHRUCharacteristics
{
    public static List<vHRUCharacteristic> List(NeptuneDbContext dbContext)
    {
        return dbContext.vHRUCharacteristics.AsNoTracking().ToList();
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

    public static List<vHRUCharacteristic> ListByTreatmentBMP(NeptuneDbContext dbContext, TreatmentBMP treatmentBMP, Delineation? delineation)
    {
        if (delineation == null)
        {
            return new List<vHRUCharacteristic>();
        }

        if (delineation.DelineationType == DelineationType.Centralized && !treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID.HasValue)
        {
            if (!treatmentBMP.RegionalSubbasinID.HasValue)
            {
                return new List<vHRUCharacteristic>(Enumerable.Empty<vHRUCharacteristic>());
            }
            var catchmentRegionalSubbasins = vRegionalSubbasinUpstreams.ListUpstreamRegionalBasinIDs(dbContext, treatmentBMP.RegionalSubbasinID.Value);

            return dbContext.vHRUCharacteristics.AsNoTracking().Where(x =>
                x.RegionalSubbasinID != null &&
                catchmentRegionalSubbasins.Contains(x.RegionalSubbasinID.Value)).ToList();
        }

        return dbContext.vHRUCharacteristics
            .AsNoTracking()
            .Where(x => x.TreatmentBMPID != null && x.TreatmentBMPID == treatmentBMP.TreatmentBMPID).ToList();
    }
}