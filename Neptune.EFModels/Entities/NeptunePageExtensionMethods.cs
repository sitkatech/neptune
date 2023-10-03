using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class NeptunePageExtensionMethods
    { 
        static partial void DoCustomMappings(NeptunePage neptunePage, NeptunePageDto neptunePageDto)
        {
            neptunePageDto.IsEmptyContent = string.IsNullOrWhiteSpace(neptunePage.NeptunePageContent);
        }
    }
}