/*-----------------------------------------------------------------------
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
            if (!context.HttpContext.User.Identity?.IsAuthenticated ?? false)
            {
                var redirectToLogin = new RedirectResult(NeptuneHelpers.GenerateLogInUrlWithReturnUrl(context.HttpContext));
                context.Result = redirectToLogin;
                return;
            }

            var dbContextService = context.HttpContext.RequestServices.GetService(typeof(NeptuneDbContext));
            if (dbContextService == null || !(dbContextService is NeptuneDbContext dbContext))
            {
                throw new ApplicationException(
                    "Could not find injected DbRepository. Can't check rights appropriately!");
            }

            var person = UserContext.GetUserFromHttpContext(dbContext, context.HttpContext);

            var isAuthorized = HasPermissionByPerson(person);

            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            }
        }

        public virtual bool HasPermissionByPerson(Person person)
        {
            return person != null && (_grantedRoles.Any(x => (int)x == person.Role.RoleID)
                    //|| !_grantedRoles.Any()); // allowing an empty list lets us implement LoggedInUnclassifiedFeature easily
                );
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
