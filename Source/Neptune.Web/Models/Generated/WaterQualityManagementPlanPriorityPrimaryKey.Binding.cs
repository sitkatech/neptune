//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanPriority
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanPriorityPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanPriority>
    {
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