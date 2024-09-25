//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanNereidLog


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanNereidLogPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanNereidLog>
    {
        public WaterQualityManagementPlanNereidLogPrimaryKey() : base(){}
        public WaterQualityManagementPlanNereidLogPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanNereidLogPrimaryKey(WaterQualityManagementPlanNereidLog waterQualityManagementPlanNereidLog) : base(waterQualityManagementPlanNereidLog){}

        public static implicit operator WaterQualityManagementPlanNereidLogPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanNereidLogPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanNereidLogPrimaryKey(WaterQualityManagementPlanNereidLog waterQualityManagementPlanNereidLog)
        {
            return new WaterQualityManagementPlanNereidLogPrimaryKey(waterQualityManagementPlanNereidLog);
        }
    }
}