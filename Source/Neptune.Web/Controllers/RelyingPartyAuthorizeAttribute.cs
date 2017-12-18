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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Neptune.Web.Security;
using Neptune.Web.Security.Shared;
using Keystone.Common;
using Neptune.Web.Common;

namespace Neptune.Web.Controllers
{
    /// <summary>
    /// Prevent unauthorized access, unless it has been specifically allowed using AllowAnonymousAttribute
    /// </summary>
    public abstract class RelyingPartyAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var attributeType = typeof(AnonymousUnclassifiedFeature);
            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(attributeType, true)
                                    || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(attributeType, true);

            if (!skipAuthorization)
            {
                var neptuneBaseFeatureType = typeof(NeptuneBaseFeature);
                var neptuneBaseFeatureAttribute = filterContext.ActionDescriptor.GetCustomAttributes(neptuneBaseFeatureType, true).SingleOrDefault();
                if (neptuneBaseFeatureAttribute != null && ((NeptuneBaseFeature) neptuneBaseFeatureAttribute).GrantedRoles.Any())
                {

                    if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                    {
                        AuthenticateUser(filterContext);
                    }
                    else
                    {
                        base.OnAuthorization(filterContext);
                    }
                }
            }
        }

        // use FAM to redirect to STS to initiate SSO - parameters come via <microsoft.identityModel> section in config
        protected void AuthenticateUser(AuthorizationContext filterContext)
        {
            var requestContext = filterContext.RequestContext;
            if (requestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new ContentResult() {Content = "<!-- This is the SitkaIfInPartialPageRedirectToLoginPage (marker for Javascript ajax login redirect handling) -->"};
            }
            else
            {
                var writeQueryString = KeystoneUtilities.GetSignInRedirectUrlWithReturnUrl(requestContext, SitkaRoute<AccountController>.BuildUrlFromExpression(x => x.LogOn()), HttpContext.Current.Request.Url.ToString());
                filterContext.Result = new RedirectResult(writeQueryString);                
            }
        }
    }
}
