//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationOverlap]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class DelineationOverlapExtensionMethods
    {
        public static DelineationOverlapDto AsDto(this DelineationOverlap delineationOverlap)
        {
            var delineationOverlapDto = new DelineationOverlapDto()
            {
                DelineationOverlapID = delineationOverlap.DelineationOverlapID,
                Delineation = delineationOverlap.Delineation.AsDto(),
                OverlappingDelineation = delineationOverlap.OverlappingDelineation.AsDto()
            };
            DoCustomMappings(delineationOverlap, delineationOverlapDto);
            return delineationOverlapDto;
        }

        static partial void DoCustomMappings(DelineationOverlap delineationOverlap, DelineationOverlapDto delineationOverlapDto);

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