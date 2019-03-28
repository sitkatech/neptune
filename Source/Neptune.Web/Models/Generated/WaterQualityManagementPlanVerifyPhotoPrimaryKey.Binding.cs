//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifyPhoto
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyPhotoPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanVerifyPhoto>
    {
        public WaterQualityManagementPlanVerifyPhotoPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanVerifyPhotoPrimaryKey(WaterQualityManagementPlanVerifyPhoto waterQualityManagementPlanVerifyPhoto) : base(waterQualityManagementPlanVerifyPhoto){}

        public static implicit operator WaterQualityManagementPlanVerifyPhotoPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanVerifyPhotoPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanVerifyPhotoPrimaryKey(WaterQualityManagementPlanVerifyPhoto waterQualityManagementPlanVerifyPhoto)
        {
            return new WaterQualityManagementPlanVerifyPhotoPrimaryKey(waterQualityManagementPlanVerifyPhoto);
        }
    }
}