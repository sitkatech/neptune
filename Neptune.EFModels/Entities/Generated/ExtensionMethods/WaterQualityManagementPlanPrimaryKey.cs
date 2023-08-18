//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlan


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlan>
    {
        public WaterQualityManagementPlanPrimaryKey() : base(){}
        public WaterQualityManagementPlanPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanPrimaryKey(WaterQualityManagementPlan waterQualityManagementPlan) : base(waterQualityManagementPlan){}

        public static implicit operator WaterQualityManagementPlanPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanPrimaryKey(WaterQualityManagementPlan waterQualityManagementPlan)
        {
            return new WaterQualityManagementPlanPrimaryKey(waterQualityManagementPlan);
        }
    }
}