//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifyType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanVerifyType>
    {
        public WaterQualityManagementPlanVerifyTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifyTypePrimaryKey(WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyType) : base(waterQualityManagementPlanVerifyType){}

        public static implicit operator WaterQualityManagementPlanVerifyTypePrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifyTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifyTypePrimaryKey(WaterQualityManagementPlanVerifyType waterQualityManagementPlanVerifyType)
        {
            return new WaterQualityManagementPlanVerifyTypePrimaryKey(waterQualityManagementPlanVerifyType);
        }
    }
}