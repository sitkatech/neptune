//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVisitStatus]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVisitStatusExtensionMethods
    {
        public static WaterQualityManagementPlanVisitStatusSimpleDto AsSimpleDto(this WaterQualityManagementPlanVisitStatus waterQualityManagementPlanVisitStatus)
        {
            var dto = new WaterQualityManagementPlanVisitStatusSimpleDto()
            {
                WaterQualityManagementPlanVisitStatusID = waterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVisitStatusID,
                WaterQualityManagementPlanVisitStatusName = waterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVisitStatusName,
                WaterQualityManagementPlanVisitStatusDisplayName = waterQualityManagementPlanVisitStatus.WaterQualityManagementPlanVisitStatusDisplayName
            };
            return dto;
        }
    }
}