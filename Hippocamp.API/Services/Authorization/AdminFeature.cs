using Hippocamp.EFModels.Entities;

namespace Hippocamp.API.Services.Authorization
{
    public class AdminFeature : BaseAuthorizationAttribute
    {
        public AdminFeature() : base(new []{RoleEnum.Admin, RoleEnum.SitkaAdmin})
        {
        }
    }
}