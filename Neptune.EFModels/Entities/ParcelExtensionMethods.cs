using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class ParcelExtensionMethods
{
    public static ParcelDisplayDto AsDisplayDto(this Parcel parcel)
    {
        var parcelDisplayDto = new ParcelDisplayDto()
        {
            ParcelID = parcel.ParcelID,
            ParcelNumber = parcel.ParcelNumber
        };
        return parcelDisplayDto;
    }
}