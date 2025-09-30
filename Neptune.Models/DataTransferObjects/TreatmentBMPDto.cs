using System;
using System.Collections.Generic;

namespace Neptune.Models.DataTransferObjects
{
    public class TreatmentBMPDto
    {
        // Basic Info
        public int? TreatmentBMPID { get; set; }
        public string? TreatmentBMPName { get; set; }
        public int? TreatmentBMPTypeID { get; set; }
        public string? TreatmentBMPTypeName { get; set; }
        public int? StormwaterJurisdictionID { get; set; }
        public string? StormwaterJurisdictionName { get; set; }
        public int? OwnerOrganizationID { get; set; }
        public string? OwnerOrganizationName { get; set; }
        public int? YearBuilt { get; set; }
        public string? Notes { get; set; }
        public bool? InventoryIsVerified { get; set; }
        public int? ProjectID { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? SystemOfRecordID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public DateTime? TreatmentBMPLifespanEndDate { get; set; }
        public int? RequiredFieldVisitsPerYear { get; set; }
        public int? RequiredPostStormFieldVisitsPerYear { get; set; }
        public bool? InventoryIsVerifiedByPerson { get; set; }
        public DateTime? DateOfLastInventoryVerification { get; set; }
        public int? InventoryVerifiedByPersonID { get; set; }
        public DateTime? InventoryLastChangedDate { get; set; }
        public int? TrashCaptureStatusTypeID { get; set; }
        public int? SizingBasisTypeID { get; set; }
        public string? TrashCaptureEffectiveness { get; set; }
        public int? WatershedID { get; set; }
        public int? ModelBasinID { get; set; }
        public int? PrecipitationZoneID { get; set; }
        public int? UpstreamBMPID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? LastNereidLogID { get; set; }

        // Modeling/Parameterization
        public bool? IsFullyParameterized { get; set; }
        public bool? HasModelingAttributes { get; set; }
        public vTreatmentBMPModelingAttributeDto? TreatmentBMPModelingAttribute { get; set; }

        // HRU Characteristics
        public object? HRUCharacteristics { get; set; }
        public object? ModeledPerformance { get; set; }

        // Related Entities
        public bool? OtherTreatmentBmpsExistInSubbasin { get; set; }
        public List<CustomAttributeUpsertDto>? CustomAttributes { get; set; }
        public DelineationDto? Delineation { get; set; }
        public TreatmentBMPDto? UpstreamBMP { get; set; }
        public bool? IsUpstreamestBMPAnalyzedInModelingModule { get; set; }
        public List<RegionalSubbasinRevisionRequestDto>? RegionalSubbasinRevisionRequests { get; set; }
        public WatershedDto? Watershed { get; set; }
        public string? WatershedFieldDefinitionText { get; set; }
        public ProjectDto? Project { get; set; }
        public OrganizationDto? OwnerOrganization { get; set; }
        public StormwaterJurisdictionDto? StormwaterJurisdiction { get; set; }
        public WaterQualityManagementPlanDto? WaterQualityManagementPlan { get; set; }
        public List<MaintenanceRecordDto>? MaintenanceRecords { get; set; }
        public List<TreatmentBMPAssessmentDto>? TreatmentBMPAssessments { get; set; }
        public List<TreatmentBMPBenchmarkAndThresholdDto>? TreatmentBMPBenchmarkAndThresholds { get; set; }
        public List<NereidResultDto>? NereidResults { get; set; }
        public List<ProjectNereidResultDto>? ProjectNereidResults { get; set; }
        public List<WaterQualityManagementPlanVerifyTreatmentBMPDto>? WaterQualityManagementPlanVerifyTreatmentBMPs { get; set; }

        // Errors
        public List<string>? DelineationErrors { get; set; }
        public List<string>? ParameterizationErrors { get; set; }

        // Display Names
        public SizingBasisTypeDto? SizingBasisType { get; set; }
        public TrashCaptureStatusTypeDto? TrashCaptureStatusType { get; set; }
        public TreatmentBMPLifeSpanTypeDto? TreatmentBMPLifespanType { get; set; }
    }
}
