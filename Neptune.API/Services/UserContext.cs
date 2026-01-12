using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Services
{
    public static class UserContext
    {
        public static PersonDto GetUserAsDtoFromHttpContext(NeptuneDbContext dbContext, HttpContext httpContext)
        {
            var user = GetUserFromHttpContext(dbContext, httpContext);
            return user == null ? new PersonDto { PersonID = Person.AnonymousPersonID,
                FirstName = "Anonymous",
                LastName = "User",
                RoleID = (int) RoleEnum.Unassigned,
                CreateDate = DateTime.UtcNow,
                LastActivityDate = DateTime.UtcNow,
                IsActive = true,
                OrganizationID = -1,
                ReceiveSupportEmails = false,
                ReceiveRSBRevisionRequestEmails = false,
                WebServiceAccessToken = Guid.Empty,
                IsOCTAGrantReviewer = false
            } : user.AsDto();
        }

        public static Person GetUserFromHttpContext(NeptuneDbContext dbContext, HttpContext httpContext)
        {

            var claimsPrincipal = httpContext.User;
            if (!claimsPrincipal.Claims.Any())
            {
                return null;
            }

            var userGuid = claimsPrincipal.Claims.Single(c => c.Type == "sub").Value;
            var keystoneUser = People.GetByAuth0ID(dbContext, userGuid);

            return keystoneUser;
        }
    }
}