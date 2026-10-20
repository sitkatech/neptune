using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    /// <summary>
    /// NPT-1129: Manager-only, jurisdiction-scoped gate for maintenance-record-routed endpoints.
    /// Exists so NPT-984's Manager-only Delete stays Manager-only —
    /// <see cref="MaintenanceRecordEditFeature"/> would also grant JurisdictionEditors.
    /// </summary>
    public class MaintenanceRecordManageFeature() : JurisdictionScopedFeature(ManageRoles)
    {
        protected override string RouteKey => "maintenanceRecordID";

        protected override int? GetStormwaterJurisdictionID(NeptuneDbContext dbContext, int id) =>
            MaintenanceRecords.GetStormwaterJurisdictionIDForFeatureContextCheck(dbContext, id);
    }
}
