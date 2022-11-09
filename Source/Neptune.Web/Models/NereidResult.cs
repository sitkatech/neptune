namespace Neptune.Web.Models
{
    public partial class NereidResult : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"TreatmentBMPID: {TreatmentBMPID}";
        }
    }
}