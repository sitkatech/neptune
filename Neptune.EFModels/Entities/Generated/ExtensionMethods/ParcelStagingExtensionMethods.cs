//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ParcelStaging]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ParcelStagingExtensionMethods
    {
        public static ParcelStagingSimpleDto AsSimpleDto(this ParcelStaging parcelStaging)
        {
            var dto = new ParcelStagingSimpleDto()
            {
                ParcelStagingID = parcelStaging.ParcelStagingID,
                ParcelNumber = parcelStaging.ParcelNumber,
                ParcelAreaInSquareFeet = parcelStaging.ParcelAreaInSquareFeet,
                ParcelAddress = parcelStaging.ParcelAddress,
                ParcelCityState = parcelStaging.ParcelCityState,
                ParcelZipCode = parcelStaging.ParcelZipCode
            };
            return dto;
        }
    }
}