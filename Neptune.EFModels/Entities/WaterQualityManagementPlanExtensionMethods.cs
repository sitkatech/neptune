﻿using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static  class WaterQualityManagementPlanExtensionMethods
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

    public static WaterQualityManagementPlanDto AsDto(this WaterQualityManagementPlan plan)
    {
        return new WaterQualityManagementPlanDto
        {
            WaterQualityManagementPlanID = plan.WaterQualityManagementPlanID,
            StormwaterJurisdictionID = plan.StormwaterJurisdictionID,
            WaterQualityManagementPlanLandUseID = plan.WaterQualityManagementPlanLandUseID,
            WaterQualityManagementPlanPriorityID = plan.WaterQualityManagementPlanPriorityID,
            WaterQualityManagementPlanStatusID = plan.WaterQualityManagementPlanStatusID,
            WaterQualityManagementPlanDevelopmentTypeID = plan.WaterQualityManagementPlanDevelopmentTypeID,
            WaterQualityManagementPlanName = plan.WaterQualityManagementPlanName,
            ApprovalDate = plan.ApprovalDate,
            MaintenanceContactName = plan.MaintenanceContactName,
            MaintenanceContactOrganization = plan.MaintenanceContactOrganization,
            MaintenanceContactPhone = plan.MaintenanceContactPhone,
            MaintenanceContactAddress1 = plan.MaintenanceContactAddress1,
            MaintenanceContactAddress2 = plan.MaintenanceContactAddress2,
            MaintenanceContactCity = plan.MaintenanceContactCity,
            MaintenanceContactState = plan.MaintenanceContactState,
            MaintenanceContactZip = plan.MaintenanceContactZip,
            WaterQualityManagementPlanPermitTermID = plan.WaterQualityManagementPlanPermitTermID,
            HydromodificationAppliesTypeID = plan.HydromodificationAppliesTypeID,
            DateOfConstruction = plan.DateOfConstruction,
            HydrologicSubareaID = plan.HydrologicSubareaID,
            RecordNumber = plan.RecordNumber,
            RecordedWQMPAreaInAcres = plan.RecordedWQMPAreaInAcres,
            TrashCaptureStatusTypeID = plan.TrashCaptureStatusTypeID,
            TrashCaptureEffectiveness = plan.TrashCaptureEffectiveness,
            WaterQualityManagementPlanModelingApproachID = plan.WaterQualityManagementPlanModelingApproachID,
            LastNereidLogID = plan.LastNereidLogID,
            WaterQualityManagementPlanBoundaryNotes = plan.WaterQualityManagementPlanBoundaryNotes
        };
    }

    public static WaterQualityManagementPlanSimpleDto AsSimpleDto(this WaterQualityManagementPlan plan)
    {
        return new WaterQualityManagementPlanSimpleDto
        {
            WaterQualityManagementPlanID = plan.WaterQualityManagementPlanID,
            StormwaterJurisdictionID = plan.StormwaterJurisdictionID,
            WaterQualityManagementPlanLandUseID = plan.WaterQualityManagementPlanLandUseID,
            WaterQualityManagementPlanPriorityID = plan.WaterQualityManagementPlanPriorityID,
            WaterQualityManagementPlanStatusID = plan.WaterQualityManagementPlanStatusID,
            WaterQualityManagementPlanDevelopmentTypeID = plan.WaterQualityManagementPlanDevelopmentTypeID,
            WaterQualityManagementPlanName = plan.WaterQualityManagementPlanName,
            ApprovalDate = plan.ApprovalDate,
            WaterQualityManagementPlanPermitTermID = plan.WaterQualityManagementPlanPermitTermID,
            HydromodificationAppliesTypeID = plan.HydromodificationAppliesTypeID,
            DateOfConstruction = plan.DateOfConstruction,
            HydrologicSubareaID = plan.HydrologicSubareaID,
            RecordNumber = plan.RecordNumber,
            RecordedWQMPAreaInAcres = plan.RecordedWQMPAreaInAcres,
            TrashCaptureStatusTypeID = plan.TrashCaptureStatusTypeID,
            TrashCaptureEffectiveness = plan.TrashCaptureEffectiveness,
            WaterQualityManagementPlanModelingApproachID = plan.WaterQualityManagementPlanModelingApproachID,
        };
    }

    public static WaterQualityManagementPlanExtractDto AsExtractDto(this WaterQualityManagementPlan entity)
    {
        return new WaterQualityManagementPlanExtractDto
        {
            WaterQualityManagementPlanName = entity.WaterQualityManagementPlanName,
            Jurisdiction = entity.StormwaterJurisdiction.Organization.OrganizationName,
            WaterQualityManagementPlanLandUse = entity.WaterQualityManagementPlanLandUse?.WaterQualityManagementPlanLandUseDisplayName,
            WaterQualityManagementPlanPriority = entity.WaterQualityManagementPlanPriority?.WaterQualityManagementPlanPriorityDisplayName,
            WaterQualityManagementPlanStatus = entity.WaterQualityManagementPlanStatus?.WaterQualityManagementPlanStatusDisplayName,
            WaterQualityManagementPlanDevelopmentType = entity.WaterQualityManagementPlanDevelopmentType?.WaterQualityManagementPlanDevelopmentTypeDisplayName,
            ApprovalDate = entity.ApprovalDate,
            MaintenanceContactName = entity.MaintenanceContactName,
            MaintenanceContactOrganization = entity.MaintenanceContactOrganization,
            MaintenanceContactPhone = entity.MaintenanceContactPhone,
            MaintenanceContactAddress1 = entity.MaintenanceContactAddress1,
            MaintenanceContactAddress2 = entity.MaintenanceContactAddress2,
            MaintenanceContactCity = entity.MaintenanceContactCity,
            MaintenanceContactState = entity.MaintenanceContactState,
            MaintenanceContactZip = entity.MaintenanceContactZip,
            WaterQualityManagementPlanPermitTerm = entity.WaterQualityManagementPlanPermitTerm?.WaterQualityManagementPlanPermitTermDisplayName,
            DateOfConstruction = entity.DateOfConstruction,
            HydrologicSubarea = entity.HydrologicSubarea?.HydrologicSubareaName,
            RecordNumber = entity.RecordNumber,
            RecordedWQMPAreaInAcres = entity.RecordedWQMPAreaInAcres,
            TrashCaptureStatusType = entity.TrashCaptureStatusType?.TrashCaptureStatusTypeDisplayName,
            TrashCaptureEffectiveness = entity.TrashCaptureEffectiveness,
            WaterQualityManagementPlanModelingApproach = entity.WaterQualityManagementPlanModelingApproach?.WaterQualityManagementPlanModelingApproachDisplayName,
            WaterQualityManagementPlanBoundaryNotes = entity.WaterQualityManagementPlanBoundaryNotes,
        };
    }

    public static void UpdateFromUpsertDto(this WaterQualityManagementPlan entity, WaterQualityManagementPlanUpsertDto dto)
    {
        entity.StormwaterJurisdictionID = dto.StormwaterJurisdictionID;
        entity.WaterQualityManagementPlanLandUseID = dto.WaterQualityManagementPlanLandUseID;
        entity.WaterQualityManagementPlanPriorityID = dto.WaterQualityManagementPlanPriorityID;
        entity.WaterQualityManagementPlanStatusID = dto.WaterQualityManagementPlanStatusID;
        entity.WaterQualityManagementPlanDevelopmentTypeID = dto.WaterQualityManagementPlanDevelopmentTypeID;
        entity.WaterQualityManagementPlanName = dto.WaterQualityManagementPlanName;
        entity.ApprovalDate = dto.ApprovalDate;
        entity.MaintenanceContactName = dto.MaintenanceContactName;
        entity.MaintenanceContactOrganization = dto.MaintenanceContactOrganization;
        entity.MaintenanceContactPhone = dto.MaintenanceContactPhone;
        entity.MaintenanceContactAddress1 = dto.MaintenanceContactAddress1;
        entity.MaintenanceContactAddress2 = dto.MaintenanceContactAddress2;
        entity.MaintenanceContactCity = dto.MaintenanceContactCity;
        entity.MaintenanceContactState = dto.MaintenanceContactState;
        entity.MaintenanceContactZip = dto.MaintenanceContactZip;
        entity.WaterQualityManagementPlanPermitTermID = dto.WaterQualityManagementPlanPermitTermID;
        entity.HydromodificationAppliesTypeID = dto.HydromodificationAppliesTypeID;
        entity.DateOfConstruction = dto.DateOfConstruction;
        entity.HydrologicSubareaID = dto.HydrologicSubareaID;
        entity.RecordNumber = dto.RecordNumber;
        entity.RecordedWQMPAreaInAcres = dto.RecordedWQMPAreaInAcres;
        entity.TrashCaptureStatusTypeID = dto.TrashCaptureStatusTypeID;
        entity.TrashCaptureEffectiveness = dto.TrashCaptureEffectiveness;
        entity.WaterQualityManagementPlanModelingApproachID = dto.WaterQualityManagementPlanModelingApproachID;
        entity.LastNereidLogID = dto.LastNereidLogID;
        entity.WaterQualityManagementPlanBoundaryNotes = dto.WaterQualityManagementPlanBoundaryNotes;
    }

    public static WaterQualityManagementPlan AsEntity(this WaterQualityManagementPlanUpsertDto dto)
    {
        return new WaterQualityManagementPlan
        {
            StormwaterJurisdictionID = dto.StormwaterJurisdictionID,
            WaterQualityManagementPlanLandUseID = dto.WaterQualityManagementPlanLandUseID,
            WaterQualityManagementPlanPriorityID = dto.WaterQualityManagementPlanPriorityID,
            WaterQualityManagementPlanStatusID = dto.WaterQualityManagementPlanStatusID,
            WaterQualityManagementPlanDevelopmentTypeID = dto.WaterQualityManagementPlanDevelopmentTypeID,
            WaterQualityManagementPlanName = dto.WaterQualityManagementPlanName,
            ApprovalDate = dto.ApprovalDate,
            MaintenanceContactName = dto.MaintenanceContactName,
            MaintenanceContactOrganization = dto.MaintenanceContactOrganization,
            MaintenanceContactPhone = dto.MaintenanceContactPhone,
            MaintenanceContactAddress1 = dto.MaintenanceContactAddress1,
            MaintenanceContactAddress2 = dto.MaintenanceContactAddress2,
            MaintenanceContactCity = dto.MaintenanceContactCity,
            MaintenanceContactState = dto.MaintenanceContactState,
            MaintenanceContactZip = dto.MaintenanceContactZip,
            WaterQualityManagementPlanPermitTermID = dto.WaterQualityManagementPlanPermitTermID,
            HydromodificationAppliesTypeID = dto.HydromodificationAppliesTypeID,
            DateOfConstruction = dto.DateOfConstruction,
            HydrologicSubareaID = dto.HydrologicSubareaID,
            RecordNumber = dto.RecordNumber,
            RecordedWQMPAreaInAcres = dto.RecordedWQMPAreaInAcres,
            TrashCaptureStatusTypeID = dto.TrashCaptureStatusTypeID,
            TrashCaptureEffectiveness = dto.TrashCaptureEffectiveness,
            WaterQualityManagementPlanModelingApproachID = dto.WaterQualityManagementPlanModelingApproachID,
            LastNereidLogID = dto.LastNereidLogID,
            WaterQualityManagementPlanBoundaryNotes = dto.WaterQualityManagementPlanBoundaryNotes
        };
    }
}