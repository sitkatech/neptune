//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifySourceControlBMP


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanVerifySourceControlBMP>
    {
        public WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey() : base(){}
        public WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(WaterQualityManagementPlanVerifySourceControlBMP waterQualityManagementPlanVerifySourceControlBMP) : base(waterQualityManagementPlanVerifySourceControlBMP){}

        public static implicit operator WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(WaterQualityManagementPlanVerifySourceControlBMP waterQualityManagementPlanVerifySourceControlBMP)
        {
            return new WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(waterQualityManagementPlanVerifySourceControlBMP);
        }
    }
}