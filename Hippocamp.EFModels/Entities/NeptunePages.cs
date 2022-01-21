using Hippocamp.Models.DataTransferObjects;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    public static class NeptunePages
    {
        public static NeptunePageDto GetByNeptunePageTypeID(HippocampDbContext dbContext, int neptunePageID)
        {
            var neptunePage = dbContext.NeptunePages
                .Include(x => x.NeptunePageType)
                .SingleOrDefault(x => x.NeptunePageTypeID == neptunePageID);

            return neptunePage?.AsDto();
        }

        public static NeptunePageDto UpdateNeptunePage(HippocampDbContext dbContext, int neptunePageID,
            NeptunePageDto customRichTextUpdateDto)
        {
            var neptunePage = dbContext.NeptunePages
                .SingleOrDefault(x => x.NeptunePageTypeID == neptunePageID);

            // null check occurs in calling endpoint method.
            neptunePage.NeptunePageContent = customRichTextUpdateDto.NeptunePageContent;

            dbContext.SaveChanges();

            return neptunePage.AsDto();
        }
    }
}