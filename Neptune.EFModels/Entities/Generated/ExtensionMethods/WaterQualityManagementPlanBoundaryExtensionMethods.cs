//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanBoundary]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanBoundaryExtensionMethods
    {
        public static WaterQualityManagementPlanBoundarySimpleDto AsSimpleDto(this WaterQualityManagementPlanBoundary waterQualityManagementPlanBoundary)
        {
            var dto = new WaterQualityManagementPlanBoundarySimpleDto()
            {
                WaterQualityManagementPlanGeometryID = waterQualityManagementPlanBoundary.WaterQualityManagementPlanGeometryID,
                WaterQualityManagementPlanID = waterQualityManagementPlanBoundary.WaterQualityManagementPlanID
            };
            return dto;
        }
    }
}