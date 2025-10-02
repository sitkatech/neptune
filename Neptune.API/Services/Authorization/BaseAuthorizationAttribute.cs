using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    public abstract class BaseAuthorizationAttribute(IEnumerable<RoleEnum> grantedRoles)
        : AuthorizeAttribute, IAuthorizationFilter
    {
        public int Order { get; set; } = 0; // Default order, higher than EntityNotFoundAttribute

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return;
            }

            var dbContextService = context.HttpContext.RequestServices.GetService(typeof(NeptuneDbContext));
            if (dbContextService == null || !(dbContextService is NeptuneDbContext dbContext))
            {
                throw new ApplicationException(
                    "Could not find injected NeptuneDbRepository. OnAuthorization.cs needs your help!");
            }

            var person = UserContext.GetUserFromHttpContext(dbContext, context.HttpContext);

            var isAuthorized = person != null && (grantedRoles.Any(x => (int)x == person.RoleID) || !grantedRoles.Any());
            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }

            // Call extension point for entity/context logic
            OnAuthorizationCore(context, dbContext, person);
        }

        // Extension point for derived classes
        protected virtual void OnAuthorizationCore(AuthorizationFilterContext context, NeptuneDbContext dbContext, Person? person)
        {
            // Default: do nothing
        }
    }
}
