//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifyStatus
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyStatusPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanVerifyStatus>
    {
        public WaterQualityManagementPlanVerifyStatusPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifyStatusPrimaryKey(WaterQualityManagementPlanVerifyStatus waterQualityManagementPlanVerifyStatus) : base(waterQualityManagementPlanVerifyStatus){}

        public static implicit operator WaterQualityManagementPlanVerifyStatusPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifyStatusPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifyStatusPrimaryKey(WaterQualityManagementPlanVerifyStatus waterQualityManagementPlanVerifyStatus)
        {
            return new WaterQualityManagementPlanVerifyStatusPrimaryKey(waterQualityManagementPlanVerifyStatus);
        }
    }
}