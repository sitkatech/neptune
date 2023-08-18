//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVisitStatus


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanVisitStatusPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanVisitStatus>
    {
        public WaterQualityManagementPlanVisitStatusPrimaryKey() : base(){}
        public WaterQualityManagementPlanVisitStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVisitStatusPrimaryKey(WaterQualityManagementPlanVisitStatus waterQualityManagementPlanVisitStatus) : base(waterQualityManagementPlanVisitStatus){}

        public static implicit operator WaterQualityManagementPlanVisitStatusPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVisitStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVisitStatusPrimaryKey(WaterQualityManagementPlanVisitStatus waterQualityManagementPlanVisitStatus)
        {
            return new WaterQualityManagementPlanVisitStatusPrimaryKey(waterQualityManagementPlanVisitStatus);
        }
    }
}