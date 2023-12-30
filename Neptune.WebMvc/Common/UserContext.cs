using Neptune.EFModels.Entities;
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
            var userGuid = Guid.Parse(claimsPrincipal.Claims.Single(x => x.Type == "sub").Value);
            person = People.GetByGuid(dbContext, userGuid);
            if (person != null)
            {
                person.LastActivityDate = DateTime.UtcNow;
                dbContext.SaveChanges();
            }
        }

        return person ?? PersonModelExtensions.GetAnonymousSitkaUser();
    }
}