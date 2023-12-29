//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationOverlap]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class DelineationOverlapExtensionMethods
    {
        public static DelineationOverlapSimpleDto AsSimpleDto(this DelineationOverlap delineationOverlap)
        {
            var dto = new DelineationOverlapSimpleDto()
            {
                DelineationOverlapID = delineationOverlap.DelineationOverlapID,
                DelineationID = delineationOverlap.DelineationID,
                OverlappingDelineationID = delineationOverlap.OverlappingDelineationID
            };
            return dto;
        }
    }
}