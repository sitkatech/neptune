using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization;

public class SitkaAdminFeature : BaseAuthorizationAttribute
{
    public SitkaAdminFeature() : base(new []{RoleEnum.SitkaAdmin})
    {
    }
}