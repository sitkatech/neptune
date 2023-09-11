namespace Neptune.EFModels.Entities;

public class vWaterQualityManagementPlanDetailedWithWQMPEntity
{
    public WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
    public vWaterQualityManagementPlanDetailed vWaterQualityManagementPlanDetailed { get; set; }

    public vWaterQualityManagementPlanDetailedWithWQMPEntity(WaterQualityManagementPlan waterQualityManagementPlan, vWaterQualityManagementPlanDetailed vWqmpDetailed)
    {
        WaterQualityManagementPlan = waterQualityManagementPlan;
        vWaterQualityManagementPlanDetailed = vWqmpDetailed;
    }
}