//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyPhoto]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanVerifyPhotoExtensionMethods
    {
        public static WaterQualityManagementPlanVerifyPhotoSimpleDto AsSimpleDto(this WaterQualityManagementPlanVerifyPhoto waterQualityManagementPlanVerifyPhoto)
        {
            var dto = new WaterQualityManagementPlanVerifyPhotoSimpleDto()
            {
                WaterQualityManagementPlanVerifyPhotoID = waterQualityManagementPlanVerifyPhoto.WaterQualityManagementPlanVerifyPhotoID,
                WaterQualityManagementPlanVerifyID = waterQualityManagementPlanVerifyPhoto.WaterQualityManagementPlanVerifyID,
                WaterQualityManagementPlanPhotoID = waterQualityManagementPlanVerifyPhoto.WaterQualityManagementPlanPhotoID
            };
            return dto;
        }
    }
}