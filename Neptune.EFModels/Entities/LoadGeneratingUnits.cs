using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class LoadGeneratingUnits
{
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

    public static LoadGeneratingUnit GetByID(NeptuneDbContext dbContext, LoadGeneratingUnitPrimaryKey loadGeneratingUnitPrimaryKey)
    {
        return GetByID(dbContext, loadGeneratingUnitPrimaryKey.PrimaryKeyValue);
    }

    public static LoadGeneratingUnit GetByID(NeptuneDbContext dbContext, int loadGeneratingUnitID)
    {
        var loadGeneratingUnit = dbContext.LoadGeneratingUnits
            .Include(x => x.RegionalSubbasin)
            .Include(x => x.WaterQualityManagementPlan)
            .Include(x => x.Delineation)
            .ThenInclude(x => x.TreatmentBMP)
            .AsNoTracking()
            .SingleOrDefault(x => x.LoadGeneratingUnitID == loadGeneratingUnitID);
        Check.RequireNotNull(loadGeneratingUnit, $"Load Generating Unit with ID {loadGeneratingUnitID} not found!");
        return loadGeneratingUnit;
    }
}