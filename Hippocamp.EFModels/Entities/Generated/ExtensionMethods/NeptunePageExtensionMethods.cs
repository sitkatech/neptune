//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePage]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class NeptunePageExtensionMethods
    {
        public static NeptunePageDto AsDto(this NeptunePage neptunePage)
        {
            var neptunePageDto = new NeptunePageDto()
            {
                NeptunePageID = neptunePage.NeptunePageID,
                NeptunePageType = neptunePage.NeptunePageType.AsDto(),
                NeptunePageContent = neptunePage.NeptunePageContent
            };
            DoCustomMappings(neptunePage, neptunePageDto);
            return neptunePageDto;
        }

        static partial void DoCustomMappings(NeptunePage neptunePage, NeptunePageDto neptunePageDto);

        public static NeptunePageSimpleDto AsSimpleDto(this NeptunePage neptunePage)
        {
            var neptunePageSimpleDto = new NeptunePageSimpleDto()
            {
                NeptunePageID = neptunePage.NeptunePageID,
                NeptunePageTypeID = neptunePage.NeptunePageTypeID,
                NeptunePageContent = neptunePage.NeptunePageContent
            };
            DoCustomSimpleDtoMappings(neptunePage, neptunePageSimpleDto);
            return neptunePageSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(NeptunePage neptunePage, NeptunePageSimpleDto neptunePageSimpleDto);
    }
}