namespace Neptune.Web.Models
{
    public partial class TreatmentBMPModelingAttribute : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"{TreatmentBMPID} Modeling Attributes";
        }
    }
}