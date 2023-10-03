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

using Neptune.WebMvc.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.OpenID;

namespace Neptune.WebMvc.Controllers
{
    public class AccountController : NeptuneBaseController<AccountController>
    {
        public AccountController(NeptuneDbContext dbContext, ILogger<AccountController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        protected string LoginUrl
        {
            get
            {
                return SitkaRoute<AccountController>.BuildAbsoluteUrlHttpsFromExpression(_linkGenerator, "", x => x.Login());
            }
        }

        protected string HomeUrl
        {
            get { return SitkaRoute<HomeController>.BuildUrlFromExpression(_linkGenerator, x => x.Index()); }
        }


        public ContentResult NotAuthorized()
        {
            return Content("Not Authorized");
        }

        [Authorize]
        public ActionResult Login()
        {
            var rawReturnUrl = HttpContext.Request.Cookies["NeptuneReturnURL"];
            var returnUrl = AuthenticationHelper.SanitizeReturnUrlForLogin(rawReturnUrl, HomeUrl);
            return Redirect(returnUrl);
        }

        public ActionResult Register()
        {
            return RedirectPermanent(HomeUrl);
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SignoutCleanup(string sid)
        {
            return Content(string.Empty);
        }
    }
}
