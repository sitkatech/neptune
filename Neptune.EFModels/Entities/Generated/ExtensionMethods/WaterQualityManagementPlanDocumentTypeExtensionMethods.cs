//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDocumentType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanDocumentTypeExtensionMethods
    {

        public static WaterQualityManagementPlanDocumentTypeSimpleDto AsSimpleDto(this WaterQualityManagementPlanDocumentType waterQualityManagementPlanDocumentType)
        {
            var waterQualityManagementPlanDocumentTypeSimpleDto = new WaterQualityManagementPlanDocumentTypeSimpleDto()
            {
                WaterQualityManagementPlanDocumentTypeID = waterQualityManagementPlanDocumentType.WaterQualityManagementPlanDocumentTypeID,
                WaterQualityManagementPlanDocumentTypeName = waterQualityManagementPlanDocumentType.WaterQualityManagementPlanDocumentTypeName,
                WaterQualityManagementPlanDocumentTypeDisplayName = waterQualityManagementPlanDocumentType.WaterQualityManagementPlanDocumentTypeDisplayName,
                IsRequired = waterQualityManagementPlanDocumentType.IsRequired
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanDocumentType, waterQualityManagementPlanDocumentTypeSimpleDto);
            return waterQualityManagementPlanDocumentTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanDocumentType waterQualityManagementPlanDocumentType, WaterQualityManagementPlanDocumentTypeSimpleDto waterQualityManagementPlanDocumentTypeSimpleDto);
    }
}