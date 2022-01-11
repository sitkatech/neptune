using Hippocamp.Models.DataTransferObjects;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public partial class CustomRichText
    {
        public static CustomRichTextDto GetByCustomRichTextTypeID(HippocampDbContext dbContext, int customRichTextTypeID)
        {
            var customRichText = dbContext.CustomRichTexts
                .Include(x => x.CustomRichTextType)
                .SingleOrDefault(x => x.CustomRichTextTypeID == customRichTextTypeID);

            return customRichText?.AsDto();
        }

        public static CustomRichTextDto UpdateCustomRichText(HippocampDbContext dbContext, int customRichTextTypeID,
            CustomRichTextDto customRichTextUpdateDto)
        {
            var customRichText = dbContext.CustomRichTexts
                .SingleOrDefault(x => x.CustomRichTextTypeID == customRichTextTypeID);

            // null check occurs in calling endpoint method.
            customRichText.CustomRichTextContent = customRichTextUpdateDto.CustomRichTextContent;

            dbContext.SaveChanges();

            return customRichText.AsDto();
        }
    }
}