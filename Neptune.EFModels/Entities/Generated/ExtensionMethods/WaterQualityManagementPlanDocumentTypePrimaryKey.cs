//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanDocumentType


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanDocumentTypePrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanDocumentType>
    {
        public WaterQualityManagementPlanDocumentTypePrimaryKey() : base(){}
        public WaterQualityManagementPlanDocumentTypePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanDocumentTypePrimaryKey(WaterQualityManagementPlanDocumentType waterQualityManagementPlanDocumentType) : base(waterQualityManagementPlanDocumentType){}

        public static implicit operator WaterQualityManagementPlanDocumentTypePrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanDocumentTypePrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanDocumentTypePrimaryKey(WaterQualityManagementPlanDocumentType waterQualityManagementPlanDocumentType)
        {
            return new WaterQualityManagementPlanDocumentTypePrimaryKey(waterQualityManagementPlanDocumentType);
        }
    }
}