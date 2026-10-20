using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    /// <summary>
    /// NPT-1129: Manager-only, jurisdiction-scoped gate for field-visit-routed endpoints. Exists so
    /// NPT-984's Manager-only rules (Delete, Verify, Mark Provisional, Return to Edit) stay
    /// Manager-only — <see cref="FieldVisitEditFeature"/> would also grant JurisdictionEditors.
    /// </summary>
    public class FieldVisitManageFeature() : JurisdictionScopedFeature(ManageRoles)
    {
        protected override string RouteKey => "fieldVisitID";

        protected override int? GetStormwaterJurisdictionID(NeptuneDbContext dbContext, int id) =>
            FieldVisits.GetStormwaterJurisdictionIDForFeatureContextCheck(dbContext, id);
    }
}
