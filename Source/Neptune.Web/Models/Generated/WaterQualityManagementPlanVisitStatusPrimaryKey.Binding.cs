//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVisitStatus
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVisitStatusPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanVisitStatus>
    {
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