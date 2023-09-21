//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPhoto]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanPhotoExtensionMethods
    {
        public static WaterQualityManagementPlanPhotoDto AsDto(this WaterQualityManagementPlanPhoto waterQualityManagementPlanPhoto)
        {
            var waterQualityManagementPlanPhotoDto = new WaterQualityManagementPlanPhotoDto()
            {
                WaterQualityManagementPlanPhotoID = waterQualityManagementPlanPhoto.WaterQualityManagementPlanPhotoID,
                FileResource = waterQualityManagementPlanPhoto.FileResource.AsDto(),
                Caption = waterQualityManagementPlanPhoto.Caption,
                UploadDate = waterQualityManagementPlanPhoto.UploadDate
            };
            DoCustomMappings(waterQualityManagementPlanPhoto, waterQualityManagementPlanPhotoDto);
            return waterQualityManagementPlanPhotoDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanPhoto waterQualityManagementPlanPhoto, WaterQualityManagementPlanPhotoDto waterQualityManagementPlanPhotoDto);

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