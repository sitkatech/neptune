//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Parcel]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ParcelExtensionMethods
    {
        public static ParcelDto AsDto(this Parcel parcel)
        {
            var parcelDto = new ParcelDto()
            {
                ParcelID = parcel.ParcelID,
                ParcelNumber = parcel.ParcelNumber,
                OwnerName = parcel.OwnerName,
                ParcelStreetNumber = parcel.ParcelStreetNumber,
                ParcelAddress = parcel.ParcelAddress,
                ParcelZipCode = parcel.ParcelZipCode,
                LandUse = parcel.LandUse,
                SquareFeetHome = parcel.SquareFeetHome,
                SquareFeetLot = parcel.SquareFeetLot,
                ParcelAreaInAcres = parcel.ParcelAreaInAcres
            };
            DoCustomMappings(parcel, parcelDto);
            return parcelDto;
        }

        static partial void DoCustomMappings(Parcel parcel, ParcelDto parcelDto);

        public static ParcelSimpleDto AsSimpleDto(this Parcel parcel)
        {
            var parcelSimpleDto = new ParcelSimpleDto()
            {
                ParcelID = parcel.ParcelID,
                ParcelNumber = parcel.ParcelNumber,
                OwnerName = parcel.OwnerName,
                ParcelStreetNumber = parcel.ParcelStreetNumber,
                ParcelAddress = parcel.ParcelAddress,
                ParcelZipCode = parcel.ParcelZipCode,
                LandUse = parcel.LandUse,
                SquareFeetHome = parcel.SquareFeetHome,
                SquareFeetLot = parcel.SquareFeetLot,
                ParcelAreaInAcres = parcel.ParcelAreaInAcres
            };
            DoCustomSimpleDtoMappings(parcel, parcelSimpleDto);
            return parcelSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(Parcel parcel, ParcelSimpleDto parcelSimpleDto);
    }
}