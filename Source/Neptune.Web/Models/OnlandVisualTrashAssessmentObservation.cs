namespace Neptune.Web.Models
{
    public partial class OnlandVisualTrashAssessmentObservation : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return
                $"OVTA Observation {OnlandVisualTrashAssessmentObservationID} for OVTA {OnlandVisualTrashAssessmentID} deleted";
        }
    }
}