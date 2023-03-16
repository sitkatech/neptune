namespace Neptune.Web.Models;

public partial class TrashGeneratingUnit4326 : IAuditableEntity
{
    public string GetAuditDescriptionString()
    {
        return $"TrashGeneratingUnit4326 ID {TrashGeneratingUnit4326ID}";
    }
}