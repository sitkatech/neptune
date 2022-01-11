using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Hippocamp.EFModels.Entities;
using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.API.Services
{
    public class UserContext
    {
        public UserDto User { get; set; }

        private UserContext(UserDto user)
        {
            User = user;
        }

        public static UserDto GetUserFromHttpContext(HippocampDbContext dbContext, HttpContext httpContext)
        {

            var claimsPrincipal = httpContext.User;
            if (!claimsPrincipal.Claims.Any())
            {
                return null;
            }

            var userGuid = Guid.Parse(claimsPrincipal.Claims.Single(c => c.Type == "sub").Value);
            var keystoneUser = Hippocamp.EFModels.Entities.User.GetByUserGuid(dbContext, userGuid);

            keystoneUser = ImpersonationService.RetrieveImpersonatedUserIfImpersonating(dbContext, keystoneUser);
            
            return keystoneUser;
        }
    }
}