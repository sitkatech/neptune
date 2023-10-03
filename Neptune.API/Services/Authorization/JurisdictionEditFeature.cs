using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    public class JurisdictionEditFeature : BaseAuthorizationAttribute
    {
        public JurisdictionEditFeature() : base(new[]{RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor, RoleEnum.Admin, RoleEnum.SitkaAdmin})
        {
        }
    }
}