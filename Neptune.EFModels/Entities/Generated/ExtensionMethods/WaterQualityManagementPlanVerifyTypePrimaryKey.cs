//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifyType


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanVerifyTypePrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanVerifyType>
    {
        public WaterQualityManagementPlanVerifyTypePrimaryKey() : base(){}
        public WaterQualityManagementPlanVerifyTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifyTypePrimaryKey(WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyType) : base(waterQualityManagementPlanVerifyType){}

        public static implicit operator WaterQualityManagementPlanVerifyTypePrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifyTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifyTypePrimaryKey(WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyType)
        {
            return new WaterQualityManagementPlanVerifyTypePrimaryKey(waterQualityManagementPlanVerifyType);
        }
    }
}