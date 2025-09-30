using System;
using System.Collections.Generic;

namespace Neptune.Models.DataTransferObjects
{
    public class WaterQualityManagementPlanDto
    {
        // Primitive properties
        public int WaterQualityManagementPlanID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? WaterQualityManagementPlanLandUseID { get; set; }
        public int? WaterQualityManagementPlanPriorityID { get; set; }
        public int? WaterQualityManagementPlanStatusID { get; set; }
        public int? WaterQualityManagementPlanDevelopmentTypeID { get; set; }
        public string? WaterQualityManagementPlanName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? MaintenanceContactName { get; set; }
        public string? MaintenanceContactOrganization { get; set; }
        public string? MaintenanceContactPhone { get; set; }
        public string? MaintenanceContactAddress1 { get; set; }
        public string? MaintenanceContactAddress2 { get; set; }
        public string? MaintenanceContactCity { get; set; }
        public string? MaintenanceContactState { get; set; }
        public string? MaintenanceContactZip { get; set; }
        public int? WaterQualityManagementPlanPermitTermID { get; set; }
        public int? HydromodificationAppliesTypeID { get; set; }
        public DateTime? DateOfConstruction { get; set; }
        public int? HydrologicSubareaID { get; set; }
        public string? RecordNumber { get; set; }
        public decimal? RecordedWQMPAreaInAcres { get; set; }
        public int TrashCaptureStatusTypeID { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        public int WaterQualityManagementPlanModelingApproachID { get; set; }
        public int? LastNereidLogID { get; set; }
        public string? WaterQualityManagementPlanBoundaryNotes { get; set; }

        // Complex/DTO properties
        public StormwaterJurisdictionDto? StormwaterJurisdiction { get; set; }
        public WaterQualityManagementPlanLandUseDto? WaterQualityManagementPlanLandUse { get; set; }
        public WaterQualityManagementPlanPriorityDto? WaterQualityManagementPlanPriority { get; set; }
        public WaterQualityManagementPlanStatusDto? WaterQualityManagementPlanStatus { get; set; }
        public WaterQualityManagementPlanDevelopmentTypeDto? WaterQualityManagementPlanDevelopmentType { get; set; }
        public WaterQualityManagementPlanPermitTermDto? WaterQualityManagementPlanPermitTerm { get; set; }
        public HydromodificationAppliesTypeDto? HydromodificationAppliesType { get; set; }
        public HydrologicSubareaDto? HydrologicSubarea { get; set; }
        public WaterQualityManagementPlanModelingApproachDto? WaterQualityManagementPlanModelingApproach { get; set; }
        public TrashCaptureStatusTypeDto? TrashCaptureStatusType { get; set; }
        public NereidLogSimpleDto? LastNereidLog { get; set; }
        public WatershedSimpleDto? Watershed { get; set; }
        public WaterQualityManagementPlanBoundaryDto? WaterQualityManagementPlanBoundary { get; set; }
        public List<WaterQualityManagementPlanDocumentDto>? WaterQualityManagementPlanDocuments { get; set; }
        public List<WaterQualityManagementPlanParcelDto>? WaterQualityManagementPlanParcels { get; set; }
        public List<WaterQualityManagementPlanVerifyDto>? WaterQualityManagementPlanVerifies { get; set; }
    }
}
