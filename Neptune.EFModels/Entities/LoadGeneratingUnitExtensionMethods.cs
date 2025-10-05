using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class LoadGeneratingUnitExtensionMethods
    {
        public static LoadGeneratingUnitDto ToDto(this LoadGeneratingUnit entity)
        {
            return new LoadGeneratingUnitDto
            {
                LoadGeneratingUnitID = entity.LoadGeneratingUnitID,
                ModelBasinID = entity.ModelBasinID,
                RegionalSubbasinID = entity.RegionalSubbasinID,
                DelineationID = entity.DelineationID,
                WaterQualityManagementPlanID = entity.WaterQualityManagementPlanID,
                IsEmptyResponseFromHRUService = entity.IsEmptyResponseFromHRUService,
                DateHRURequested = entity.DateHRURequested,
                HRULogID = entity.HRULogID,
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
