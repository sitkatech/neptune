//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlan]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanExtensionMethods
    {
        public static WaterQualityManagementPlanDto AsDto(this WaterQualityManagementPlan waterQualityManagementPlan)
        {
            var waterQualityManagementPlanDto = new WaterQualityManagementPlanDto()
            {
                WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID,
                StormwaterJurisdiction = waterQualityManagementPlan.StormwaterJurisdiction.AsDto(),
                WaterQualityManagementPlanLandUse = waterQualityManagementPlan.WaterQualityManagementPlanLandUse?.AsDto(),
                WaterQualityManagementPlanPriority = waterQualityManagementPlan.WaterQualityManagementPlanPriority?.AsDto(),
                WaterQualityManagementPlanStatus = waterQualityManagementPlan.WaterQualityManagementPlanStatus?.AsDto(),
                WaterQualityManagementPlanDevelopmentType = waterQualityManagementPlan.WaterQualityManagementPlanDevelopmentType?.AsDto(),
                WaterQualityManagementPlanName = waterQualityManagementPlan.WaterQualityManagementPlanName,
                ApprovalDate = waterQualityManagementPlan.ApprovalDate,
                MaintenanceContactName = waterQualityManagementPlan.MaintenanceContactName,
                MaintenanceContactOrganization = waterQualityManagementPlan.MaintenanceContactOrganization,
                MaintenanceContactPhone = waterQualityManagementPlan.MaintenanceContactPhone,
                MaintenanceContactAddress1 = waterQualityManagementPlan.MaintenanceContactAddress1,
                MaintenanceContactAddress2 = waterQualityManagementPlan.MaintenanceContactAddress2,
                MaintenanceContactCity = waterQualityManagementPlan.MaintenanceContactCity,
                MaintenanceContactState = waterQualityManagementPlan.MaintenanceContactState,
                MaintenanceContactZip = waterQualityManagementPlan.MaintenanceContactZip,
                WaterQualityManagementPlanPermitTerm = waterQualityManagementPlan.WaterQualityManagementPlanPermitTerm?.AsDto(),
                HydromodificationAppliesType = waterQualityManagementPlan.HydromodificationAppliesType?.AsDto(),
                DateOfContruction = waterQualityManagementPlan.DateOfContruction,
                HydrologicSubarea = waterQualityManagementPlan.HydrologicSubarea?.AsDto(),
                RecordNumber = waterQualityManagementPlan.RecordNumber,
                RecordedWQMPAreaInAcres = waterQualityManagementPlan.RecordedWQMPAreaInAcres,
                TrashCaptureStatusType = waterQualityManagementPlan.TrashCaptureStatusType.AsDto(),
                TrashCaptureEffectiveness = waterQualityManagementPlan.TrashCaptureEffectiveness,
                WaterQualityManagementPlanModelingApproach = waterQualityManagementPlan.WaterQualityManagementPlanModelingApproach.AsDto(),
                WaterQualityManagementPlanAreaInAcres = waterQualityManagementPlan.WaterQualityManagementPlanAreaInAcres
            };
            DoCustomMappings(waterQualityManagementPlan, waterQualityManagementPlanDto);
            return waterQualityManagementPlanDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanDto waterQualityManagementPlanDto);

        public static WaterQualityManagementPlanSimpleDto AsSimpleDto(this WaterQualityManagementPlan waterQualityManagementPlan)
        {
            var waterQualityManagementPlanSimpleDto = new WaterQualityManagementPlanSimpleDto()
            {
                WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID,
                StormwaterJurisdictionID = waterQualityManagementPlan.StormwaterJurisdictionID,
                WaterQualityManagementPlanLandUseID = waterQualityManagementPlan.WaterQualityManagementPlanLandUseID,
                WaterQualityManagementPlanPriorityID = waterQualityManagementPlan.WaterQualityManagementPlanPriorityID,
                WaterQualityManagementPlanStatusID = waterQualityManagementPlan.WaterQualityManagementPlanStatusID,
                WaterQualityManagementPlanDevelopmentTypeID = waterQualityManagementPlan.WaterQualityManagementPlanDevelopmentTypeID,
                WaterQualityManagementPlanName = waterQualityManagementPlan.WaterQualityManagementPlanName,
                ApprovalDate = waterQualityManagementPlan.ApprovalDate,
                MaintenanceContactName = waterQualityManagementPlan.MaintenanceContactName,
                MaintenanceContactOrganization = waterQualityManagementPlan.MaintenanceContactOrganization,
                MaintenanceContactPhone = waterQualityManagementPlan.MaintenanceContactPhone,
                MaintenanceContactAddress1 = waterQualityManagementPlan.MaintenanceContactAddress1,
                MaintenanceContactAddress2 = waterQualityManagementPlan.MaintenanceContactAddress2,
                MaintenanceContactCity = waterQualityManagementPlan.MaintenanceContactCity,
                MaintenanceContactState = waterQualityManagementPlan.MaintenanceContactState,
                MaintenanceContactZip = waterQualityManagementPlan.MaintenanceContactZip,
                WaterQualityManagementPlanPermitTermID = waterQualityManagementPlan.WaterQualityManagementPlanPermitTermID,
                HydromodificationAppliesTypeID = waterQualityManagementPlan.HydromodificationAppliesTypeID,
                DateOfContruction = waterQualityManagementPlan.DateOfContruction,
                HydrologicSubareaID = waterQualityManagementPlan.HydrologicSubareaID,
                RecordNumber = waterQualityManagementPlan.RecordNumber,
                RecordedWQMPAreaInAcres = waterQualityManagementPlan.RecordedWQMPAreaInAcres,
                TrashCaptureStatusTypeID = waterQualityManagementPlan.TrashCaptureStatusTypeID,
                TrashCaptureEffectiveness = waterQualityManagementPlan.TrashCaptureEffectiveness,
                WaterQualityManagementPlanModelingApproachID = waterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID,
                WaterQualityManagementPlanAreaInAcres = waterQualityManagementPlan.WaterQualityManagementPlanAreaInAcres
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlan, waterQualityManagementPlanSimpleDto);
            return waterQualityManagementPlanSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlan waterQualityManagementPlan, WaterQualityManagementPlanSimpleDto waterQualityManagementPlanSimpleDto);
    }
}