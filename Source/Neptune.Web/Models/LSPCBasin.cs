namespace Neptune.Web.Models
{
    public partial class LSPCBasin : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return LSPCBasinName;
        }
    }
}