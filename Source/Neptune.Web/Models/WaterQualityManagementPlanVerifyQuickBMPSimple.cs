namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyQuickBMPSimple
    {
        public WaterQualityManagementPlanVerifyQuickBMPSimple()
        {
        }
        public WaterQualityManagementPlanVerifyQuickBMPSimple(Models.QuickBMP quickBMP)
        {
            QuickBMPName = quickBMP.QuickBMPName;
            QuickBMPID = quickBMP.QuickBMPID;
            IsAdequate = null;
            WaterQualityManagementPlanVerifyQuickBMPNote = null;
        }

        public WaterQualityManagementPlanVerifyQuickBMPSimple(Models.WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMP)
        {
            QuickBMPName = waterQualityManagementPlanVerifyQuickBMP.QuickBMP.QuickBMPName;
            QuickBMPID = waterQualityManagementPlanVerifyQuickBMP.QuickBMPID;
            IsAdequate = waterQualityManagementPlanVerifyQuickBMP.IsAdequate;
            WaterQualityManagementPlanVerifyQuickBMPNote = waterQualityManagementPlanVerifyQuickBMP.WaterQualityManagementPlanVerifyQuickBMPNote;
        }

        public string QuickBMPName { get; set; }
        public int QuickBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyQuickBMPNote { get; set; }
    }

}
