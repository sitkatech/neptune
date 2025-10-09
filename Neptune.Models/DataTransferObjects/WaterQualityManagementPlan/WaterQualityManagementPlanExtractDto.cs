using System;
using System.Collections.Generic;

namespace Neptune.Models.DataTransferObjects.WaterQualityManagementPlan
{
    public class ExtractionEvidence
    {
        public string Rationale { get; set; }
        public string Snippet { get; set; }
    }

    public class WaterQualityManagementPlanExtractDto
    {
        public string WaterQualityManagementPlanName { get; set; }
        public string Jurisdiction { get; set; }
        public string? WaterQualityManagementPlanLandUse { get; set; }
        public string? WaterQualityManagementPlanPriority { get; set; }
        public string? WaterQualityManagementPlanStatus { get; set; }
        public string? WaterQualityManagementPlanDevelopmentType { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string MaintenanceContactName { get; set; }
        public string MaintenanceContactOrganization { get; set; }
        public string MaintenanceContactPhone { get; set; }
        public string MaintenanceContactAddress1 { get; set; }
        public string MaintenanceContactAddress2 { get; set; }
        public string MaintenanceContactCity { get; set; }
        public string MaintenanceContactState { get; set; }
        public string MaintenanceContactZip { get; set; }
        public string WaterQualityManagementPlanPermitTerm{ get; set; }
        public string HydromodificationAppliesType { get; set; }
        public DateTime? DateOfConstruction { get; set; }
        public string HydrologicSubarea { get; set; }
        public string RecordNumber { get; set; }
        public decimal? RecordedWQMPAreaInAcres { get; set; }
        public string TrashCaptureStatusType { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        public string WaterQualityManagementPlanModelingApproach { get; set; }
        public string WaterQualityManagementPlanBoundaryNotes { get; set; }
        public List<WaterQualityManagementPlanParcelExtractDto> Parcels { get; set; } = new();
        public List<QuickBMPExtractDto> QuickBMPs { get; set; } = new();
        public List<TreatmentBMPExtractDto> TreatmentBMPs { get; set; } = new();
        public List<SourceControlBMPExtractDto> SourceControlBMPs { get; set; } = new();
        public Dictionary<string, ExtractionEvidence> ExtractionEvidences { get; set; } = new();
    }

    public class WaterQualityManagementPlanParcelExtractDto
    {
        public string ParcelNumber { get; set; }
        public Dictionary<string, ExtractionEvidence> ExtractionEvidences { get; set; } = new();
    }

    public class QuickBMPExtractDto
    {
        public string QuickBMPName { get; set; }
        public string QuickBMPNote { get; set; }
        public decimal? PercentOfSiteTreated { get; set; }
        public decimal? PercentCaptured { get; set; }
        public decimal? PercentRetained { get; set; }
        public string DryWeatherFlowOverride { get; set; }
        public int NumberOfIndividualBMPs { get; set; }
        public string TreatmentBMPType { get; set; }
        public Dictionary<string, ExtractionEvidence> ExtractionEvidences { get; set; } = new();
    }

    public class TreatmentBMPExtractDto
    {
        public string TreatmentBMPName { get; set; }
        public string TreatmentBMPType { get; set; }
        public string LocationPointWkt { get; set; }
        public string Jurisdiction { get; set; }
        public string Notes { get; set; }
        public string SystemOfRecordID { get; set; }
        public int? YearBuilt { get; set; }
        public string OwnerOrganization { get; set; }
        public string TreatmentBMPLifespanType { get; set; }
        public DateTime? TreatmentBMPLifespanEndDate { get; set; }
        public int? RequiredFieldVisitsPerYear { get; set; }
        public int? RequiredPostStormFieldVisitsPerYear { get; set; }
        public string TrashCaptureStatusType { get; set; }
        public string SizingBasisType { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        public Dictionary<string, ExtractionEvidence> ExtractionEvidences { get; set; } = new();
    }

    public class SourceControlBMPExtractDto
    {
        public string SourceControlBMPAttribute { get; set; }
        public bool? IsPresent { get; set; }
        public string SourceControlBMPNote { get; set; }
        public Dictionary<string, ExtractionEvidence> ExtractionEvidences { get; set; } = new();
    }
}
