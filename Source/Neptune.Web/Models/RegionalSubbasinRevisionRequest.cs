namespace Neptune.Web.Models
{
    public partial class RegionalSubbasinRevisionRequest : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return
                $"TreatmentBMPID:{TreatmentBMPID} RegionalSubbasinRevisionRequestID:{RegionalSubbasinRevisionRequestID}";
        }
    }
}