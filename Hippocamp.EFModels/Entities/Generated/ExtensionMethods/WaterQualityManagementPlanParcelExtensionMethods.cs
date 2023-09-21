//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanParcel]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class WaterQualityManagementPlanParcelExtensionMethods
    {
        public static WaterQualityManagementPlanParcelDto AsDto(this WaterQualityManagementPlanParcel waterQualityManagementPlanParcel)
        {
            var waterQualityManagementPlanParcelDto = new WaterQualityManagementPlanParcelDto()
            {
                WaterQualityManagementPlanParcelID = waterQualityManagementPlanParcel.WaterQualityManagementPlanParcelID,
                WaterQualityManagementPlan = waterQualityManagementPlanParcel.WaterQualityManagementPlan.AsDto(),
                Parcel = waterQualityManagementPlanParcel.Parcel.AsDto()
            };
            DoCustomMappings(waterQualityManagementPlanParcel, waterQualityManagementPlanParcelDto);
            return waterQualityManagementPlanParcelDto;
        }

        static partial void DoCustomMappings(WaterQualityManagementPlanParcel waterQualityManagementPlanParcel, WaterQualityManagementPlanParcelDto waterQualityManagementPlanParcelDto);

        public static WaterQualityManagementPlanParcelSimpleDto AsSimpleDto(this WaterQualityManagementPlanParcel waterQualityManagementPlanParcel)
        {
            var waterQualityManagementPlanParcelSimpleDto = new WaterQualityManagementPlanParcelSimpleDto()
            {
                WaterQualityManagementPlanParcelID = waterQualityManagementPlanParcel.WaterQualityManagementPlanParcelID,
                WaterQualityManagementPlanID = waterQualityManagementPlanParcel.WaterQualityManagementPlanID,
                ParcelID = waterQualityManagementPlanParcel.ParcelID
            };
            DoCustomSimpleDtoMappings(waterQualityManagementPlanParcel, waterQualityManagementPlanParcelSimpleDto);
            return waterQualityManagementPlanParcelSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(WaterQualityManagementPlanParcel waterQualityManagementPlanParcel, WaterQualityManagementPlanParcelSimpleDto waterQualityManagementPlanParcelSimpleDto);
    }
}