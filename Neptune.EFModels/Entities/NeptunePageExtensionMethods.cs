using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NeptunePageExtensionMethods
    {
        public static NeptunePageDto AsDto(this NeptunePage neptunePage)
        {
            var neptunePageDto = new NeptunePageDto()
            {
                NeptunePageID = neptunePage.NeptunePageID,
                NeptunePageType = neptunePage.NeptunePageType.AsSimpleDto(),
                NeptunePageContent = neptunePage.NeptunePageContent
            };
            return neptunePageDto;
        }
    }
}