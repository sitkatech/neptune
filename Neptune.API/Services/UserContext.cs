using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Models.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

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

            var clientClaim = claimsPrincipal.FindFirst(ClaimsConstants.IsClient);
            if (clientClaim is { Value: "client-credentials" })
            {
                if (claimsPrincipal.Claims.All(c => c.Type != ClaimsConstants.ClientID))
                {
                    return null;
                }

                var clientID = claimsPrincipal.Claims.Single(c => c.Type == ClaimsConstants.ClientID).Value;
                var clientUser = People.GetByGlobalID(dbContext, clientID);
                return clientUser;
            }

            var userGlobalID = claimsPrincipal.Claims.Single(c => c.Type == ClaimsConstants.Sub).Value;
            var user = People.GetByGlobalID(dbContext, userGlobalID);
            return user;
        }
    }
}