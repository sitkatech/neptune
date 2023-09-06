using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class WaterQualityManagementPlanVerifyQuickBMPExtensionMethods
{
    static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMP,
        WaterQualityManagementPlanVerifyQuickBMPSimpleDto waterQualityManagementPlanVerifyQuickBMPSimpleDto)
    {
        waterQualityManagementPlanVerifyQuickBMPSimpleDto.QuickBMPName = waterQualityManagementPlanVerifyQuickBMP.QuickBMP.QuickBMPName;
        waterQualityManagementPlanVerifyQuickBMPSimpleDto.TreatmentBMPType = waterQualityManagementPlanVerifyQuickBMP.QuickBMP.TreatmentBMPType.TreatmentBMPTypeName;

    }
}