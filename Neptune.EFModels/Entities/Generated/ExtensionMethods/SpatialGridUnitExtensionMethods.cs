//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SpatialGridUnit]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class SpatialGridUnitExtensionMethods
    {
        public static SpatialGridUnitSimpleDto AsSimpleDto(this SpatialGridUnit spatialGridUnit)
        {
            var dto = new SpatialGridUnitSimpleDto()
            {
                SpatialGridUnitID = spatialGridUnit.SpatialGridUnitID
            };
            return dto;
        }
    }
}