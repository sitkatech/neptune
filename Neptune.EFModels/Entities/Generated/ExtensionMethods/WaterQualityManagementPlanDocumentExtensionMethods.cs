//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDocument]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanDocumentExtensionMethods
    {

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