namespace Neptune.Web.Models
{
    public partial class TreatmentBMPTypeObservationType : IAuditableEntity
    {
        public string AuditDescriptionString =>
            $"Treatment BMP Type: {TreatmentBMPType?.TreatmentBMPTypeName ?? "Unknown"}; ObservationType: {ObservationType?.ObservationTypeName ?? "Unknown"}";
    }
}