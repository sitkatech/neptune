using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    /// <summary>
    /// NPT-984: Manager-only authorization. Tighter than JurisdictionEditFeature — excludes
    /// JurisdictionEditor. Mirrors the frontend's
    /// AuthenticationService.doesCurrentUserHaveJurisdictionManagePermission() role set.
    /// Role check only — for a record-routed endpoint use a jurisdiction-scoped Manage variant
    /// (e.g. FieldVisitManageFeature) instead.
    /// </summary>
    public class JurisdictionManageFeature : BaseAuthorizationAttribute
    {
        public JurisdictionManageFeature() : base(new[] { RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin })
        {
        }
    }
}
