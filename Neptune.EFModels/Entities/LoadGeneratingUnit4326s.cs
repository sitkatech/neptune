using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class LoadGeneratingUnit4326s
{
    public static LoadGeneratingUnit4326 GetByID(NeptuneDbContext dbContext, int loadGeneratingUnit4326ID)
    {
        var loadGeneratingUnit4326 = dbContext.LoadGeneratingUnit4326s
            .AsNoTracking()
            .SingleOrDefault(x => x.LoadGeneratingUnit4326ID == loadGeneratingUnit4326ID);
        Check.RequireNotNull(loadGeneratingUnit4326, $"Load Generating Unit 4326 with ID {loadGeneratingUnit4326ID} not found!");
        return loadGeneratingUnit4326;
    }
}