namespace Neptune.EFModels.Entities;

public class vWaterQualityManagementPlanDetailedWithTreatmentBMPsAndQuickBMPs
{
    public vWaterQualityManagementPlanDetailed vWaterQualityManagementPlanDetailed { get; }
    public IEnumerable<TreatmentBMPWithModelingAttributesAndDelineation> TreatmentBMPsWithModelingAttributesAndDelineation { get; }
    public IEnumerable<QuickBMP> QuickBMPs { get; }

    public vWaterQualityManagementPlanDetailedWithTreatmentBMPsAndQuickBMPs(vWaterQualityManagementPlanDetailed vWqmpDetailed, IEnumerable< TreatmentBMPWithModelingAttributesAndDelineation> treatmentBMPsWithModelingAttributesAndDelineation, IEnumerable<QuickBMP> quickBMPs)
    {
        vWaterQualityManagementPlanDetailed = vWqmpDetailed;
        TreatmentBMPsWithModelingAttributesAndDelineation = treatmentBMPsWithModelingAttributesAndDelineation;
        QuickBMPs = quickBMPs;
    }
}