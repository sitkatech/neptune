using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class TreatmentBMPAssessmentExtensionMethods
    {
        public static TreatmentBMPAssessmentDto AsDto(this TreatmentBMPAssessment entity)
        {
            return new TreatmentBMPAssessmentDto
            {
                TreatmentBMPAssessmentID = entity.TreatmentBMPAssessmentID,
                Status = null
            };
        }
    }
}
