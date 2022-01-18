//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class DelineationTypeExtensionMethods
    {
        public static DelineationTypeDto AsDto(this DelineationType delineationType)
        {
            var delineationTypeDto = new DelineationTypeDto()
            {
                DelineationTypeID = delineationType.DelineationTypeID,
                DelineationTypeName = delineationType.DelineationTypeName,
                DelineationTypeDisplayName = delineationType.DelineationTypeDisplayName
            };
            DoCustomMappings(delineationType, delineationTypeDto);
            return delineationTypeDto;
        }

        static partial void DoCustomMappings(DelineationType delineationType, DelineationTypeDto delineationTypeDto);

        public static DelineationTypeSimpleDto AsSimpleDto(this DelineationType delineationType)
        {
            var delineationTypeSimpleDto = new DelineationTypeSimpleDto()
            {
                DelineationTypeID = delineationType.DelineationTypeID,
                DelineationTypeName = delineationType.DelineationTypeName,
                DelineationTypeDisplayName = delineationType.DelineationTypeDisplayName
            };
            DoCustomSimpleDtoMappings(delineationType, delineationTypeSimpleDto);
            return delineationTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(DelineationType delineationType, DelineationTypeSimpleDto delineationTypeSimpleDto);
    }
}