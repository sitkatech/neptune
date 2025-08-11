using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class vLoadGeneratingUnits
{
    public static List<vLoadGeneratingUnit> List(NeptuneDbContext dbContext)
    {
        return dbContext.vLoadGeneratingUnits.AsNoTracking().ToList();
    }

    public static List<vLoadGeneratingUnit> ListByRegionalSubbasinID(NeptuneDbContext dbContext, int regionalSubbasinID)
    {
        return dbContext.vLoadGeneratingUnits.AsNoTracking().Where(x => x.RegionalSubbasinID == regionalSubbasinID)
            .ToList();
    }
    
}