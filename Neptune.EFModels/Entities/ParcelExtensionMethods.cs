using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class ParcelExtensionMethods
{
    public static ParcelSimpleDto AsSimpleDto(this Parcel parcel)
    {
        var dto = new ParcelSimpleDto()
        {
            ParcelID = parcel.ParcelID,
            ParcelNumber = parcel.ParcelNumber,
            ParcelAddress = parcel.ParcelAddress,
            ParcelCityState = parcel.ParcelCityState,
            ParcelZipCode = parcel.ParcelZipCode,
            ParcelAreaInAcres = parcel.ParcelAreaInAcres,
            LastUpdate = parcel.LastUpdate
        };
        return dto;
    }
}