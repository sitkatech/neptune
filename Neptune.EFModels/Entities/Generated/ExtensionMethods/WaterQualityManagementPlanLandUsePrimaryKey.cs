//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanLandUse


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanLandUsePrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanLandUse>
    {
        public WaterQualityManagementPlanLandUsePrimaryKey() : base(){}
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