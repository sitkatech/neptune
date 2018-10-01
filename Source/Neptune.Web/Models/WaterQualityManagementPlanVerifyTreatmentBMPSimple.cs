namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyTreatmentBMPSimple : IAuditableEntity
    {
        public string TreatmentBMPName { get; set; }
        public int? WaterQualityManagementPlanVerifyTreatmentBMPID { get; set; }
        public int TreatmentBMPID { get; set; }
        public bool? IsAdequate { get; set; }
        public string WaterQualityManagementPlanVerifyTreatmentBMPNote { get; set; }

        public WaterQualityManagementPlanVerifyTreatmentBMPSimple()
        {
        }
        public WaterQualityManagementPlanVerifyTreatmentBMPSimple(Models.TreatmentBMP treatmentBMP)
        {
            WaterQualityManagementPlanVerifyTreatmentBMPID = null;
            TreatmentBMPName = treatmentBMP.TreatmentBMPName;
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            IsAdequate = null;
            WaterQualityManagementPlanVerifyTreatmentBMPNote = null;
        }

        public WaterQualityManagementPlanVerifyTreatmentBMPSimple(Models.WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMP)
        {
            WaterQualityManagementPlanVerifyTreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMP
                .WaterQualityManagementPlanVerifyTreatmentBMPID;
            TreatmentBMPName = waterQualityManagementPlanVerifyTreatmentBMP.TreatmentBMP.TreatmentBMPName;
            TreatmentBMPID = waterQualityManagementPlanVerifyTreatmentBMP.TreatmentBMPID;
            IsAdequate = waterQualityManagementPlanVerifyTreatmentBMP.IsAdequate;
            WaterQualityManagementPlanVerifyTreatmentBMPNote = waterQualityManagementPlanVerifyTreatmentBMP.WaterQualityManagementPlanVerifyTreatmentBMPNote;
        }

        
        public string GetAuditDescriptionString()
        {
            return TreatmentBMPName;
        }
    }
}