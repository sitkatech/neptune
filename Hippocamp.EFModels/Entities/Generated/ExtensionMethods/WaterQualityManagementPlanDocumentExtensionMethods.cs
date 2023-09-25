//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDocument]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanDocumentExtensionMethods
    {
        public static WaterQualityManagementPlanDocumentDto AsDto(this WaterQualityManagementPlanDocument waterQualityManagementPlanDocument)
        {
            var waterQualityManagementPlanDocumentDto = new WaterQualityManagementPlanDocumentDto()
            {
                WaterQualityManagementPlanDocumentID = waterQualityManagementPlanDocument.WaterQualityManagementPlanDocumentID,
                WaterQualityManagementPlan = waterQualityManagementPlanDocument.WaterQualityManagementPlan.AsDto(),
                FileResource = waterQualityManagementPlanDocument.FileResource.AsDto(),
                DisplayName = waterQualityManagementPlanDocument.DisplayName,
                Description = waterQualityManagementPlanDocument.Description,
                UploadDate = waterQualityManagementPlanDocument.UploadDate,
                WaterQualityManagementPlanDocumentType = waterQualityManagementPlanDocument.WaterQualityManagementPlanDocumentType.AsDto()
            };
            DoCustomMappings(waterQualityManagementPlanDocument, waterQualityManagementPlanDocumentDto);
            return waterQualityManagementPlanDocumentDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanDocument waterQualityManagementPlanDocument, WaterQualityManagementPlanDocumentDto waterQualityManagementPlanDocumentDto);

        public static WaterQualityManagementPlanDocumentSimpleDto AsSimpleDto(this WaterQualityManagementPlanDocument waterQualityManagementPlanDocument)
        {
            var waterQualityManagementPlanDocumentSimpleDto = new WaterQualityManagementPlanDocumentSimpleDto()
            {
                WaterQualityManagementPlanDocumentID = waterQualityManagementPlanDocument.WaterQualityManagementPlanDocumentID,
                WaterQualityManagementPlanID = waterQualityManagementPlanDocument.WaterQualityManagementPlanID,
                FileResourceID = waterQualityManagementPlanDocument.FileResourceID,
                DisplayName = waterQualityManagementPlanDocument.DisplayName,
                Description = waterQualityManagementPlanDocument.Description,
                UploadDate = waterQualityManagementPlanDocument.UploadDate,
                WaterQualityManagementPlanDocumentTypeID = waterQualityManagementPlanDocument.WaterQualityManagementPlanDocumentTypeID
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanDocument, waterQualityManagementPlanDocumentSimpleDto);
            return waterQualityManagementPlanDocumentSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanDocument waterQualityManagementPlanDocument, WaterQualityManagementPlanDocumentSimpleDto waterQualityManagementPlanDocumentSimpleDto);
    }
}