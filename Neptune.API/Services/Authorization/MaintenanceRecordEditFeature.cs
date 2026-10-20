using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    /// <summary>
    /// NPT-1129: entity-scoped edit gate for maintenance-record-routed endpoints — Editor and up,
    /// plus a jurisdiction match against the routed maintenance record's BMP.
    /// </summary>
    public class MaintenanceRecordEditFeature() : JurisdictionScopedFeature(EditRoles)
    {
        protected override string RouteKey => "maintenanceRecordID";

        protected override int? GetStormwaterJurisdictionID(NeptuneDbContext dbContext, int id) =>
            MaintenanceRecords.GetStormwaterJurisdictionIDForFeatureContextCheck(dbContext, id);
    }
}
