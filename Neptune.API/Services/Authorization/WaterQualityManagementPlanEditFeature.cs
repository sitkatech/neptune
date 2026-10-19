using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    /// <summary>
    /// NPT-1109: entity-scoped edit gate for WQMP-routed endpoints — role check
    /// (Editor and up) plus a jurisdiction match against the routed WQMP. Mirrors
    /// <see cref="TreatmentBMPEditFeature"/>. Replaces [JurisdictionManageFeature] on the
    /// document CRUD and AI-extraction endpoints, which was (a) Manager-only, blocking
    /// JurisdictionEditors from supporting-data CRUD the JE/JM doctrine says they should
    /// have, and (b) jurisdiction-blind, letting any Manager modify any jurisdiction's
    /// WQMP documents.
    /// </summary>
    public class WaterQualityManagementPlanEditFeature() : JurisdictionScopedFeature(EditRoles)
    {
        protected override string RouteKey => "waterQualityManagementPlanID";

        protected override int? GetStormwaterJurisdictionID(NeptuneDbContext dbContext, int id) =>
            WaterQualityManagementPlans.GetByIDForFeatureContextCheck(dbContext, id)?.StormwaterJurisdictionID;
    }
}
