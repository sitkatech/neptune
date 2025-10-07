using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Models.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            .Include(x => x.HRULog)
            .Include(x => x.RegionalSubbasin)
            .Include(x => x.WaterQualityManagementPlan)
            .Include(x => x.Delineation)
            .ThenInclude(x => x.TreatmentBMP)
            .AsNoTracking()
            .SingleOrDefault(x => x.LoadGeneratingUnitID == loadGeneratingUnitID);
        Check.RequireNotNull(loadGeneratingUnit, $"Load Generating Unit with ID {loadGeneratingUnitID} not found!");
        return loadGeneratingUnit;
    }

    public static async Task<List<LoadGeneratingUnitGridDto>> ListAsGridDtoAsync(NeptuneDbContext dbContext)
    {
        var entities = await dbContext.LoadGeneratingUnits
            .Include(x => x.RegionalSubbasin)
            .Include(x => x.WaterQualityManagementPlan)
            .Include(x => x.Delineation)
            .ThenInclude(x => x.TreatmentBMP)
            .AsNoTracking().ToListAsync();
        return entities.Select(x => x.AsGridDto()).ToList();
    }

    public static async Task<LoadGeneratingUnitDto?> GetByIDAsDtoAsync(NeptuneDbContext dbContext, int loadGeneratingUnitID)
    {
        var entity = await dbContext.LoadGeneratingUnits
            .Include(x => x.RegionalSubbasin)
            .Include(x => x.WaterQualityManagementPlan)
            .Include(x => x.Delineation)
            .ThenInclude(x => x.TreatmentBMP)
            .Include(x => x.HRULog)
            .AsNoTracking().FirstOrDefaultAsync(x => x.LoadGeneratingUnitID == loadGeneratingUnitID);
        return entity?.AsDto();
    }

    public static async Task<List<LoadGeneratingUnitGridDto>> ListByRegionalSubbasinAsGridDtoAsync(NeptuneDbContext dbContext, int regionalSubbasinID)
    {
        var entities = await dbContext.LoadGeneratingUnits
            .Include(x => x.RegionalSubbasin)
            .Include(x => x.WaterQualityManagementPlan)
            .Include(x => x.Delineation)
            .ThenInclude(x => x.TreatmentBMP)
            .AsNoTracking().Where(x => x.RegionalSubbasinID == regionalSubbasinID).ToListAsync();
        return entities.Select(x => x.AsGridDto()).ToList();
    }
}