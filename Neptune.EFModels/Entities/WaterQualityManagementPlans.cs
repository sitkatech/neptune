using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using System;

namespace Neptune.EFModels.Entities;

public static class WaterQualityManagementPlans
{
    public static IQueryable<WaterQualityManagementPlan> GetImpl(NeptuneDbContext dbContext)
    {
        return dbContext.WaterQualityManagementPlans
            .Include(x => x.TreatmentBMPs)
            .Include(x => x.WaterQualityManagementPlanVerifies)
            .Include(x => x.WaterQualityManagementPlanParcels)
            .ThenInclude(x => x.Parcel)
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
}