using System;
using System.Linq;
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
                PersonGuid = Guid.Empty,
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

            var userGuid = Guid.Parse(claimsPrincipal.Claims.Single(c => c.Type == "sub").Value);
            var keystoneUser = People.GetByGuid(dbContext, userGuid);

            return keystoneUser;
        }
    }
}