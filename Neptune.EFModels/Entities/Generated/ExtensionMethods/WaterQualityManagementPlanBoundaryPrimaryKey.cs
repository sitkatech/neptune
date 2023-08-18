//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanBoundary


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanBoundaryPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanBoundary>
    {
        public WaterQualityManagementPlanBoundaryPrimaryKey() : base(){}
        public WaterQualityManagementPlanBoundaryPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanBoundaryPrimaryKey(WaterQualityManagementPlanBoundary waterQualityManagementPlanBoundary) : base(waterQualityManagementPlanBoundary){}

        public static implicit operator WaterQualityManagementPlanBoundaryPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanBoundaryPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanBoundaryPrimaryKey(WaterQualityManagementPlanBoundary waterQualityManagementPlanBoundary)
        {
            return new WaterQualityManagementPlanBoundaryPrimaryKey(waterQualityManagementPlanBoundary);
        }
    }
}