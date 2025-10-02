using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class vFieldVisitDetailedExtensionMethods
    {
        public static FieldVisitDto AsDto(this vFieldVisitDetailed entity)
        {
            return new FieldVisitDto
            {
                FieldVisitID = entity.FieldVisitID,
                VisitDate = entity.VisitDate,
                TreatmentBMPID = entity.TreatmentBMPID,
                TreatmentBMPName = entity.TreatmentBMPName,
                TreatmentBMPTypeID = entity.TreatmentBMPTypeID,
                TreatmentBMPTypeName = entity.TreatmentBMPTypeName,
                StormwaterJurisdictionID = entity.StormwaterJurisdictionID,
                OrganizationName = entity.OrganizationName,
                FieldVisitTypeID = entity.FieldVisitTypeID,
                FieldVisitTypeDisplayName = entity.FieldVisitTypeDisplayName,
                PerformedByPersonID = entity.PerformedByPersonID,
                PerformedByPersonName = entity.PerformedByPersonName,
                FieldVisitStatusID = entity.FieldVisitStatusID,
                FieldVisitStatusDisplayName = entity.FieldVisitStatusDisplayName,
                IsFieldVisitVerified = entity.IsFieldVisitVerified,
                InventoryUpdated = entity.InventoryUpdated,
                NumberOfRequiredAttributes = entity.NumberOfRequiredAttributes,
                NumberRequiredAttributesEntered = entity.NumberRequiredAttributesEntered,
                TreatmentBMPAssessmentIDInitial = entity.TreatmentBMPAssessmentIDInitial,
                IsAssessmentCompleteInitial = entity.IsAssessmentCompleteInitial,
                AssessmentScoreInitial = entity.AssessmentScoreInitial,
                TreatmentBMPAssessmentIDPM = entity.TreatmentBMPAssessmentIDPM,
                IsAssessmentCompletePM = entity.IsAssessmentCompletePM,
                AssessmentScorePM = entity.AssessmentScorePM,
                MaintenanceRecordID = entity.MaintenanceRecordID,
                WaterQualityManagementPlanID = entity.WaterQualityManagementPlanID,
                WaterQualityManagementPlanName = entity.WaterQualityManagementPlanName
            };
        }
    }
}
