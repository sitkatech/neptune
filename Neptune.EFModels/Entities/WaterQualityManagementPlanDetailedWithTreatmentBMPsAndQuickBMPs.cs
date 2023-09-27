namespace Neptune.EFModels.Entities;

public class WaterQualityManagementPlanDetailedWithTreatmentBMPsAndQuickBMPs
{
    public vWaterQualityManagementPlanDetailed WaterQualityManagementPlan { get; }
    public IEnumerable<TreatmentBMPWithModelingAttributesAndDelineation> TreatmentBMPsWithModelingAttributesAndDelineation { get; }
    public IEnumerable<QuickBMP> QuickBMPs { get; }

    public WaterQualityManagementPlanDetailedWithTreatmentBMPsAndQuickBMPs(vWaterQualityManagementPlanDetailed waterQualityManagementPlan, IEnumerable<TreatmentBMPWithModelingAttributesAndDelineation> treatmentBMPsWithModelingAttributesAndDelineation, IEnumerable<QuickBMP> quickBMPs)
    {
        WaterQualityManagementPlan = waterQualityManagementPlan;
        TreatmentBMPsWithModelingAttributesAndDelineation = treatmentBMPsWithModelingAttributesAndDelineation;
        QuickBMPs = quickBMPs;
    }
}