//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerify


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanVerifyPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanVerify>
    {
        public WaterQualityManagementPlanVerifyPrimaryKey() : base(){}
        public WaterQualityManagementPlanVerifyPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifyPrimaryKey(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify) : base(waterQualityManagementPlanVerify){}

        public static implicit operator WaterQualityManagementPlanVerifyPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifyPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifyPrimaryKey(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            return new WaterQualityManagementPlanVerifyPrimaryKey(waterQualityManagementPlanVerify);
        }
    }
}