using Microsoft.EntityFrameworkCore;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class vHRUCharacteristics
{
    private static IQueryable<vHRUCharacteristic> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.vHRUCharacteristics.AsNoTracking();
    }
    public static List<vHRUCharacteristic> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).ToList();
    }

    public static List<vHRUCharacteristic> ListByWaterQualityManagementPlanID(NeptuneDbContext dbContext,
        int waterQualityManagementPlanID)
    {
        var waterQualityManagementPlan = WaterQualityManagementPlans.GetByID(dbContext, waterQualityManagementPlanID);
        if (waterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID == (int) WaterQualityManagementPlanModelingApproachEnum.Simplified)
        {
            return GetImpl(dbContext).Where(x =>
                x.WaterQualityManagementPlanID == waterQualityManagementPlanID).ToList();
        }

        var treatmentBMPIDs = waterQualityManagementPlan.TreatmentBMPs.Select(x => x.TreatmentBMPID).ToList();
        return GetImpl(dbContext).Where(x =>
            x.TreatmentBMPID.HasValue &&
            treatmentBMPIDs.Contains(x.TreatmentBMPID.Value)).ToList();
    }

    public static List<vHRUCharacteristic> ListByRegionalSubbasinID(NeptuneDbContext dbContext, int regionalSubbasinID)
    {
        return GetImpl(dbContext)
            .Where(x => x.RegionalSubbasinID == regionalSubbasinID).ToList();
    }

    public static List<vHRUCharacteristic> ListByLoadGeneratingUnitID(NeptuneDbContext dbContext,
        int loadGeneratingUnitID)
    {
        return GetImpl(dbContext).Where(x => x.LoadGeneratingUnitID == loadGeneratingUnitID).ToList();
    }

    public static List<vHRUCharacteristic> ListByTreatmentBMP(NeptuneDbContext dbContext, TreatmentBMP treatmentBMP, Delineation? delineation)
    {
        if (delineation == null)
        {
            return new List<vHRUCharacteristic>();
        }

        if (delineation.DelineationType == DelineationType.Centralized && treatmentBMP.TreatmentBMPType.TreatmentBMPModelingTypeID.HasValue)
        {
            if (!treatmentBMP.RegionalSubbasinID.HasValue)
            {
                return [];
            }
            var catchmentRegionalSubbasins = vRegionalSubbasinUpstreams.ListUpstreamRegionalBasinIDs(dbContext, treatmentBMP.RegionalSubbasinID.Value);

            return GetImpl(dbContext).Where(x =>
                x.RegionalSubbasinID != null &&
                catchmentRegionalSubbasins.Contains(x.RegionalSubbasinID.Value)).ToList();
        }

        return GetImpl(dbContext)
            .Where(x => x.TreatmentBMPID != null && x.TreatmentBMPID == treatmentBMP.TreatmentBMPID).ToList();
    }

    public static List<HRUCharacteristicDto> ListByTreatmentBMPAsDto(NeptuneDbContext dbContext, TreatmentBMP treatmentBMP, Delineation? delineation)
    {
        return ListByTreatmentBMP(dbContext, treatmentBMP, delineation).Select(x => x.AsDto()).ToList();
    }
}