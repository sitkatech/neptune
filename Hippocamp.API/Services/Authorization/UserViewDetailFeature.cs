using Hippocamp.EFModels.Entities;

namespace Hippocamp.API.Services.Authorization
{
    public class UserViewDetailFeature : BaseAuthorizationAttribute
    {
        public UserViewDetailFeature() : base(new[] { RoleEnum.Admin, RoleEnum.SitkaAdmin, RoleEnum.Unassigned })
        {
        }
    }
}