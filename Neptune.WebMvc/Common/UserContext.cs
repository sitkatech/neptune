using Neptune.EFModels.Entities;
using Neptune.Models.Helpers;
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
            var globalID = claimsPrincipal.Claims.Single(c => c.Type == ClaimsConstants.Sub).Value;
            person = People.GetByGlobalID(dbContext, globalID);
        }

        return person ?? PersonModelExtensions.GetAnonymousSitkaUser();
    }
}