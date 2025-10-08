using System;
using System.Collections.Generic;

namespace Neptune.Models.DataTransferObjects.WaterQualityManagementPlan
{
    public class WaterQualityManagementPlanExtractDto
    {
        public int? WaterQualityManagementPlanID { get; set; }
        public string WaterQualityManagementPlanName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? WaterQualityManagementPlanLandUseID { get; set; }
        public int? WaterQualityManagementPlanPriorityID { get; set; }
        public int? WaterQualityManagementPlanStatusID { get; set; }
        public int? WaterQualityManagementPlanDevelopmentTypeID { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string MaintenanceContactName { get; set; }
        public string MaintenanceContactOrganization { get; set; }
        public string MaintenanceContactPhone { get; set; }
        public string MaintenanceContactAddress1 { get; set; }
        public string MaintenanceContactAddress2 { get; set; }
        public string MaintenanceContactCity { get; set; }
        public string MaintenanceContactState { get; set; }
        public string MaintenanceContactZip { get; set; }
        public int? WaterQualityManagementPlanPermitTermID { get; set; }
        public int? HydromodificationAppliesTypeID { get; set; }
        public DateTime? DateOfConstruction { get; set; }
        public int? HydrologicSubareaID { get; set; }
        public string RecordNumber { get; set; }
        public decimal? RecordedWQMPAreaInAcres { get; set; }
        public int TrashCaptureStatusTypeID { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        public int WaterQualityManagementPlanModelingApproachID { get; set; }
        public string WaterQualityManagementPlanBoundaryNotes { get; set; }
        public List<WaterQualityManagementPlanParcelExtractDto> Parcels { get; set; } = new();
        public List<QuickBMPExtractDto> QuickBMPs { get; set; } = new();
        public List<TreatmentBMPExtractDto> TreatmentBMPs { get; set; } = new();
        public List<SourceControlBMPExtractDto> SourceControlBMPs { get; set; } = new();
    }

    public class WaterQualityManagementPlanParcelExtractDto
    {
        public int? WaterQualityManagementPlanParcelID { get; set; }
        public int ParcelID { get; set; }
    }

    public class QuickBMPExtractDto
    {
        public int? QuickBMPID { get; set; }
        public string QuickBMPName { get; set; }
        public string QuickBMPNote { get; set; }
        public decimal? PercentOfSiteTreated { get; set; }
        public decimal? PercentCaptured { get; set; }
        public decimal? PercentRetained { get; set; }
        public int? DryWeatherFlowOverrideID { get; set; }
        public int NumberOfIndividualBMPs { get; set; }
        public int TreatmentBMPTypeID { get; set; }
    }

    public class TreatmentBMPExtractDto
    {
        public int? TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string LocationPointWkt { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public string Notes { get; set; }
        public string SystemOfRecordID { get; set; }
        public int? YearBuilt { get; set; }
        public int OwnerOrganizationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? TreatmentBMPLifespanTypeID { get; set; }
        public DateTime? TreatmentBMPLifespanEndDate { get; set; }
        public int? RequiredFieldVisitsPerYear { get; set; }
        public int? RequiredPostStormFieldVisitsPerYear { get; set; }
        public bool InventoryIsVerified { get; set; }
        public DateTime? DateOfLastInventoryVerification { get; set; }
        public int? InventoryVerifiedByPersonID { get; set; }
        public DateTime? InventoryLastChangedDate { get; set; }
        public int TrashCaptureStatusTypeID { get; set; }
        public int SizingBasisTypeID { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        public int? WatershedID { get; set; }
        public int? ModelBasinID { get; set; }
        public int? PrecipitationZoneID { get; set; }
        public int? UpstreamBMPID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? ProjectID { get; set; }
        public int? LastNereidLogID { get; set; }
    }

    public class SourceControlBMPExtractDto
    {
        public int? SourceControlBMPID { get; set; }
        public int SourceControlBMPAttributeID { get; set; }
        public bool? IsPresent { get; set; }
        public string SourceControlBMPNote { get; set; }
    }
}
