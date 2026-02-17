/*-----------------------------------------------------------------------
<copyright file="AccountController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Auth0.AspNetCore.Authentication;
using Neptune.WebMvc.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.OpenID;
using Neptune.WebMvc.Views.Account;

namespace Neptune.WebMvc.Controllers
{
    public class AccountController(
        NeptuneDbContext dbContext,
        ILogger<AccountController> logger,
        IOptions<WebConfiguration> webConfiguration,
        LinkGenerator linkGenerator)
        : NeptuneBaseController<AccountController>(dbContext, logger, linkGenerator, webConfiguration)
    {
        protected string HomeUrl
        {
            get { return SitkaRoute<HomeController>.BuildUrlFromExpression(_linkGenerator, x => x.Index()); }
        }


        public ContentResult NotAuthorized()
        {
            return Content("Not Authorized");
        }

        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            returnUrl ??= "/";

            // Security: block open redirects and avoid redirect loops
            if (!Url.IsLocalUrl(returnUrl) ||
                returnUrl.StartsWith("//", StringComparison.Ordinal) ||
                returnUrl.StartsWith("/callback", StringComparison.OrdinalIgnoreCase) ||
                returnUrl.StartsWith("/Account/Login", StringComparison.OrdinalIgnoreCase) ||
                returnUrl.StartsWith("/Account/Logout", StringComparison.OrdinalIgnoreCase))
            {
                returnUrl = "/";
            }

            var props = new LoginAuthenticationPropertiesBuilder()
                // This is the final landing page AFTER Auth0 returns to /callback and the middleware completes auth
                .WithRedirectUri(returnUrl)
                .Build();

            return Challenge(props, Auth0Constants.AuthenticationScheme);
        }

        public ActionResult Register()
        {
            return RedirectPermanent(HomeUrl);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // 1) Clear the local app session
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 2) Send the user to Auth0 to clear the Auth0 session (SSO cookie)
            var returnTo = Url.Action("Index", "Home", values: null, protocol: Request.Scheme);

            var properties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnTo) // MUST be absolute and whitelisted in Auth0 Allowed Logout URLs
                .Build();

            return SignOut(properties, Auth0Constants.AuthenticationScheme);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult VerifyEmailRequired()
        {
            var viewData = new VerifyEmailRequiredViewData(HttpContext, _linkGenerator, _webConfiguration,
                CurrentPerson);
            return RazorView<Views.Account.VerifyEmailRequired, VerifyEmailRequiredViewData>(viewData);
        }
    }
}