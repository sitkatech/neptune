using Hippocamp.EFModels.Entities;

namespace Hippocamp.API.Services.Authorization
{
    public class JurisdictionManagerOrEditorFeature : BaseAuthorizationAttribute
    {
        public JurisdictionManagerOrEditorFeature() : base(new[]{RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor})
        {
        }
    }
}