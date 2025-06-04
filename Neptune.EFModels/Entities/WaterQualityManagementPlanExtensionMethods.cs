using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class WaterQualityManagementPlanExtensionMethods
{
    public static WaterQualityManagementPlanWithGeometryDto AsGeometryDto(
        this WaterQualityManagementPlan waterQualityManagementPlan)
    {
        var dto = new WaterQualityManagementPlanWithGeometryDto
        {
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID,
            StormwaterJurisdictionID = waterQualityManagementPlan.StormwaterJurisdictionID,
            WaterQualityManagementPlanLandUseID = waterQualityManagementPlan.WaterQualityManagementPlanLandUseID,
            WaterQualityManagementPlanPriorityID = waterQualityManagementPlan.WaterQualityManagementPlanPriorityID,
            WaterQualityManagementPlanStatusID = waterQualityManagementPlan.WaterQualityManagementPlanStatusID,
            WaterQualityManagementPlanDevelopmentTypeID =
                waterQualityManagementPlan.WaterQualityManagementPlanDevelopmentTypeID,
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
            WaterQualityManagementPlanModelingApproachID =
                waterQualityManagementPlan.WaterQualityManagementPlanModelingApproachID,
            LastNereidLogID = waterQualityManagementPlan.LastNereidLogID,
            WaterQualityManagementPlanBoundaryNotes =
                waterQualityManagementPlan.WaterQualityManagementPlanBoundaryNotes,
            WaterQualityManagementPlanBoundaryGeometry =
                waterQualityManagementPlan.WaterQualityManagementPlanBoundary?.Geometry4326
        };
        return dto;
    }
}