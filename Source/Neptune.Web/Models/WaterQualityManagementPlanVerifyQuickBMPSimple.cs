﻿namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyQuickBMPSimple : IAuditableEntity
    {
        public string QuickBMPName { get; set; }
        public int? WaterQualityManagementPlanVerifyQuickBMPID { get; set; }
        public int QuickBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyQuickBMPNote { get; set; }
        public string TreatmentBMPType { get; set; }


        public WaterQualityManagementPlanVerifyQuickBMPSimple()
        {
        }
        public WaterQualityManagementPlanVerifyQuickBMPSimple(Models.QuickBMP quickBMP)
        {
            WaterQualityManagementPlanVerifyQuickBMPID = null;
            QuickBMPName = quickBMP.QuickBMPName;
            QuickBMPID = quickBMP.QuickBMPID;
            IsAdequate = null;
            WaterQualityManagementPlanVerifyQuickBMPNote = null;
            TreatmentBMPType = quickBMP.TreatmentBMPType.TreatmentBMPTypeName;
        }

        public WaterQualityManagementPlanVerifyQuickBMPSimple(Models.WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMP)
        {
            WaterQualityManagementPlanVerifyQuickBMPID =
                waterQualityManagementPlanVerifyQuickBMP.WaterQualityManagementPlanVerifyQuickBMPID;
            QuickBMPName = waterQualityManagementPlanVerifyQuickBMP.QuickBMP.QuickBMPName;
            QuickBMPID = waterQualityManagementPlanVerifyQuickBMP.QuickBMPID;
            IsAdequate = waterQualityManagementPlanVerifyQuickBMP.IsAdequate;
            WaterQualityManagementPlanVerifyQuickBMPNote = waterQualityManagementPlanVerifyQuickBMP.WaterQualityManagementPlanVerifyQuickBMPNote;
            TreatmentBMPType = waterQualityManagementPlanVerifyQuickBMP.QuickBMP.TreatmentBMPType.TreatmentBMPTypeName;
        }

        public string GetAuditDescriptionString()
        {
            return WaterQualityManagementPlanVerifyQuickBMPID.ToString();
        }
    }
}
