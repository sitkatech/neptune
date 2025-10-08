//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: dbo.WaterQualityManagementPlanDocumentAssistant


namespace Neptune.EFModels.Entities
{
    public class WaterQualityManagementPlanDocumentAssistantPrimaryKey : EntityPrimaryKey<WaterQualityManagementPlanDocumentAssistant>
    {
        public WaterQualityManagementPlanDocumentAssistantPrimaryKey() : base(){}
        public WaterQualityManagementPlanDocumentAssistantPrimaryKey(int primaryKeyValue) : base(primaryKeyValue){}
        public WaterQualityManagementPlanDocumentAssistantPrimaryKey(WaterQualityManagementPlanDocumentAssistant waterQualityManagementPlanDocumentAssistant) : base(waterQualityManagementPlanDocumentAssistant){}

        public static implicit operator WaterQualityManagementPlanDocumentAssistantPrimaryKey(int primaryKeyValue)
        {
            return new WaterQualityManagementPlanDocumentAssistantPrimaryKey(primaryKeyValue);
        }

        public static implicit operator WaterQualityManagementPlanDocumentAssistantPrimaryKey(WaterQualityManagementPlanDocumentAssistant waterQualityManagementPlanDocumentAssistant)
        {
            return new WaterQualityManagementPlanDocumentAssistantPrimaryKey(waterQualityManagementPlanDocumentAssistant);
        }
    }
}