namespace Neptune.Web.Models
{
    public partial class OnlandVisualTrashAssessment : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"OVTA {OnlandVisualTrashAssessmentID} deleted";
        }
    }
}