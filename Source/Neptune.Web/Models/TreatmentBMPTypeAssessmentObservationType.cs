namespace Neptune.Web.Models
{
    public partial class TreatmentBMPTypeAssessmentObservationType : IAuditableEntity, IHaveASortOrder
    {
        public string AuditDescriptionString =>
            $"Treatment BMP Type: {TreatmentBMPType?.TreatmentBMPTypeName ?? "Unknown"}; TreatmentBMPAssessmentObservationType: {TreatmentBMPAssessmentObservationType?.TreatmentBMPAssessmentObservationTypeName ?? "Unknown"}";

        public string DisplayName => TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;
        public int ID => TreatmentBMPTypeAssessmentObservationTypeID;
    }
}