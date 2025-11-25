//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanDocumentVectorStore


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanDocumentVectorStorePrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanDocumentVectorStore>
    {
        public WaterQualityManagementPlanDocumentVectorStorePrimaryKey() : base(){}
        public WaterQualityManagementPlanDocumentVectorStorePrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanDocumentVectorStorePrimaryKey(WaterQualityManagementPlanDocumentVectorStore waterQualityManagementPlanDocumentVectorStore) : base(waterQualityManagementPlanDocumentVectorStore){}

        public static implicit operator WaterQualityManagementPlanDocumentVectorStorePrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanDocumentVectorStorePrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanDocumentVectorStorePrimaryKey(WaterQualityManagementPlanDocumentVectorStore waterQualityManagementPlanDocumentVectorStore)
        {
            return new WaterQualityManagementPlanDocumentVectorStorePrimaryKey(waterQualityManagementPlanDocumentVectorStore);
        }
    }
}