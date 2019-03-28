//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanPhoto
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanPhotoPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanPhoto>
    {
        public WaterQualityManagementPlanPhotoPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanPhotoPrimaryKey(WaterQualityManagementPlanPhoto waterQualityManagementPlanPhoto) : base(waterQualityManagementPlanPhoto){}

        public static implicit operator WaterQualityManagementPlanPhotoPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanPhotoPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanPhotoPrimaryKey(WaterQualityManagementPlanPhoto waterQualityManagementPlanPhoto)
        {
            return new WaterQualityManagementPlanPhotoPrimaryKey(waterQualityManagementPlanPhoto);
        }
    }
}