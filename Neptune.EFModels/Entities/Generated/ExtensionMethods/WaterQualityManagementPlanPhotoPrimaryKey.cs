//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanPhoto


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanPhotoPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanPhoto>
    {
        public WaterQualityManagementPlanPhotoPrimaryKey() : base(){}
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