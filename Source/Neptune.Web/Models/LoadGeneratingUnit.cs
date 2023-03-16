namespace Neptune.Web.Models;

public partial class LoadGeneratingUnit : IAuditableEntity
{
    public string GetAuditDescriptionString()
    {
        return $"LoadGeneratingUnit ID {LoadGeneratingUnitID}";
    }
}