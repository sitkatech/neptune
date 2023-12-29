//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerify]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifyExtensionMethods
    {
        public static WaterQualityManagementPlanVerifySimpleDto AsSimpleDto(this WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            var dto = new WaterQualityManagementPlanVerifySimpleDto()
            {
                WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID,
                WaterQualityManagementPlanID = waterQualityManagementPlanVerify.WaterQualityManagementPlanID,
                WaterQualityManagementPlanVerifyTypeID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyTypeID,
                WaterQualityManagementPlanVisitStatusID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVisitStatusID,
                FileResourceID = waterQualityManagementPlanVerify.FileResourceID,
                WaterQualityManagementPlanVerifyStatusID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyStatusID,
                LastEditedByPersonID = waterQualityManagementPlanVerify.LastEditedByPersonID,
                SourceControlCondition = waterQualityManagementPlanVerify.SourceControlCondition,
                EnforcementOrFollowupActions = waterQualityManagementPlanVerify.EnforcementOrFollowupActions,
                LastEditedDate = waterQualityManagementPlanVerify.LastEditedDate,
                IsDraft = waterQualityManagementPlanVerify.IsDraft,
                VerificationDate = waterQualityManagementPlanVerify.VerificationDate
            };
            return dto;
        }
    }
}