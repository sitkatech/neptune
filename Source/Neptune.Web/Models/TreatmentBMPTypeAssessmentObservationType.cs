namespace Neptune.Web.Models
{
    public partial class TreatmentBMPTypeAssessmentObservationType : IAuditableEntity
    {
        public string AuditDescriptionString =>
            $"Treatment BMP Type: {TreatmentBMPType?.TreatmentBMPTypeName ?? "Unknown"}; TreatmentBMPAssessmentObservationType: {TreatmentBMPAssessmentObservationType?.TreatmentBMPAssessmentObservationTypeName ?? "Unknown"}";
    }
}