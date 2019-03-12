namespace Neptune.Web.Models
{
    public partial class Delineation : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return $"Delineation ID {DelineationID}";
        }
    }
}