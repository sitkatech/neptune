//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanBoundary]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanBoundaryExtensionMethods
    {
        public static WaterQualityManagementPlanBoundaryDto AsDto(this WaterQualityManagementPlanBoundary waterQualityManagementPlanBoundary)
        {
            var waterQualityManagementPlanBoundaryDto = new WaterQualityManagementPlanBoundaryDto()
            {
                WaterQualityManagementPlanGeometryID = waterQualityManagementPlanBoundary.WaterQualityManagementPlanGeometryID,
                WaterQualityManagementPlan = waterQualityManagementPlanBoundary.WaterQualityManagementPlan.AsDto()
            };
            DoCustomMappings(waterQualityManagementPlanBoundary, waterQualityManagementPlanBoundaryDto);
            return waterQualityManagementPlanBoundaryDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanBoundary waterQualityManagementPlanBoundary, WaterQualityManagementPlanBoundaryDto waterQualityManagementPlanBoundaryDto);

        public static WaterQualityManagementPlanBoundarySimpleDto AsSimpleDto(this WaterQualityManagementPlanBoundary waterQualityManagementPlanBoundary)
        {
            var waterQualityManagementPlanBoundarySimpleDto = new WaterQualityManagementPlanBoundarySimpleDto()
            {
                WaterQualityManagementPlanGeometryID = waterQualityManagementPlanBoundary.WaterQualityManagementPlanGeometryID,
                WaterQualityManagementPlanID = waterQualityManagementPlanBoundary.WaterQualityManagementPlanID
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanBoundary, waterQualityManagementPlanBoundarySimpleDto);
            return waterQualityManagementPlanBoundarySimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanBoundary waterQualityManagementPlanBoundary, WaterQualityManagementPlanBoundarySimpleDto waterQualityManagementPlanBoundarySimpleDto);
    }
}