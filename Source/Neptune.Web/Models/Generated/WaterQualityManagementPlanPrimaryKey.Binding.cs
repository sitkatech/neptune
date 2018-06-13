//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlan
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlan>
    {
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