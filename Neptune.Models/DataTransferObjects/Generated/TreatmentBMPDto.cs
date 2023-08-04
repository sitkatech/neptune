//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMP]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class TreatmentBMPDto
    {
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public TreatmentBMPTypeDto TreatmentBMPType { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
        public string Notes { get; set; }
        public string SystemOfRecordID { get; set; }
        public int? YearBuilt { get; set; }
        public OrganizationDto OwnerOrganization { get; set; }
        public WaterQualityManagementPlanDto WaterQualityManagementPlan { get; set; }
        public TreatmentBMPLifespanTypeDto TreatmentBMPLifespanType { get; set; }
        public DateTime? TreatmentBMPLifespanEndDate { get; set; }
        public int? RequiredFieldVisitsPerYear { get; set; }
        public int? RequiredPostStormFieldVisitsPerYear { get; set; }
        public bool InventoryIsVerified { get; set; }
        public DateTime? DateOfLastInventoryVerification { get; set; }
        public PersonDto InventoryVerifiedByPerson { get; set; }
        public DateTime? InventoryLastChangedDate { get; set; }
        public TrashCaptureStatusTypeDto TrashCaptureStatusType { get; set; }
        public SizingBasisTypeDto SizingBasisType { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        public WatershedDto Watershed { get; set; }
        public ModelBasinDto ModelBasin { get; set; }
        public PrecipitationZoneDto PrecipitationZone { get; set; }
        public int? UpstreamBMPID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public ProjectDto Project { get; set; }
    }

    public partial class TreatmentBMPSimpleDto
    {
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public System.Int32 TreatmentBMPTypeID { get; set; }
        public System.Int32 StormwaterJurisdictionID { get; set; }
        public string Notes { get; set; }
        public string SystemOfRecordID { get; set; }
        public int? YearBuilt { get; set; }
        public System.Int32 OwnerOrganizationID { get; set; }
        public System.Int32? WaterQualityManagementPlanID { get; set; }
        public System.Int32? TreatmentBMPLifespanTypeID { get; set; }
        public DateTime? TreatmentBMPLifespanEndDate { get; set; }
        public int? RequiredFieldVisitsPerYear { get; set; }
        public int? RequiredPostStormFieldVisitsPerYear { get; set; }
        public bool InventoryIsVerified { get; set; }
        public DateTime? DateOfLastInventoryVerification { get; set; }
        public System.Int32? InventoryVerifiedByPersonID { get; set; }
        public DateTime? InventoryLastChangedDate { get; set; }
        public System.Int32 TrashCaptureStatusTypeID { get; set; }
        public System.Int32 SizingBasisTypeID { get; set; }
        public int? TrashCaptureEffectiveness { get; set; }
        public System.Int32? WatershedID { get; set; }
        public System.Int32? ModelBasinID { get; set; }
        public System.Int32? PrecipitationZoneID { get; set; }
        public int? UpstreamBMPID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public System.Int32? ProjectID { get; set; }
    }

}