//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanParcel
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanParcelPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanParcel>
    {
        public WaterQualityManagementPlanParcelPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanParcelPrimaryKey(WaterQualityManagementPlanParcel waterQualityManagementPlanParcel) : base(waterQualityManagementPlanParcel){}

        public static implicit operator WaterQualityManagementPlanParcelPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanParcelPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanParcelPrimaryKey(WaterQualityManagementPlanParcel waterQualityManagementPlanParcel)
        {
            return new WaterQualityManagementPlanParcelPrimaryKey(waterQualityManagementPlanParcel);
        }
    }
}