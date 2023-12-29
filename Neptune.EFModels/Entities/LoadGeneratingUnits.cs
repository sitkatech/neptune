using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class LoadGeneratingUnits
{
    private static IQueryable<RegionalSubbasin> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.RegionalSubbasins
            ;
    }

    public static List<LoadGeneratingUnit> ListUpdateCandidates(NeptuneDbContext dbContext)
    {
        return dbContext.LoadGeneratingUnits
            .Include(x => x.RegionalSubbasin)
            .Include(x => x.HRUCharacteristics)
            .Where(x =>
                x.RegionalSubbasin != null 
                && !(x.HRUCharacteristics.Any() || x.IsEmptyResponseFromHRUService == true) 
                && x.LoadGeneratingUnitGeometry.Area >= 10)
            .ToList();
    }
}