using LtInfo.Common.Models;

namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanVerifyTreatmentBMP : IAuditableEntity
    {

        public WaterQualityManagementPlanVerifyTreatmentBMP(WaterQualityManagementPlanVerifyTreatmentBMPSimple waterQualityManagementPlanVerifyTreatmentBMPSimple, int waterQualityManagementPlanVerifyID)
        {
            WaterQualityManagementPlanVerifyTreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMPSimple.WaterQualityManagementPlanVerifyTreatmentBMPID ?? ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            TreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMPSimple.TreatmentBMPID;
            IsAdequate = waterQualityManagementPlanVerifyTreatmentBMPSimple.IsAdequate;
            WaterQualityManagementPlanVerifyTreatmentBMPNote = waterQualityManagementPlanVerifyTreatmentBMPSimple
                .WaterQualityManagementPlanVerifyTreatmentBMPNote;
        }

        public string GetAuditDescriptionString()
        {
            return  $"Treatment BMP Name: {TreatmentBMP?.TreatmentBMPName};  WaterQualityManagementPlanName: {WaterQualityManagementPlanVerify?.WaterQualityManagementPlan?.WaterQualityManagementPlanName}";
        }
    }

}