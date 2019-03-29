//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanLandUse
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanLandUsePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanLandUse>
    {
        public WaterQualityManagementPlanLandUsePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanLandUsePrimaryKey(WaterQualityManagementPlanLandUse waterQualityManagementPlanLandUse) : base(waterQualityManagementPlanLandUse){}

        public static implicit operator WaterQualityManagementPlanLandUsePrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanLandUsePrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanLandUsePrimaryKey(WaterQualityManagementPlanLandUse waterQualityManagementPlanLandUse)
        {
            return new WaterQualityManagementPlanLandUsePrimaryKey(waterQualityManagementPlanLandUse);
        }
    }
}