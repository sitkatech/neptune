//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Parcel]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ParcelExtensionMethods
    {

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