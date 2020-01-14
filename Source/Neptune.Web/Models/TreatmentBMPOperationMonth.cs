namespace Neptune.Web.Models
{
    public partial class TreatmentBMPOperationMonth : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return OperationMonth.ToString();
        }
    }
}