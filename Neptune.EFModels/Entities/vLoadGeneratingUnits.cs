using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class vLoadGeneratingUnits
{
    public static List<vLoadGeneratingUnit> List(NeptuneDbContext dbContext)
    {
        return dbContext.vLoadGeneratingUnits.AsNoTracking().ToList();
    }
    
}