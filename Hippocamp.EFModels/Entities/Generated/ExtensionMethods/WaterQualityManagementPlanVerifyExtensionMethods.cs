//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerify]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifyExtensionMethods
    {
        public static WaterQualityManagementPlanVerifyDto AsDto(this WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            var waterQualityManagementPlanVerifyDto = new WaterQualityManagementPlanVerifyDto()
            {
                WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyID,
                WaterQualityManagementPlan = waterQualityManagementPlanVerify.WaterQualityManagementPlan.AsDto(),
                WaterQualityManagementPlanVerifyType = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyType.AsDto(),
                WaterQualityManagementPlanVisitStatus = waterQualityManagementPlanVerify.WaterQualityManagementPlanVisitStatus.AsDto(),
                FileResource = waterQualityManagementPlanVerify.FileResource?.AsDto(),
                WaterQualityManagementPlanVerifyStatus = waterQualityManagementPlanVerify.WaterQualityManagementPlanVerifyStatus?.AsDto(),
                LastEditedByPerson = waterQualityManagementPlanVerify.LastEditedByPerson.AsDto(),
                SourceControlCondition = waterQualityManagementPlanVerify.SourceControlCondition,
                EnforcementOrFollowupActions = waterQualityManagementPlanVerify.EnforcementOrFollowupActions,
                LastEditedDate = waterQualityManagementPlanVerify.LastEditedDate,
                IsDraft = waterQualityManagementPlanVerify.IsDraft,
                VerificationDate = waterQualityManagementPlanVerify.VerificationDate
            };
            DoCustomMappings(waterQualityManagementPlanVerify, waterQualityManagementPlanVerifyDto);
            return waterQualityManagementPlanVerifyDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, WaterQualityManagementPlanVerifyDto waterQualityManagementPlanVerifyDto);

        public static WaterQualityManagementPlanVerifySimpleDto AsSimpleDto(this WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            var waterQualityManagementPlanVerifySimpleDto = new WaterQualityManagementPlanVerifySimpleDto()
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
            DoCustomSimpleDtoMappings(waterQualityManagementPlanVerify, waterQualityManagementPlanVerifySimpleDto);
            return waterQualityManagementPlanVerifySimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, WaterQualityManagementPlanVerifySimpleDto waterQualityManagementPlanVerifySimpleDto);
    }
}