//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ParcelGeometry]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class ParcelGeometryExtensionMethods
    {
        public static ParcelGeometryDto AsDto(this ParcelGeometry parcelGeometry)
        {
            var parcelGeometryDto = new ParcelGeometryDto()
            {
                ParcelGeometryID = parcelGeometry.ParcelGeometryID,
                Parcel = parcelGeometry.Parcel.AsDto()
            };
            DoCustomMappings(parcelGeometry, parcelGeometryDto);
            return parcelGeometryDto;
        }

        static partial void DoCustomMappings(ParcelGeometry parcelGeometry, ParcelGeometryDto parcelGeometryDto);

        public static ParcelGeometrySimpleDto AsSimpleDto(this ParcelGeometry parcelGeometry)
        {
            var parcelGeometrySimpleDto = new ParcelGeometrySimpleDto()
            {
                ParcelGeometryID = parcelGeometry.ParcelGeometryID,
                ParcelID = parcelGeometry.ParcelID
            };
            DoCustomSimpleDtoMappings(parcelGeometry, parcelGeometrySimpleDto);
            return parcelGeometrySimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(ParcelGeometry parcelGeometry, ParcelGeometrySimpleDto parcelGeometrySimpleDto);
    }
}