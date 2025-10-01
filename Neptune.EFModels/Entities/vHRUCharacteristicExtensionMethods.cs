using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class vHRUCharacteristicExtensionMethods
{
    public static HRUCharacteristicDto AsDto(this vHRUCharacteristic entity)
    {
        return new HRUCharacteristicDto
        {
            HRUCharacteristicID = entity.HRUCharacteristicID,
            LastUpdated = entity.LastUpdated,
            LoadGeneratingUnitID = entity.LoadGeneratingUnitID,
            HRUEntity = entity.HRUEntity,
            TreatmentBMPID = entity.TreatmentBMPID,
            TreatmentBMPName = entity.TreatmentBMPName,
            WaterQualityManagementPlanID = entity.WaterQualityManagementPlanID,
            WaterQualityManagementPlanName = entity.WaterQualityManagementPlanName,
            RegionalSubbasinID = entity.RegionalSubbasinID,
            RegionalSubbasinName = entity.RegionalSubbasinName,
            HydrologicSoilGroup = entity.HydrologicSoilGroup,
            SlopePercentage = entity.SlopePercentage,
            Area = entity.Area,
            ImperviousAcres = entity.ImperviousAcres,
            HRUCharacteristicLandUseCodeID = entity.HRUCharacteristicLandUseCodeID,
            HRUCharacteristicLandUseCodeDisplayName = entity.HRUCharacteristicLandUseCodeDisplayName,
            BaselineImperviousAcres = entity.BaselineImperviousAcres,
            BaselineHRUCharacteristicLandUseCodeID = entity.BaselineHRUCharacteristicLandUseCodeID,
            BaselineHRUCharacteristicLandUseCodeDisplayName = entity.BaselineHRUCharacteristicLandUseCodeDisplayName
        };
    }
}
