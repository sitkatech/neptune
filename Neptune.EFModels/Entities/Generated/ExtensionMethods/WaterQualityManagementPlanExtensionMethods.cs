//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlan]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanExtensionMethods
    {
        public static WaterQualityManagementPlanSimpleDto AsSimpleDto(this WaterQualityManagementPlan waterQualityManagementPlan)
        {
            var dto = new WaterQualityManagementPlanSimpleDto()
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
                DateOfConstruction = waterQualityManagementPlan.DateOfConstruction,
                HydrologicSubareaID = waterQualityManagementPlan.HydrologicSubareaID,
                RecordNumber = waterQualityManagementPlan.RecordNumber,
                RecordedWQMPAreaInAcres = waterQualityManagementPlan.RecordedWQMPAreaInAcres,
                TrashCaptureStatusTypeID = waterQualityManagementPlan.TrashCaptureStatusTypeID,
                TrashCaptureEffectiveness = waterQualityManagementPlan.TrashCaptureEffectiveness,
                WaterQualityManagementPlanModelingApproachID = waterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID,
                LastNereidLogID = waterQualityManagementPlan.LastNereidLogID
            };
            return dto;
        }
    }
}