using LtInfo.Common.Models;

namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanVerifyTreatmentBMP : IAuditableEntity
    {

        public WaterQualityManagementPlanVerifyTreatmentBMP(WaterQualityManagementPlanVerifyTreatmentBMPSimple waterQualityManagementPlanVerifyTreatmentBMPSimple, int tenantId, int waterQualityManagementPlanVerifyID)
        {
            WaterQualityManagementPlanVerifyTreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMPSimple.WaterQualityManagementPlanVerifyTreatmentBMPID ?? ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            TenantID = tenantId;
            WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            TreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMPSimple.TreatmentBMPID;
            IsAdequate = waterQualityManagementPlanVerifyTreatmentBMPSimple.IsAdequate;
            WaterQualityManagementPlanVerifyTreatmentBMPNote = waterQualityManagementPlanVerifyTreatmentBMPSimple
                .WaterQualityManagementPlanVerifyTreatmentBMPNote;
        }

        public string GetAuditDescriptionString()
        {
            return TreatmentBMP.TreatmentBMPName;
        }
    }

}