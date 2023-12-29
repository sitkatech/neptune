//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifyStatus


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanVerifyStatusPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanVerifyStatus>
    {
        public WaterQualityManagementPlanVerifyStatusPrimaryKey() : base(){}
        public WaterQualityManagementPlanVerifyStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifyStatusPrimaryKey(WaterQualityManagementPlanVerifyStatus waterQualityManagementPlanVerifyStatus) : base(waterQualityManagementPlanVerifyStatus){}

        public static implicit operator WaterQualityManagementPlanVerifyStatusPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifyStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifyStatusPrimaryKey(WaterQualityManagementPlanVerifyStatus waterQualityManagementPlanVerifyStatus)
        {
            return new WaterQualityManagementPlanVerifyStatusPrimaryKey(waterQualityManagementPlanVerifyStatus);
        }
    }
}