//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifyTreatmentBMP


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanVerifyTreatmentBMP>
    {
        public WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey() : base(){}
        public WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMP) : base(waterQualityManagementPlanVerifyTreatmentBMP){}

        public static implicit operator WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(WaterQualityManagementPlanVerifyTreatmentBMP waterQualityManagementPlanVerifyTreatmentBMP)
        {
            return new WaterQualityManagementPlanVerifyTreatmentBMPPrimaryKey(waterQualityManagementPlanVerifyTreatmentBMP);
        }
    }
}