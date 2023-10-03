using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    public class UserViewDetailFeature : BaseAuthorizationAttribute
    {
        public UserViewDetailFeature() : base(new[] { RoleEnum.Admin, RoleEnum.SitkaAdmin, RoleEnum.Unassigned })
        {
        }
    }
}