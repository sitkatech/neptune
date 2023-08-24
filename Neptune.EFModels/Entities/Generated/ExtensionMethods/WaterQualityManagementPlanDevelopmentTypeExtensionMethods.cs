//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDevelopmentType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanDevelopmentTypeExtensionMethods
    {
        public static WaterQualityManagementPlanDevelopmentTypeDto AsDto(this WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentType)
        {
            var waterQualityManagementPlanDevelopmentTypeDto = new WaterQualityManagementPlanDevelopmentTypeDto()
            {
                WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeID,
                WaterQualityManagementPlanDevelopmentTypeName = waterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeName,
                WaterQualityManagementPlanDevelopmentTypeDisplayName = waterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeDisplayName
            };
            DoCustomMappings(waterQualityManagementPlanDevelopmentType, waterQualityManagementPlanDevelopmentTypeDto);
            return waterQualityManagementPlanDevelopmentTypeDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentType, WaterQualityManagementPlanDevelopmentTypeDto waterQualityManagementPlanDevelopmentTypeDto);

        public static WaterQualityManagementPlanDevelopmentTypeSimpleDto AsSimpleDto(this WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentType)
        {
            var waterQualityManagementPlanDevelopmentTypeSimpleDto = new WaterQualityManagementPlanDevelopmentTypeSimpleDto()
            {
                WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeID,
                WaterQualityManagementPlanDevelopmentTypeName = waterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeName,
                WaterQualityManagementPlanDevelopmentTypeDisplayName = waterQualityManagementPlanDevelopmentType.WaterQualityManagementPlanDevelopmentTypeDisplayName
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanDevelopmentType, waterQualityManagementPlanDevelopmentTypeSimpleDto);
            return waterQualityManagementPlanDevelopmentTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentType, WaterQualityManagementPlanDevelopmentTypeSimpleDto waterQualityManagementPlanDevelopmentTypeSimpleDto);
    }
}