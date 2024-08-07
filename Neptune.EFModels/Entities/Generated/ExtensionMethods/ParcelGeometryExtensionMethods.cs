//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ParcelGeometry]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class ParcelGeometryExtensionMethods
    {
        public static ParcelGeometrySimpleDto AsSimpleDto(this ParcelGeometry parcelGeometry)
        {
            var dto = new ParcelGeometrySimpleDto()
            {
                ParcelGeometryID = parcelGeometry.ParcelGeometryID,
                ParcelID = parcelGeometry.ParcelID
            };
            return dto;
        }
    }
}