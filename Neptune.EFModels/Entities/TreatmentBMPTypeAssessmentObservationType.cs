using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMPTypeAssessmentObservationType : IAuditableEntity, IHaveASortOrder
    {
        public string GetAuditDescriptionString()
        {
            return
                $"Treatment BMP Type: {TreatmentBMPType?.TreatmentBMPTypeName ?? "Unknown"}; TreatmentBMPAssessmentObservationType: {TreatmentBMPAssessmentObservationType?.TreatmentBMPAssessmentObservationTypeName ?? "Unknown"}";
        }

        public string GetDisplayName()
        {
            return TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName;
        }

        public int GetID()
        {
            return TreatmentBMPTypeAssessmentObservationTypeID;
        }
    }
}