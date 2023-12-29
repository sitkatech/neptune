//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Watershed]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class WatershedExtensionMethods
    {
        public static WatershedSimpleDto AsSimpleDto(this Watershed watershed)
        {
            var dto = new WatershedSimpleDto()
            {
                WatershedID = watershed.WatershedID,
                WatershedName = watershed.WatershedName
            };
            return dto;
        }
    }
}