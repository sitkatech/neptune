//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanPermitTerm
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanPermitTermPrimaryKey : LtInfo.Common.EntityModelBinding.LtInfoEntityPrimaryKey<WaterQualityManagementPlanPermitTerm>
    {
        public WaterQualityManagementPlanPermitTermPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanPermitTermPrimaryKey(WaterQualityManagementPlanPermitTerm waterQualityManagementPlanPermitTerm) : base(waterQualityManagementPlanPermitTerm){}

        public static implicit operator WaterQualityManagementPlanPermitTermPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanPermitTermPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanPermitTermPrimaryKey(WaterQualityManagementPlanPermitTerm waterQualityManagementPlanPermitTerm)
        {
            return new WaterQualityManagementPlanPermitTermPrimaryKey(waterQualityManagementPlanPermitTerm);
        }
    }
}