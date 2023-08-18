//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanPermitTerm


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanPermitTermPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanPermitTerm>
    {
        public WaterQualityManagementPlanPermitTermPrimaryKey() : base(){}
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