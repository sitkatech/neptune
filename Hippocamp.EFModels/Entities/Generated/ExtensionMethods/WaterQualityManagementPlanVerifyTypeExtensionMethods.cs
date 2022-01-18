//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifyTypeExtensionMethods
    {
        public static WaterQualityManagementPlanVerifyTypeDto AsDto(this WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyType)
        {
            var waterQualityManagementPlanVerifyTypeDto = new WaterQualityManagementPlanVerifyTypeDto()
            {
                WaterQualityManagementPlanVerifyTypeID = waterQualityManagementPlanVerifyType.WaterQualityManagementPlanVerifyTypeID,
                WaterQualityManagementPlanVerifyTypeName = waterQualityManagementPlanVerifyType.WaterQualityManagementPlanVerifyTypeName
            };
            DoCustomMappings(waterQualityManagementPlanVerifyType, waterQualityManagementPlanVerifyTypeDto);
            return waterQualityManagementPlanVerifyTypeDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyType, WaterQualityManagementPlanVerifyTypeDto waterQualityManagementPlanVerifyTypeDto);

        public static WaterQualityManagementPlanVerifyTypeSimpleDto AsSimpleDto(this WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyType)
        {
            var waterQualityManagementPlanVerifyTypeSimpleDto = new WaterQualityManagementPlanVerifyTypeSimpleDto()
            {
                WaterQualityManagementPlanVerifyTypeID = waterQualityManagementPlanVerifyType.WaterQualityManagementPlanVerifyTypeID,
                WaterQualityManagementPlanVerifyTypeName = waterQualityManagementPlanVerifyType.WaterQualityManagementPlanVerifyTypeName
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanVerifyType, waterQualityManagementPlanVerifyTypeSimpleDto);
            return waterQualityManagementPlanVerifyTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyType, WaterQualityManagementPlanVerifyTypeSimpleDto waterQualityManagementPlanVerifyTypeSimpleDto);
    }
}