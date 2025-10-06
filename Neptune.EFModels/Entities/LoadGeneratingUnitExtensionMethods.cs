using Neptune.Common;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class LoadGeneratingUnitExtensionMethods
    {
        public static LoadGeneratingUnitDto AsDto(this LoadGeneratingUnit entity)
        {
            return new LoadGeneratingUnitDto
            {
                LoadGeneratingUnitID = entity.LoadGeneratingUnitID,
                ModelBasinID = entity.ModelBasinID,
                RegionalSubbasinID = entity.RegionalSubbasinID,
                RegionalSubbasinName = entity.RegionalSubbasin?.GetDisplayName(),
                DelineationID = entity.DelineationID,
                TreatmentBMPID = entity.Delineation?.TreatmentBMPID,
                TreatmentBMPName = entity.Delineation?.TreatmentBMP?.TreatmentBMPName,
                WaterQualityManagementPlanID = entity.WaterQualityManagementPlanID,
                WaterQualityManagementPlanName = entity.WaterQualityManagementPlan?.WaterQualityManagementPlanName,
                IsEmptyResponseFromHRUService = entity.IsEmptyResponseFromHRUService,
                DateHRURequested = entity.DateHRURequested,
                HRULogID = entity.HRULogID,
                Area = entity.LoadGeneratingUnitGeometry.Area * Constants.SquareMetersToAcres
            };
        }

        public static LoadGeneratingUnitGridDto AsGridDto(this LoadGeneratingUnit entity)
        {
            return new LoadGeneratingUnitGridDto
            {
                LoadGeneratingUnitID = entity.LoadGeneratingUnitID,
                ModelBasinID = entity.ModelBasinID,
                RegionalSubbasinID = entity.RegionalSubbasinID,
                RegionalSubbasinName = entity.RegionalSubbasin?.GetDisplayName(),
                TreatmentBMPID = entity.Delineation?.TreatmentBMPID,
                TreatmentBMPName = entity.Delineation?.TreatmentBMP?.TreatmentBMPName,
                WaterQualityManagementPlanID = entity.WaterQualityManagementPlanID,
                WaterQualityManagementPlanName = entity.WaterQualityManagementPlan?.WaterQualityManagementPlanName,
                IsEmptyResponseFromHRUService = entity.IsEmptyResponseFromHRUService,
                DateHRURequested = entity.DateHRURequested,
            };
        }

        public static void UpdateFromDto(this LoadGeneratingUnit entity, LoadGeneratingUnitDto dto)
        {
            entity.ModelBasinID = dto.ModelBasinID;
            entity.RegionalSubbasinID = dto.RegionalSubbasinID;
            entity.DelineationID = dto.DelineationID;
            entity.WaterQualityManagementPlanID = dto.WaterQualityManagementPlanID;
            entity.IsEmptyResponseFromHRUService = dto.IsEmptyResponseFromHRUService;
            entity.DateHRURequested = dto.DateHRURequested;
            entity.HRULogID = dto.HRULogID;
        }
    }
}
