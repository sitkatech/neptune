using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Models;

namespace Neptune.Web.Common;

public static class UserContext
{
    public static Person GetUserFromHttpContext(NeptuneDbContext dbContext, HttpContext httpContext)
    {
        Person? person = null;
        var claimsPrincipal = httpContext.User;
        if (claimsPrincipal.Claims.Any())
        {
            var userGuid = Guid.Parse(claimsPrincipal.Claims.Single(c => c.Type == "sub").Value);
            person = People.GetByGuid(dbContext, userGuid);
        }

        return person ?? PersonModelExtensions.GetAnonymousSitkaUser();
    }

    public static PersonDto GetUserAsDtoFromHttpContext(NeptuneDbContext dbContext, HttpContext httpContext)
    {
        var claimsPrincipal = httpContext.User;
        if (!claimsPrincipal.Claims.Any())
        {
            return null;
        }

        var userGuid = Guid.Parse(claimsPrincipal.Claims.Single(c => c.Type == "sub").Value);
        var keystoneUser = People.GetByGuidAsDto(dbContext, userGuid);

        return keystoneUser;
    }
}