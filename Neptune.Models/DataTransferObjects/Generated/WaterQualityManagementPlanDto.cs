//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlan]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class WaterQualityManagementPlanDto
    {
        public int WaterQualityManagementPlanID { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
        public WaterQualityManagementPlanLandUseDto WaterQualityManagementPlanLandUse { get; set; }
        public WaterQualityManagementPlanPriorityDto WaterQualityManagementPlanPriority { get; set; }
        public WaterQualityManagementPlanStatusDto WaterQualityManagementPlanStatus { get; set; }
        public WaterQualityManagementPlanDevelopmentTypeDto WaterQualityManagementPlanDevelopmentType { get; set; }
        public string WaterQualityManagementPlanName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string MaintenanceContactName { get; set; }
        public string MaintenanceContactOrganization { get; set; }
        public string MaintenanceContactPhone { get; set; }
        public string MaintenanceContactAddress1 { get; set; }
        public string MaintenanceContactAddress2 { get; set; }
        public string MaintenanceContactCity { get; set; }
        public string MaintenanceContactState { get; set; }
        public string MaintenanceContactZip { get; set; }
        public WaterQualityManagementPlanPermitTermDto WaterQualityManagementPlanPermitTerm { get; set; }
        public HydromodificationAppliesTypeDto HydromodificationAppliesType { get; set; }
        public DateTime? DateOfContruction { get; set; }
        public HydrologicSubareaDto HydrologicSubarea { get; set; }
        public string RecordNumber { get; set; }
        public decimal? RecordedWQMPAreaInAcres { get; set; }
        public TrashCaptureStatusTypeDto TrashCaptureStatusType { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        public WaterQualityManagementPlanModelingApproachDto WaterQualityManagementPlanModelingApproach { get; set; }
        public double? WaterQualityManagementPlanAreaInAcres { get; set; }
    }

    public partial class WaterQualityManagementPlanSimpleDto
    {
        public int WaterQualityManagementPlanID { get; set; }
        public System.Int32 StormwaterJurisdictionID { get; set; }
        public System.Int32? WaterQualityManagementPlanLandUseID { get; set; }
        public System.Int32? WaterQualityManagementPlanPriorityID { get; set; }
        public System.Int32? WaterQualityManagementPlanStatusID { get; set; }
        public System.Int32? WaterQualityManagementPlanDevelopmentTypeID { get; set; }
        public string WaterQualityManagementPlanName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string MaintenanceContactName { get; set; }
        public string MaintenanceContactOrganization { get; set; }
        public string MaintenanceContactPhone { get; set; }
        public string MaintenanceContactAddress1 { get; set; }
        public string MaintenanceContactAddress2 { get; set; }
        public string MaintenanceContactCity { get; set; }
        public string MaintenanceContactState { get; set; }
        public string MaintenanceContactZip { get; set; }
        public System.Int32? WaterQualityManagementPlanPermitTermID { get; set; }
        public System.Int32? HydromodificationAppliesTypeID { get; set; }
        public DateTime? DateOfContruction { get; set; }
        public System.Int32? HydrologicSubareaID { get; set; }
        public string RecordNumber { get; set; }
        public decimal? RecordedWQMPAreaInAcres { get; set; }
        public System.Int32 TrashCaptureStatusTypeID { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        public System.Int32 WaterQualityManagementPlanModelingApproachID { get; set; }
        public double? WaterQualityManagementPlanAreaInAcres { get; set; }
    }

}