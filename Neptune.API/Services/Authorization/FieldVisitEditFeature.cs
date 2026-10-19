using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    /// <summary>
    /// NPT-1129: entity-scoped edit gate for field-visit-routed endpoints — Editor and up, plus a
    /// jurisdiction match against the routed field visit's BMP.
    /// </summary>
    public class FieldVisitEditFeature() : JurisdictionScopedFeature(EditRoles)
    {
        protected override string RouteKey => "fieldVisitID";

        protected override int? GetStormwaterJurisdictionID(NeptuneDbContext dbContext, int id) =>
            FieldVisits.GetStormwaterJurisdictionIDForFeatureContextCheck(dbContext, id);
    }
}
