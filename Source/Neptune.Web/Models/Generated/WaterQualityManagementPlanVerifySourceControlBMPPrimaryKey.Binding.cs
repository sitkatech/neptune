//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifySourceControlBMP
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanVerifySourceControlBMP>
    {
        public WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(WaterQualityManagementPlanVerifySourceControlBMP waterQualityManagementPlanVerifySourceControlBMP) : base(waterQualityManagementPlanVerifySourceControlBMP){}

        public static implicit operator WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(WaterQualityManagementPlanVerifySourceControlBMP waterQualityManagementPlanVerifySourceControlBMP)
        {
            return new WaterQualityManagementPlanVerifySourceControlBMPPrimaryKey(waterQualityManagementPlanVerifySourceControlBMP);
        }
    }
}