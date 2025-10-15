using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlans
{
    public static IQueryable<WaterQualityManagementPlan> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.WaterQualityManagementPlans
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            ;
    }

    public static WaterQualityManagementPlan GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        int waterQualityManagementPlanID)
    {
        var waterQualityManagementPlan = GetImpl(dbContext)
            .SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        Check.RequireNotNull(waterQualityManagementPlan,
            $"WaterQualityManagementPlan with ID {waterQualityManagementPlanID} not found!");
        return waterQualityManagementPlan;
    }

    public static WaterQualityManagementPlan GetByIDWithChangeTracking(NeptuneDbContext dbContext,
        WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
    {
        return GetByIDWithChangeTracking(dbContext, waterQualityManagementPlanPrimaryKey.PrimaryKeyValue);
    }

    public static WaterQualityManagementPlan GetByID(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        var waterQualityManagementPlan = GetImpl(dbContext)
            .Include(x => x.TreatmentBMPs)
            .ThenInclude(x => x.TreatmentBMPType)
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.StormwaterJurisdictionGeometry)
            .Include(x => x.HydrologicSubarea)
            .AsNoTracking()
            .SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        Check.RequireNotNull(waterQualityManagementPlan,
            $"WaterQualityManagementPlan with ID {waterQualityManagementPlanID} not found!");
        return waterQualityManagementPlan;
    }

    public static WaterQualityManagementPlan GetByID(NeptuneDbContext dbContext,
        WaterQualityManagementPlanPrimaryKey waterQualityManagementPlanPrimaryKey)
    {
        return GetByID(dbContext, waterQualityManagementPlanPrimaryKey.PrimaryKeyValue);
    }

    public static WaterQualityManagementPlan GetByIDForFeatureContextCheck(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        var waterQualityManagementPlan = dbContext.WaterQualityManagementPlans
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization).AsNoTracking()
            .SingleOrDefault(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        Check.RequireNotNull(waterQualityManagementPlan, $"WaterQualityManagementPlan with ID {waterQualityManagementPlanID} not found!");
        return waterQualityManagementPlan;
    }

    public static List<WaterQualityManagementPlan> ListViewableByPerson(NeptuneDbContext dbContext, Person person)
    {
        var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForWQMPs(dbContext, person);

        //These users can technically see all Jurisdictions, just potentially not the WQMPs inside them
        var waterQualityManagementPlans = GetImpl(dbContext)
            .AsNoTracking()
            .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID));
        if (person.IsAnonymousOrUnassigned())
        {
            var publicWaterQualityManagementPlans = waterQualityManagementPlans.Where(x =>
                x.WaterQualityManagementPlanStatusID ==
                (int)WaterQualityManagementPlanStatusEnum.Active ||
                x.WaterQualityManagementPlanStatusID ==
                (int)WaterQualityManagementPlanStatusEnum.Inactive &&
                x.StormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityTypeID ==
                (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.ActiveAndInactive).ToList();
            return publicWaterQualityManagementPlans;
        }

        return waterQualityManagementPlans.ToList();
    }


    public static IEnumerable<WaterQualityManagementPlan> ListByStormwaterJurisdictionID(NeptuneDbContext dbContext, int stormwaterJurisdictionID)
    {
        return GetImpl(dbContext)
            .AsNoTracking()
            .Where(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID).ToList();
    }

    public static async Task<List<WaterQualityManagementPlanDto>> ListAsDtoAsync(NeptuneDbContext dbContext)
    {
        var entities = await GetImpl(dbContext).AsNoTracking().ToListAsync();
        return entities.Select(x => x.AsDto()).ToList();
    }

    public static async Task<WaterQualityManagementPlanDto?> GetByIDAsDtoAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        var entity = await GetImpl(dbContext).AsNoTracking().FirstOrDefaultAsync(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        return entity?.AsDto();
    }

    public static async Task<WaterQualityManagementPlanDto> CreateAsync(NeptuneDbContext dbContext, WaterQualityManagementPlanUpsertDto dto)
    {
        var entity = dto.AsEntity();
        dbContext.WaterQualityManagementPlans.Add(entity);
        await dbContext.SaveChangesAsync();
        return await GetByIDAsDtoAsync(dbContext, entity.WaterQualityManagementPlanID);
    }

    public static async Task<WaterQualityManagementPlanDto?> UpdateAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanID, WaterQualityManagementPlanUpsertDto dto)
    {
        var entity = await dbContext.WaterQualityManagementPlans.FirstAsync(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        entity.UpdateFromUpsertDto(dto);
        await dbContext.SaveChangesAsync();
        return await GetByIDAsDtoAsync(dbContext, entity.WaterQualityManagementPlanID);
    }

    public static async Task<bool> DeleteAsync(NeptuneDbContext dbContext, int waterQualityManagementPlanID)
    {
        var entity = await dbContext.WaterQualityManagementPlans.FirstOrDefaultAsync(x => x.WaterQualityManagementPlanID == waterQualityManagementPlanID);
        if (entity == null) return false;
        await entity.DeleteFull(dbContext);
        return true;
    }

    public static async Task<List<WaterQualityManagementPlanDto>> ListWithFinalWQMPDocumentAsync(NeptuneDbContext dbContext)
    {
        // todo: filtering by a hardcoded list of WQMP IDs is temporary until we can get the full list of WQMPs that should be included
        var wqmpIDsToFilterBy = new List<int>
            {
            3066, 2845, 2856, 2857, 2850, 1623, 1632, 2528, 2531, 2343, 2527
        };
        var entities = await dbContext.WaterQualityManagementPlans
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization)
            .Include(x => x.WaterQualityManagementPlanDocuments)
            .Where(x => wqmpIDsToFilterBy.Contains(x.WaterQualityManagementPlanID) && x.WaterQualityManagementPlanDocuments
                .Any(y => y.WaterQualityManagementPlanDocumentTypeID == (int) WaterQualityManagementPlanDocumentTypeEnum.FinalWQMP))
            .AsNoTracking()
            .ToListAsync();
        return entities.Select(x => x.AsDto()).ToList();
    }
}