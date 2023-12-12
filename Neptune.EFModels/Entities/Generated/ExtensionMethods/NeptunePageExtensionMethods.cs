//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePage]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NeptunePageExtensionMethods
    {

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