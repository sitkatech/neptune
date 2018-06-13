//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanDevelopmentType
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanDevelopmentTypePrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanDevelopmentType>
    {
        public WaterQualityManagementPlanDevelopmentTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanDevelopmentTypePrimaryKey(WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentType) : base(waterQualityManagementPlanDevelopmentType){}

        public static implicit operator WaterQualityManagementPlanDevelopmentTypePrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanDevelopmentTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanDevelopmentTypePrimaryKey(WaterQualityManagementPlanDevelopmentType waterQualityManagementPlanDevelopmentType)
        {
            return new WaterQualityManagementPlanDevelopmentTypePrimaryKey(waterQualityManagementPlanDevelopmentType);
        }
    }
}