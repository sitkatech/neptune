using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Common;

public static class UserContext
{
    public static Person GetUserFromHttpContext(NeptuneDbContext dbContext, HttpContext httpContext)
    {
        Person? person = null;
        var claimsPrincipal = httpContext.User;
        if (claimsPrincipal.Claims.Any())
        {
            var auth0ID = claimsPrincipal.Claims.Single(x => x.Type == "sub").Value;
            person = People.GetByAuth0ID(dbContext, auth0ID);
        }

        return person ?? PersonModelExtensions.GetAnonymousSitkaUser();
    }
}