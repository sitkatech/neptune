namespace Neptune.Web.Models
{
    public partial class ModelBasin : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return ModelBasinName;
        }
    }
}