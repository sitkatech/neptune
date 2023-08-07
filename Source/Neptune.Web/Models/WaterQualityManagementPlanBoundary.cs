namespace Neptune.Web.Models;

public partial class WaterQualityManagementPlanBoundary : IAuditableEntity
{
    public string GetAuditDescriptionString()
    {
        return $"Water Quality Management Plan \"{WaterQualityManagementPlanID}\" geometry deleted";
    }
}