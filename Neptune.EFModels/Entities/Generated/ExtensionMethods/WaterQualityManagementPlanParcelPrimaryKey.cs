//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanParcel


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanParcelPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanParcel>
    {
        public WaterQualityManagementPlanParcelPrimaryKey() : base(){}
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