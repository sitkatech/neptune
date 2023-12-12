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
            var delineationOverlapSimpleDto = new DelineationOverlapSimpleDto()
            {
                DelineationOverlapID = delineationOverlap.DelineationOverlapID,
                DelineationID = delineationOverlap.DelineationID,
                OverlappingDelineationID = delineationOverlap.OverlappingDelineationID
            };
            DoCustomSimpleDtoMappings(delineationOverlap, delineationOverlapSimpleDto);
            return delineationOverlapSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(DelineationOverlap delineationOverlap, DelineationOverlapSimpleDto delineationOverlapSimpleDto);
    }
}