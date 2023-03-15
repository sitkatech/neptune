namespace Neptune.Web.Models;

public partial class ProjectNereidResult : IAuditableEntity
{
    public string GetAuditDescriptionString()
    {
        return $"ProjectNereidResult ID {ProjectNereidResultID}";
    }
}