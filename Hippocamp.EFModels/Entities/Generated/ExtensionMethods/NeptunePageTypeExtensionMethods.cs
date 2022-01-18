//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageType]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class NeptunePageTypeExtensionMethods
    {
        public static NeptunePageTypeDto AsDto(this NeptunePageType neptunePageType)
        {
            var neptunePageTypeDto = new NeptunePageTypeDto()
            {
                NeptunePageTypeID = neptunePageType.NeptunePageTypeID,
                NeptunePageTypeName = neptunePageType.NeptunePageTypeName,
                NeptunePageTypeDisplayName = neptunePageType.NeptunePageTypeDisplayName
            };
            DoCustomMappings(neptunePageType, neptunePageTypeDto);
            return neptunePageTypeDto;
        }

        static partial void DoCustomMappings(NeptunePageType neptunePageType, NeptunePageTypeDto neptunePageTypeDto);

        public static NeptunePageTypeSimpleDto AsSimpleDto(this NeptunePageType neptunePageType)
        {
            var neptunePageTypeSimpleDto = new NeptunePageTypeSimpleDto()
            {
                NeptunePageTypeID = neptunePageType.NeptunePageTypeID,
                NeptunePageTypeName = neptunePageType.NeptunePageTypeName,
                NeptunePageTypeDisplayName = neptunePageType.NeptunePageTypeDisplayName
            };
            DoCustomSimpleDtoMappings(neptunePageType, neptunePageTypeSimpleDto);
            return neptunePageTypeSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(NeptunePageType neptunePageType, NeptunePageTypeSimpleDto neptunePageTypeSimpleDto);
    }
}