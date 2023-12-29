using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class LandUseBlockStagings
{
    public static IQueryable<LandUseBlockStaging> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.LandUseBlockStagings;
    }

    public static LandUseBlockStaging GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int landUseBlockStagingID)
    {
        var landUseBlockStaging = GetImpl(dbContext)
            .SingleOrDefault(x => x.LandUseBlockStagingID == landUseBlockStagingID);
        Check.RequireNotNull(landUseBlockStaging,
            $"LandUseBlockStaging with ID {landUseBlockStagingID} not found!");
        return landUseBlockStaging;
    }

    public static LandUseBlockStaging GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        LandUseBlockStagingPrimaryKey landUseBlockStagingPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, landUseBlockStagingPrimaryKey.PrimaryKeyValue);
    }

    public static LandUseBlockStaging GetByID(NeptuneDbContext dbContext, int landUseBlockStagingID)
    {
        var landUseBlockStaging = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.LandUseBlockStagingID == landUseBlockStagingID);
        Check.RequireNotNull(landUseBlockStaging,
            $"LandUseBlockStaging with ID {landUseBlockStagingID} not found!");
        return landUseBlockStaging;
    }

    public static LandUseBlockStaging GetByID(NeptuneDbContext dbContext,
        LandUseBlockStagingPrimaryKey landUseBlockStagingPrimaryKey)
    {
        return GetByID(dbContext, landUseBlockStagingPrimaryKey.PrimaryKeyValue);
    }

    public static List<LandUseBlockStaging> ListByPersonID(NeptuneDbContext dbContext, int personID)
    {
        return GetImpl(dbContext).AsNoTracking()
            .Where(x => x.UploadedByPersonID == personID).ToList();
    }

    public static List<LandUseBlockStaging> ListByPersonIDWithChangeTracking(NeptuneDbContext dbContext, int personID)
    {
        return GetImpl(dbContext).Where(x => x.UploadedByPersonID == personID).ToList();
    }
}