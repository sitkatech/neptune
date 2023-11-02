using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public partial class LoadGeneratingUnit
{
    public async Task DeleteFull(NeptuneDbContext dbContext)
    {
        await dbContext.HRUCharacteristics.Where(x => x.LoadGeneratingUnitID == LoadGeneratingUnitID)
            .ExecuteDeleteAsync();
        await dbContext.LoadGeneratingUnits.Where(x => x.LoadGeneratingUnitID == LoadGeneratingUnitID)
            .ExecuteDeleteAsync();
    }
}