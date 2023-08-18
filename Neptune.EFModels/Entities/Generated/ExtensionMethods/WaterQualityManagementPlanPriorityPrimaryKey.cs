//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanPriority


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanPriorityPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanPriority>
    {
        public WaterQualityManagementPlanPriorityPrimaryKey() : base(){}
        public WaterQualityManagementPlanPriorityPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanPriorityPrimaryKey(WaterQualityManagementPlanPriority waterQualityManagementPlanPriority) : base(waterQualityManagementPlanPriority){}

        public static implicit operator WaterQualityManagementPlanPriorityPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanPriorityPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanPriorityPrimaryKey(WaterQualityManagementPlanPriority waterQualityManagementPlanPriority)
        {
            return new WaterQualityManagementPlanPriorityPrimaryKey(waterQualityManagementPlanPriority);
        }
    }
}