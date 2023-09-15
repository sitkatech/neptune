using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlans
{
    public static IQueryable<WaterQualityManagementPlan> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.WaterQualityManagementPlans
            .Include(x => x.StormwaterJurisdiction)
            .ThenInclude(x => x.Organization);
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
            .ThenInclude(x => x.StormwaterJurisdictionGeometry).AsNoTracking()
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
        var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPerson(dbContext, person);

        //These users can technically see all Jurisdictions, just potentially not the WQMPs inside them
        var waterQualityManagementPlans = GetImpl(dbContext).Include(x => x.WaterQualityManagementPlanParcels)
            .Include(x => x.TreatmentBMPs)
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


}