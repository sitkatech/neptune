namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanVerifyQuickBMP : IAuditableEntity
    {

        public WaterQualityManagementPlanVerifyQuickBMP(WaterQualityManagementPlanVerifyQuickBMPSimple waterQualityManagementPlanVerifyQuickBMPSimple, int tenantId, int waterQualityManagementPlanVerifyID)
        {
            WaterQualityManagementPlanVerifyQuickBMPID = waterQualityManagementPlanVerifyQuickBMPSimple.QuickBMPID;
            TenantID = tenantId;
            WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyID;
            QuickBMPID = waterQualityManagementPlanVerifyQuickBMPSimple.QuickBMPID;
            IsAdequate = waterQualityManagementPlanVerifyQuickBMPSimple.IsAdequate;
            WaterQualityManagementPlanVerifyQuickBMPNote = waterQualityManagementPlanVerifyQuickBMPSimple
                .WaterQualityManagementPlanVerifyQuickBMPNote;
        }


        public string GetAuditDescriptionString()
        {
            return WaterQualityManagementPlanVerifyQuickBMPID.ToString();
        }
    }

}
