using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class CustomRichTextExtensionMethods
    { 
        static partial void DoCustomMappings(CustomRichText customRichText, CustomRichTextDto customRichTextDto)
        {
            customRichTextDto.IsEmptyContent = string.IsNullOrWhiteSpace(customRichText.CustomRichTextContent);
        }
    }
}