//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanNereidLog]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanNereidLogExtensionMethods
    {
        public static WaterQualityManagementPlanNereidLogSimpleDto AsSimpleDto(this WaterQualityManagementPlanNereidLog waterQualityManagementPlanNereidLog)
        {
            var dto = new WaterQualityManagementPlanNereidLogSimpleDto()
            {
                WaterQualityManagementPlanNereidLogID = waterQualityManagementPlanNereidLog.WaterQualityManagementPlanNereidLogID,
                WaterQualityManagementPlanID = waterQualityManagementPlanNereidLog.WaterQualityManagementPlanID,
                LastRequestDate = waterQualityManagementPlanNereidLog.LastRequestDate,
                NereidRequest = waterQualityManagementPlanNereidLog.NereidRequest,
                NereidResponse = waterQualityManagementPlanNereidLog.NereidResponse
            };
            return dto;
        }
    }
}