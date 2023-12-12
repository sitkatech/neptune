//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPhoto]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanPhotoExtensionMethods
    {

        public static WaterQualityManagementPlanPhotoSimpleDto AsSimpleDto(this WaterQualityManagementPlanPhoto waterQualityManagementPlanPhoto)
        {
            var waterQualityManagementPlanPhotoSimpleDto = new WaterQualityManagementPlanPhotoSimpleDto()
            {
                WaterQualityManagementPlanPhotoID = waterQualityManagementPlanPhoto.WaterQualityManagementPlanPhotoID,
                FileResourceID = waterQualityManagementPlanPhoto.FileResourceID,
                Caption = waterQualityManagementPlanPhoto.Caption,
                UploadDate = waterQualityManagementPlanPhoto.UploadDate
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanPhoto, waterQualityManagementPlanPhotoSimpleDto);
            return waterQualityManagementPlanPhotoSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanPhoto waterQualityManagementPlanPhoto, WaterQualityManagementPlanPhotoSimpleDto waterQualityManagementPlanPhotoSimpleDto);
    }
}