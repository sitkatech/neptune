//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyPhoto]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifyPhotoExtensionMethods
    {
        public static WaterQualityManagementPlanVerifyPhotoDto AsDto(this WaterQualityManagementPlanVerifyPhoto waterQualityManagementPlanVerifyPhoto)
        {
            var waterQualityManagementPlanVerifyPhotoDto = new WaterQualityManagementPlanVerifyPhotoDto()
            {
                WaterQualityManagementPlanVerifyPhotoID = waterQualityManagementPlanVerifyPhoto.WaterQualityManagementPlanVerifyPhotoID,
                WaterQualityManagementPlanVerify = waterQualityManagementPlanVerifyPhoto.WaterQualityManagementPlanVerify.AsDto(),
                WaterQualityManagementPlanPhoto = waterQualityManagementPlanVerifyPhoto.WaterQualityManagementPlanPhoto.AsDto()
            };
            DoCustomMappings(waterQualityManagementPlanVerifyPhoto, waterQualityManagementPlanVerifyPhotoDto);
            return waterQualityManagementPlanVerifyPhotoDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanVerifyPhoto waterQualityManagementPlanVerifyPhoto, WaterQualityManagementPlanVerifyPhotoDto waterQualityManagementPlanVerifyPhotoDto);

        public static WaterQualityManagementPlanVerifyPhotoSimpleDto AsSimpleDto(this WaterQualityManagementPlanVerifyPhoto waterQualityManagementPlanVerifyPhoto)
        {
            var waterQualityManagementPlanVerifyPhotoSimpleDto = new WaterQualityManagementPlanVerifyPhotoSimpleDto()
            {
                WaterQualityManagementPlanVerifyPhotoID = waterQualityManagementPlanVerifyPhoto.WaterQualityManagementPlanVerifyPhotoID,
                WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyPhoto.WaterQualityManagementPlanVerifyID,
                WaterQualityManagementPlanPhotoID = waterQualityManagementPlanVerifyPhoto.WaterQualityManagementPlanPhotoID
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanVerifyPhoto, waterQualityManagementPlanVerifyPhotoSimpleDto);
            return waterQualityManagementPlanVerifyPhotoSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanVerifyPhoto waterQualityManagementPlanVerifyPhoto, WaterQualityManagementPlanVerifyPhotoSimpleDto waterQualityManagementPlanVerifyPhotoSimpleDto);
    }
}