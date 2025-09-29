using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class vTreatmentBMPDetailedExtensionMethods
    {
        public static TreatmentBMPIndexGridDto AsDto(this vTreatmentBMPDetailed entity)
        {
            return new TreatmentBMPIndexGridDto
            {
                TreatmentBMPID = entity.TreatmentBMPID,
                TreatmentBMPName = entity.TreatmentBMPName,
                TreatmentBMPTypeID = entity.TreatmentBMPTypeID,
                TreatmentBMPTypeName = entity.TreatmentBMPTypeName,
                StormwaterJurisdictionID = entity.StormwaterJurisdictionID,
                OrganizationName = entity.OrganizationName,
                OwnerOrganizationID = entity.OwnerOrganizationID,
                OwnerOrganizationName = entity.OwnerOrganizationName,
                YearBuilt = entity.YearBuilt,
                Notes = entity.Notes,
                LatestAssessmentDate = entity.LatestAssessmentDate,
                LatestAssessmentScore = entity.LatestAssessmentScore,
                NumberOfAssessments = entity.NumberOfAssessments,
                LatestMaintenanceDate = entity.LatestMaintenanceDate,
                NumberOfMaintenanceRecords = entity.NumberOfMaintenanceRecords,
                BenchmarkAndThresholdSet = (entity.NumberOfBenchmarkAndThresholds == entity.NumberOfBenchmarkAndThresholdsEntered),
                TreatmentBMPLifespanTypeDisplayName = entity.TreatmentBMPLifespanTypeDisplayName,
                TreatmentBMPLifespanEndDate = entity.TreatmentBMPLifespanEndDate,
                RequiredFieldVisitsPerYear = entity.RequiredFieldVisitsPerYear,
                RequiredPostStormFieldVisitsPerYear = entity.RequiredPostStormFieldVisitsPerYear,
                SizingBasisTypeDisplayName = entity.SizingBasisTypeDisplayName,
                TrashCaptureStatusTypeDisplayName = entity.TrashCaptureStatusTypeDisplayName,
                TrashCaptureEffectiveness = entity.TrashCaptureEffectiveness,
                DelineationTypeDisplayName = entity.DelineationTypeDisplayName,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
            };
        }
    }
}
