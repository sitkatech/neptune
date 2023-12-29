//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanVerifyPhoto


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanVerifyPhotoPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanVerifyPhoto>
    {
        public WaterQualityManagementPlanVerifyPhotoPrimaryKey() : base(){}
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