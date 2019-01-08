using LtInfo.Common.Models;

namespace Neptune.Web.Models
{
    public partial class WaterQualityManagementPlanVerifyQuickBMP : IAuditableEntity
    {

        public WaterQualityManagementPlanVerifyQuickBMP(WaterQualityManagementPlanVerifyQuickBMPSimple waterQualityManagementPlanVerifyQuickBMPSimple, int waterQualityManagementPlanVerifyID)
        {
            WaterQualityManagementPlanVerifyQuickBMPID = waterQualityManagementPlanVerifyQuickBMPSimple.WaterQualityManagementPlanVerifyQuickBMPID ?? ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();;
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
