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
            var watershedSimpleDto = new WatershedSimpleDto()
            {
                WatershedID = watershed.WatershedID,
                WatershedName = watershed.WatershedName
            };
            DoCustomSimpleDtoMappings(watershed, watershedSimpleDto);
            return watershedSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(Watershed watershed, WatershedSimpleDto watershedSimpleDto);
    }
}