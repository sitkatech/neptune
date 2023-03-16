namespace Neptune.Web.Models;

public partial class ProjectLoadGeneratingUnit : IAuditableEntity
{
    public string GetAuditDescriptionString()
    {
        return $"ProjectLoadGeneratingUnit ID {ProjectLoadGeneratingUnitID}";
    }
}