//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanDevelopmentType


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanDevelopmentTypePrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanDevelopmentType>
    {
        public WaterQualityManagementPlanDevelopmentTypePrimaryKey() : base(){}
        public WaterQualityManagementPlanDevelopmentTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanDevelopmentTypePrimaryKey(WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentType) : base(waterQualityManagementPlanDevelopmentType){}

        public static implicit operator WaterQualityManagementPlanDevelopmentTypePrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanDevelopmentTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanDevelopmentTypePrimaryKey(WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentType)
        {
            return new WaterQualityManagementPlanDevelopmentTypePrimaryKey(waterQualityManagementPlanDevelopmentType);
        }
    }
}