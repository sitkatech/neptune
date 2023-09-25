using Neptune.Models.DataTransferObjects;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public static class NeptunePages
    {
        public static NeptunePageDto GetByNeptunePageTypeID(NeptuneDbContext dbContext, int neptunePageID)
        {
            var neptunePage = dbContext.NeptunePages
                .Include(x => x.NeptunePageType)
                .SingleOrDefault(x => x.NeptunePageTypeID == neptunePageID);

            return neptunePage?.AsDto();
        }

        public static NeptunePageDto UpdateNeptunePage(NeptuneDbContext dbContext, int neptunePageID,
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