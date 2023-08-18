//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanStatus


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanStatusPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanStatus>
    {
        public WaterQualityManagementPlanStatusPrimaryKey() : base(){}
        public WaterQualityManagementPlanStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanStatusPrimaryKey(WaterQualityManagementPlanStatus waterQualityManagementPlanStatus) : base(waterQualityManagementPlanStatus){}

        public static implicit operator WaterQualityManagementPlanStatusPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanStatusPrimaryKey(WaterQualityManagementPlanStatus waterQualityManagementPlanStatus)
        {
            return new WaterQualityManagementPlanStatusPrimaryKey(waterQualityManagementPlanStatus);
        }
    }
}