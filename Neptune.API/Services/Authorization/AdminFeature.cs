using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    public class AdminFeature : BaseAuthorizationAttribute
    {
        public AdminFeature() : base(new []{RoleEnum.Admin, RoleEnum.SitkaAdmin})
        {
        }
    }
}