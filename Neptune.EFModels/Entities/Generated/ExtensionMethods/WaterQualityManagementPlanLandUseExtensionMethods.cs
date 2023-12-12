//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanLandUse]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanLandUseExtensionMethods
    {

        public static WaterQualityManagementPlanLandUseSimpleDto AsSimpleDto(this WaterQualityManagementPlanLandUse waterQualityManagementPlanLandUse)
        {
            var waterQualityManagementPlanLandUseSimpleDto = new WaterQualityManagementPlanLandUseSimpleDto()
            {
                WaterQualityManagementPlanLandUseID = waterQualityManagementPlanLandUse.WaterQualityManagementPlanLandUseID,
                WaterQualityManagementPlanLandUseName = waterQualityManagementPlanLandUse.WaterQualityManagementPlanLandUseName,
                WaterQualityManagementPlanLandUseDisplayName = waterQualityManagementPlanLandUse.WaterQualityManagementPlanLandUseDisplayName,
                SortOrder = waterQualityManagementPlanLandUse.SortOrder
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanLandUse, waterQualityManagementPlanLandUseSimpleDto);
            return waterQualityManagementPlanLandUseSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanLandUse waterQualityManagementPlanLandUse, WaterQualityManagementPlanLandUseSimpleDto waterQualityManagementPlanLandUseSimpleDto);
    }
}