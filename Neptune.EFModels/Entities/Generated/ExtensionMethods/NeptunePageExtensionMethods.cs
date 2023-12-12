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
            var dto = new NeptunePageSimpleDto()
            {
                NeptunePageID = neptunePage.NeptunePageID,
                NeptunePageTypeID = neptunePage.NeptunePageTypeID,
                NeptunePageContent = neptunePage.NeptunePageContent
            };
            return dto;
        }
    }
}