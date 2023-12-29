//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMP]

namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPSimpleDto
    {
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
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
    }
}