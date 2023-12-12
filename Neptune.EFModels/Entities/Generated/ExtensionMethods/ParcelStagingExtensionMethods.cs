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
            var parcelStagingSimpleDto = new ParcelStagingSimpleDto()
            {
                ParcelStagingID = parcelStaging.ParcelStagingID,
                ParcelNumber = parcelStaging.ParcelNumber,
                ParcelStagingAreaSquareFeet = parcelStaging.ParcelStagingAreaSquareFeet,
                OwnerName = parcelStaging.OwnerName,
                ParcelStreetNumber = parcelStaging.ParcelStreetNumber,
                ParcelAddress = parcelStaging.ParcelAddress,
                ParcelZipCode = parcelStaging.ParcelZipCode,
                LandUse = parcelStaging.LandUse,
                SquareFeetHome = parcelStaging.SquareFeetHome,
                SquareFeetLot = parcelStaging.SquareFeetLot,
                UploadedByPersonID = parcelStaging.UploadedByPersonID
            };
            DoCustomSimpleDtoMappings(parcelStaging, parcelStagingSimpleDto);
            return parcelStagingSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ParcelStaging parcelStaging, ParcelStagingSimpleDto parcelStagingSimpleDto);
    }
}