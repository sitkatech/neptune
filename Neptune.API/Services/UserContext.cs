using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Services
{
    public class UserContext
    {
        public PersonDto User { get; set; }

        private UserContext(PersonDto user)
        {
            User = user;
        }

        public static PersonDto GetUserFromHttpContext(NeptuneDbContext dbContext, HttpContext httpContext)
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
}