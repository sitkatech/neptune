//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifyQuickBMP


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanVerifyQuickBMPPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanVerifyQuickBMP>
    {
        public WaterQualityManagementPlanVerifyQuickBMPPrimaryKey() : base(){}
        public WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMP) : base(waterQualityManagementPlanVerifyQuickBMP){}

        public static implicit operator WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMP)
        {
            return new WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(waterQualityManagementPlanVerifyQuickBMP);
        }
    }
}