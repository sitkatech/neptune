namespace Neptune.Web.Models
{
    public partial class TreatmentBMPAssessmentPhoto : IAuditableEntity
    {
        public string AuditDescriptionString =>
            $"Treatment BMP Assessment Photo {FileResource?.OriginalCompleteFileName ?? "<File Name Not Found>"}";
    }
}
