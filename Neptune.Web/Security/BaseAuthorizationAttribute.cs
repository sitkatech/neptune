﻿/*-----------------------------------------------------------------------
<copyright file="RelyingPartyAuthorizeAttribute.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;

namespace Neptune.Web.Security
{
    /// <summary>
    /// Prevent unauthorized access, unless it has been specifically allowed using AllowAnonymousAttribute
    /// </summary>
    public abstract class BaseAuthorizationAttribute : Attribute, IAuthorizationFilter
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
                throw new ApplicationException(
                    "Could not find injected DbRepository. Can't check rights appropriately!");
            }

            var userDto = UserContext.GetUserFromHttpContext(dbContext, context.HttpContext);

            var isAuthorized = userDto != null && (_grantedRoles.Any(x => (int)x == userDto.Role.RoleID) || !_grantedRoles.Any()); // allowing an empty list lets us implement LoggedInUnclassifiedFeature easily

            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            }
        }


        //public override void OnAuthorization(AuthorizationFilterContext context)
        //{
        //    var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AnonymousUnclassifiedFeature>().Any();
        //    if (allowAnonymous)
        //        return;

        //    var neptuneBaseFeatureAttribute = context.ActionDescriptor.EndpointMetadata.OfType<NeptuneBaseFeature>().SingleOrDefault();
        //    if (neptuneBaseFeatureAttribute != null && neptuneBaseFeatureAttribute.GrantedRoles.Any())
        //    {
        //        base.OnAuthorization(context);
        //    }
        //}
    }
}
