//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifyQuickBMP
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyQuickBMPPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanVerifyQuickBMP>
    {
        public WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMP) : base(waterQualityManagementPlanVerifyQuickBMP){}

        public static implicit operator WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(WaterQualityManagementPlanVerifyQuickBMP waterQualityManagementPlanVerifyQuickBMP)
        {
            return new WaterQualityManagementPlanVerifyQuickBMPPrimaryKey(waterQualityManagementPlanVerifyQuickBMP);
        }
    }
}