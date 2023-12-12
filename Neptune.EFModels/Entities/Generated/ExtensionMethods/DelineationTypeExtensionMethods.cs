//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DelineationType]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class DelineationTypeExtensionMethods
    {

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