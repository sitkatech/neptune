using Hippocamp.EFModels.Entities;

namespace Hippocamp.API.Services.Authorization
{
    public class JurisdictionEditFeature : BaseAuthorizationAttribute
    {
        public JurisdictionEditFeature() : base(new[]{RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor, RoleEnum.Admin, RoleEnum.SitkaAdmin})
        {
        }
    }
}