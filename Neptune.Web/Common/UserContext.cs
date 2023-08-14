using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.Web.Common;

public static class UserContext
{
    public static Person GetUserFromHttpContext(NeptuneDbContext dbContext, HttpContext httpContext)
    {
        var claimsPrincipal = httpContext.User;
        if (!claimsPrincipal.Claims.Any())
        {
            return null;
        }

        var userGuid = Guid.Parse(claimsPrincipal.Claims.Single(c => c.Type == "sub").Value);
        var keystoneUser = People.GetByPersonGuid(dbContext, userGuid);

        return keystoneUser;
    }

    public static PersonDto GetUserAsDtoFromHttpContext(NeptuneDbContext dbContext, HttpContext httpContext)
    {
        var claimsPrincipal = httpContext.User;
        if (!claimsPrincipal.Claims.Any())
        {
            return null;
        }

        var userGuid = Guid.Parse(claimsPrincipal.Claims.Single(c => c.Type == "sub").Value);
        var keystoneUser = People.GetByPersonGuidAsDto(dbContext, userGuid);

        return keystoneUser;
    }
}