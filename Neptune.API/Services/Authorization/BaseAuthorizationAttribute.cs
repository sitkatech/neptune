﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Neptune.EFModels.Entities;

namespace Neptune.API.Services.Authorization
{
    public abstract class BaseAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IEnumerable<RoleEnum> _grantedRoles;

        protected BaseAuthorizationAttribute(IEnumerable<RoleEnum> grantedRoles)
        {
            _grantedRoles = grantedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();
                return;
            }

            var dbContextService = context.HttpContext.RequestServices.GetService(typeof(NeptuneDbContext));
            if (dbContextService == null || !(dbContextService is NeptuneDbContext dbContext))
            {
                //MK 1/16/2018 - If we don't have a repository we are in a terrible state.
                throw new ApplicationException(
                    "Could not find injected NeptuneDbRepository. OnAuthorization.cs needs your help!");
            }

            var person = UserContext.GetUserFromHttpContext(dbContext, context.HttpContext);

            var isAuthorized = person != null && (_grantedRoles.Any(x => (int) x == person.RoleID) || !_grantedRoles.Any()); // allowing an empty list lets us implement LoggedInUnclassifiedFeature easily
            
            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}
