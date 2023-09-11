using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using System;

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

    public static List<WaterQualityManagementPlan> ListViewableByPerson(NeptuneDbContext dbContext, Person person)
    {
        var stormwaterJurisdictionsPersonCanViewWithContext = StormwaterJurisdictions.ListViewableByPerson(dbContext, person);

        var stormwaterJurisdictionIDsPersonCanView = person.IsAnonymousOrUnassigned()
            ? stormwaterJurisdictionsPersonCanViewWithContext
                .Where(x => x.StormwaterJurisdictionPublicWQMPVisibilityTypeID !=
                            (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.None)
                .Select(x => x.StormwaterJurisdictionID)
            : stormwaterJurisdictionsPersonCanViewWithContext.Select(x => x.StormwaterJurisdictionID);

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