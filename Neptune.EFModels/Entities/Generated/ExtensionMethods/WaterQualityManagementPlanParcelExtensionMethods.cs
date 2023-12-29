//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanParcel]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanParcelExtensionMethods
    {
        public static WaterQualityManagementPlanParcelSimpleDto AsSimpleDto(this WaterQualityManagementPlanParcel waterQualityManagementPlanParcel)
        {
            var dto = new WaterQualityManagementPlanParcelSimpleDto()
            {
                WaterQualityManagementPlanParcelID = waterQualityManagementPlanParcel.WaterQualityManagementPlanParcelID,
                WaterQualityManagementPlanID = waterQualityManagementPlanParcel.WaterQualityManagementPlanID,
                ParcelID = waterQualityManagementPlanParcel.ParcelID
            };
            return dto;
        }
    }
}