namespace Neptune.Web.Models;

public partial class DirtyModelNode : IAuditableEntity
{
    public string GetAuditDescriptionString()
    {
        return $"DirtyModelNode ID {DirtyModelNodeID}";
    }
}