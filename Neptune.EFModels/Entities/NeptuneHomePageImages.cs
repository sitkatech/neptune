using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class NeptuneHomePageImages
{
    public static List<NeptuneHomePageImage> List(NeptuneDbContext dbContext)
    {
        return dbContext.NeptuneHomePageImages.Include(x => x.FileResource)
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .ToList();
    }
}