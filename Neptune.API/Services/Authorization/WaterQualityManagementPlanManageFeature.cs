using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    /// <summary>
    /// NPT-1129: Manager-only, jurisdiction-scoped gate for WQMP-routed endpoints. Exists so
    /// NPT-984's Manager-only rules stay Manager-only — <see cref="WaterQualityManagementPlanEditFeature"/>
    /// would also grant JurisdictionEditors. Used for deleting a verification (PO decision on NPT-1129).
    /// </summary>
    public class WaterQualityManagementPlanManageFeature() : JurisdictionScopedFeature(ManageRoles)
    {
        protected override string RouteKey => "waterQualityManagementPlanID";

        protected override int? GetStormwaterJurisdictionID(NeptuneDbContext dbContext, int id) =>
            WaterQualityManagementPlans.GetByIDForFeatureContextCheck(dbContext, id)?.StormwaterJurisdictionID;
    }
}
