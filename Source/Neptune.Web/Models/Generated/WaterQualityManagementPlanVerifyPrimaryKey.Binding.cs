//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerify
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanVerify>
    {
        public WaterQualityManagementPlanVerifyPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifyPrimaryKey(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify) : base(waterQualityManagementPlanVerify){}

        public static implicit operator WaterQualityManagementPlanVerifyPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifyPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifyPrimaryKey(WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            return new WaterQualityManagementPlanVerifyPrimaryKey(waterQualityManagementPlanVerify);
        }
    }
}