using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class OnlandVisualTrashAssessmentAreas
{
    private static IQueryable<OnlandVisualTrashAssessmentArea> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.OnlandVisualTrashAssessmentAreas
                .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
            ;
    }

    public static OnlandVisualTrashAssessmentArea GetByIDWithChangeTracking(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentAreaID)
    {
        var onlandVisualTrashAssessmentArea = GetImpl(dbContext)
            .SingleOrDefault(x => x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID);
        Check.RequireNotNull(onlandVisualTrashAssessmentArea, $"OnlandVisualTrashAssessmentArea with ID {onlandVisualTrashAssessmentAreaID} not found!");
        return onlandVisualTrashAssessmentArea;
    }

    public static OnlandVisualTrashAssessmentArea GetByIDWithChangeTracking(NeptuneDbContext dbContext, OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, onlandVisualTrashAssessmentAreaPrimaryKey.PrimaryKeyValue);
    }

    public static OnlandVisualTrashAssessmentArea GetByID(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentAreaID)
    {
        var onlandVisualTrashAssessmentArea = GetImpl(dbContext).AsNoTracking()
            .SingleOrDefault(x => x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID);
        Check.RequireNotNull(onlandVisualTrashAssessmentArea, $"OnlandVisualTrashAssessmentArea with ID {onlandVisualTrashAssessmentAreaID} not found!");
        return onlandVisualTrashAssessmentArea;
    }

    public static OnlandVisualTrashAssessmentArea GetByID(NeptuneDbContext dbContext, OnlandVisualTrashAssessmentAreaPrimaryKey onlandVisualTrashAssessmentAreaPrimaryKey)
    {
        return GetByID(dbContext, onlandVisualTrashAssessmentAreaPrimaryKey.PrimaryKeyValue);
    }

    public static List<OnlandVisualTrashAssessmentArea> List(NeptuneDbContext dbContext)
    {
        return GetImpl(dbContext).AsNoTracking().ToList().OrderBy(x => x.OnlandVisualTrashAssessmentAreaName).ToList();

    }

    public static OnlandVisualTrashAssessmentArea GetByIDForFeatureContextCheck(NeptuneDbContext dbContext, int onlandVisualTrashAssessmentAreaID)
    {
        var onlandVisualTrashAssessmentArea = dbContext.OnlandVisualTrashAssessmentAreas
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization).AsNoTracking()
            .SingleOrDefault(x => x.OnlandVisualTrashAssessmentAreaID == onlandVisualTrashAssessmentAreaID);
        Check.RequireNotNull(onlandVisualTrashAssessmentArea, $"OnlandVisualTrashAssessmentArea with ID {onlandVisualTrashAssessmentAreaID} not found!");
        return onlandVisualTrashAssessmentArea;
    }

    public static List<OnlandVisualTrashAssessmentArea> ListByStormwaterJurisdictionIDList(NeptuneDbContext dbContext, IEnumerable<int> stormwaterJurisdictionIDList)
    {
        return GetImpl(dbContext).AsNoTracking().Where(x => stormwaterJurisdictionIDList.Contains(x.StormwaterJurisdictionID)).OrderBy(x => x.OnlandVisualTrashAssessmentAreaName).ToList();
    }
}