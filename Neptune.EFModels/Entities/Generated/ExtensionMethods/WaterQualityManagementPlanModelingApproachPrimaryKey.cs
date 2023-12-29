//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanModelingApproach


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanModelingApproachPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanModelingApproach>
    {
        public WaterQualityManagementPlanModelingApproachPrimaryKey() : base(){}
        public WaterQualityManagementPlanModelingApproachPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanModelingApproachPrimaryKey(WaterQualityManagementPlanModelingApproach waterQualityManagementPlanModelingApproach) : base(waterQualityManagementPlanModelingApproach){}

        public static implicit operator WaterQualityManagementPlanModelingApproachPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanModelingApproachPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanModelingApproachPrimaryKey(WaterQualityManagementPlanModelingApproach waterQualityManagementPlanModelingApproach)
        {
            return new WaterQualityManagementPlanModelingApproachPrimaryKey(waterQualityManagementPlanModelingApproach);
        }
    }
}