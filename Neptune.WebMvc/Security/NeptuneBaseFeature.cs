/*-----------------------------------------------------------------------
<copyright file="NeptuneBaseFeature.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Common.DesignByContract;
using Neptune.EFModels;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Security
{
    //[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class NeptuneBaseFeature// : BaseAuthorizationAttribute
    {
        private readonly NeptuneArea _neptuneArea;

        public IList<IRole> GrantedRoles { get; }

        protected NeptuneBaseFeature(IList<IRole> grantedRoles, NeptuneArea neptuneArea) // params
        {
            // Force user to pass us empty lists to make life simpler
            Check.RequireNotNull(grantedRoles, "Can\'t pass null for Granted Roles.");

            if (grantedRoles.Any())
            {
                // roles passed in need to be for the corresponding area
                Check.Require(grantedRoles.All(x => x.NeptuneAreaDisplayName == neptuneArea.NeptuneAreaDisplayName));
            }

            // At least one of these must be set
            //Check.Ensure(grantedRoles.Any(), "Must set at least one Role");

            GrantedRoles = grantedRoles;
            _neptuneArea = neptuneArea;
        }

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    Roles = CalculateRoleNameStringFromFeature();

        //    // MR #321 - force reload of user roles onto IClaimsIdentity
        //    KeystoneOpenIDUtilities.AddLocalUserAccountRolesToClaims(HttpRequestStorage.Person, HttpRequestStorage.GetHttpContextUserThroughOwin().Identity);

        //    // This ends up making the calls into the RoleProvider
        //    base.OnAuthorization(filterContext);
        //}

        //internal string CalculateRoleNameStringFromFeature()
        //{
        //    return String.Join(", ", GrantedRoles.Select(r => r.RoleName).ToList());
        //}

        //public string FeatureName => GetType().Name;

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    return HasPermissionByPerson(HttpRequestStorage.Person);
        //}

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    var redirectToLogin = new RedirectResult(NeptuneHelpers.GenerateLogInUrlWithReturnUrl());
        //    if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        filterContext.Result = redirectToLogin;
        //        return;
        //    }
        //    throw new SitkaRecordNotAuthorizedException($"You are not authorized for feature \"{FeatureName}\". Log out and log in as a different user or request additional permissions.");
        //}

        //public static NeptuneBaseFeature InstantiateFeature(Type featureType)
        //{
        //    return (NeptuneBaseFeature)Activator.CreateInstance(featureType);
        //}
    }
}
