namespace Neptune.Models.DataTransferObjects
{
    public class WaterQualityManagementPlanDocumentExtractionResultDto
    {
        public string FinalOutput { get; set; }
        public string RawResults { get; set; }
        public DateTime ExtractedAt { get; set; }
    }

    /// <summary>
    /// Standard container for an extracted attribute with evidence and validation metadata.
    /// Value: raw extracted value or null. ExtractionEvidence: snippet from source PDF. DocumentSource: page reference.
    /// Validation fields are populated only during the validation pass.
    /// </summary>
    public class ExtractedValue
    {
        public string Value { get; set; }
        public string ExtractionEvidence { get; set; }
        public string DocumentSource { get; set; }
        public string ValidationScore { get; set; }
        public string ValidationEvidence { get; set; }
    }

    /// <summary>
    /// Root-level Water Quality Management Plan (WQMP) metadata extracted from a WQMP document.
    /// All properties use the <see cref="ExtractedValue"/> sub-schema (Value, ExtractionEvidence, DocumentSource).
    /// Nullable properties may legitimately be absent in the source document.
    /// </summary>
    public class WaterQualityManagementPlanExtractDto
    {
        /// <summary>Official display name / title of the WQMP.</summary>
        public ExtractedValue WaterQualityManagementPlanName { get; set; }
        /// <summary>MS4 jurisdiction (city or county) responsible for the WQMP.</summary>
        public ExtractedValue Jurisdiction { get; set; }
        /// <summary>Predominant land use classification associated with the WQMP area.</summary>
        public ExtractedValue? WaterQualityManagementPlanLandUse { get; set; }
        /// <summary>Priority category assigned to the WQMP (e.g., High, Medium).</summary>
        public ExtractedValue? WaterQualityManagementPlanPriority { get; set; }
        /// <summary>Current status of the WQMP (e.g., Draft, Approved, Implemented).</summary>
        public ExtractedValue? WaterQualityManagementPlanStatus { get; set; }
        /// <summary>Type of development triggering the WQMP (e.g., Residential, Commercial).</summary>
        public ExtractedValue? WaterQualityManagementPlanDevelopmentType { get; set; }
        /// <summary>Official approval date of the WQMP.</summary>
        public ExtractedValue? ApprovalDate { get; set; }
        /// <summary>Name of the primary maintenance contact.</summary>
        public ExtractedValue MaintenanceContactName { get; set; }
        /// <summary>Organization of the maintenance contact.</summary>
        public ExtractedValue MaintenanceContactOrganization { get; set; }
        /// <summary>Telephone number for the maintenance contact.</summary>
        public ExtractedValue MaintenanceContactPhone { get; set; }
        /// <summary>Address line 1 for the maintenance contact.</summary>
        public ExtractedValue MaintenanceContactAddress1 { get; set; }
        /// <summary>Address line 2 (suite, unit, etc.) for the maintenance contact.</summary>
        public ExtractedValue MaintenanceContactAddress2 { get; set; }
        /// <summary>City portion of the maintenance contact mailing address.</summary>
        public ExtractedValue MaintenanceContactCity { get; set; }
        /// <summary>State portion of the maintenance contact mailing address.</summary>
        public ExtractedValue MaintenanceContactState { get; set; }
        /// <summary>ZIP / postal code for the maintenance contact mailing address.</summary>
        public ExtractedValue MaintenanceContactZip { get; set; }
        /// <summary>Permit term under which this WQMP is administered.</summary>
        public ExtractedValue WaterQualityManagementPlanPermitTerm { get; set; }
        /// <summary>Date construction associated with the WQMP was completed (if stated).</summary>
        public ExtractedValue? DateOfConstruction { get; set; }
        /// <summary>Hydrologic subarea name covering the WQMP site.</summary>
        public ExtractedValue HydrologicSubarea { get; set; }
        /// <summary>Agency or permit record number referenced in the document.</summary>
        public ExtractedValue RecordNumber { get; set; }
        /// <summary>Recorded area of the WQMP site in acres.</summary>
        public ExtractedValue RecordedWQMPAreaInAcres { get; set; }
        /// <summary>Status of trash capture compliance at the WQMP level.</summary>
        public ExtractedValue TrashCaptureStatusType { get; set; }
    }

    /// <summary>
    /// Parcel-level extraction for WQMP documents.
    /// </summary>
    public class WaterQualityManagementPlanParcelExtractDto
    {
        /// <summary>Parcel identifier / number associated with the WQMP.</summary>
        public ExtractedValue ParcelNumber { get; set; }
    }
    
    /// <summary>
    /// Extraction schema for Treatment BMP entities referenced in a WQMP.
    /// Each property uses the <see cref="ExtractedValue"/> pattern.
    /// Nullable properties may be absent or not applicable.
    /// </summary>
    public class TreatmentBMPExtractDto
    {
        /// <summary>Official or reported name of the Treatment BMP.</summary>
        public ExtractedValue TreatmentBMPName { get; set; }
        /// <summary>Classification / type of the Treatment BMP (e.g., bioretention, infiltration basin).</summary>
        public ExtractedValue TreatmentBMPType { get; set; }
        /// <summary>Geospatial location encoded as WKT (Well Known Text) point.</summary>
        public ExtractedValue LocationPointAsWellKnownText { get; set; }
        /// <summary>Jurisdiction owning or responsible for the BMP.</summary>
        public ExtractedValue Jurisdiction { get; set; }
        /// <summary>Document notes or narrative comments about the BMP.</summary>
        public ExtractedValue Notes { get; set; }
        /// <summary>External or system-of-record identifier for the BMP.</summary>
        public ExtractedValue SystemOfRecordID { get; set; }
        /// <summary>Year the BMP was constructed or commissioned.</summary>
        public ExtractedValue? YearBuilt { get; set; }
        /// <summary>Organization that owns or operates the BMP.</summary>
        public ExtractedValue OwnerOrganization { get; set; }
        /// <summary>Expected lifespan category for the BMP (design life classification).</summary>
        public ExtractedValue TreatmentBMPLifespanType { get; set; }
        /// <summary>Projected or stated end date of the BMP lifespan.</summary>
        public ExtractedValue? TreatmentBMPLifespanEndDate { get; set; }
        /// <summary>Number of routine field visits required per year.</summary>
        public ExtractedValue? RequiredFieldVisitsPerYear { get; set; }
        /// <summary>Number of post-storm field visits required per year.</summary>
        public ExtractedValue? RequiredPostStormFieldVisitsPerYear { get; set; }
        /// <summary>Trash capture compliance / status specific to this BMP.</summary>
        public ExtractedValue TrashCaptureStatusType { get; set; }
        /// <summary>Basis used to size the BMP (e.g., flow rate, design storm volume).</summary>
        public ExtractedValue SizingBasisType { get; set; }
        /// <summary>Reported trash capture effectiveness (percentage or qualitative descriptor).</summary>
        public ExtractedValue? TrashCaptureEffectiveness { get; set; }
    }

    /// <summary>
    /// Extraction schema for Source Control BMP attributes listed in a WQMP.
    /// </summary>
    public class SourceControlBMPExtractDto
    {
        /// <summary>Name / descriptor of the Source Control BMP attribute (e.g., "Parking Lot Sweeping").</summary>
        public ExtractedValue SourceControlBMPAttribute { get; set; }
        /// <summary>Whether the source control measure is present / implemented (Yes/No or equivalent).</summary>
        public ExtractedValue? IsPresent { get; set; }
        /// <summary>Notes or clarifying narrative regarding the Source Control BMP attribute.</summary>
        public ExtractedValue SourceControlBMPNote { get; set; }
    }
}
