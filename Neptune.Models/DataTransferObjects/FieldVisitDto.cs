using System;
using System.Web;

namespace Neptune.Models.DataTransferObjects
{
    public class FieldVisitDto
    {
        public int FieldVisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public int TreatmentBMPID { get; set; }
        public string? TreatmentBMPName { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public string? TreatmentBMPTypeName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public string OrganizationName { get; set; } = null!;
        public int FieldVisitTypeID { get; set; }
        public string? FieldVisitTypeDisplayName { get; set; }
        public int PerformedByPersonID { get; set; }
        public string? PerformedByPersonName { get; set; }
        public int FieldVisitStatusID { get; set; }
        public string? FieldVisitStatusDisplayName { get; set; }
        public bool IsFieldVisitVerified { get; set; }
        public bool InventoryUpdated { get; set; }
        public int NumberOfRequiredAttributes { get; set; }
        public int NumberRequiredAttributesEntered { get; set; }
        public bool RequiredAttributesEntered => NumberRequiredAttributesEntered >= NumberOfRequiredAttributes;
        public int? TreatmentBMPAssessmentIDInitial { get; set; }
        public bool IsAssessmentCompleteInitial { get; set; }
        public double? AssessmentScoreInitial { get; set; }

        public string InitialAssessmentStatus =>
            TreatmentBMPAssessmentIDInitial.HasValue
                ? (IsAssessmentCompleteInitial ? "Complete" : "In Progress")
                : "Not Performed";

        public int? TreatmentBMPAssessmentIDPM { get; set; }
        public bool IsAssessmentCompletePM { get; set; }
        public double? AssessmentScorePM { get; set; }
        public int? MaintenanceRecordID { get; set; }

        public string MaintenanceOccurred =>
            MaintenanceRecordID.HasValue
                ? "Performed"
                : "Not Performed";

        public string PostMaintenanceAssessmentStatus =>
            TreatmentBMPAssessmentIDPM.HasValue
                ? (IsAssessmentCompletePM ? "Complete" : "In Progress")
                : "Not Performed";

        public int? WaterQualityManagementPlanID { get; set; }
        public string? WaterQualityManagementPlanName { get; set; }
    }
}
