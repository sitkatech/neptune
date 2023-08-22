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

using Neptune.Web.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.OpenID;
using Neptune.Web.Security;

namespace Neptune.Web.Controllers
{
    public class AccountController : NeptuneBaseController<AccountController>
    {
        public AccountController(NeptuneDbContext dbContext, ILogger<AccountController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        protected string LoginUrl
        {
            get
            {
                return SitkaRoute<AccountController>.BuildAbsoluteUrlHttpsFromExpression(_linkGenerator, "", c => c.Login());
            }
        }

        protected string HomeUrl
        {
            get { return SitkaRoute<HomeController>.BuildUrlFromExpression(_linkGenerator, c => c.Index()); }
        }


        [AnonymousUnclassifiedFeature]
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

        [AnonymousUnclassifiedFeature]
        public ActionResult Register()
        {
            return RedirectPermanent(HomeUrl);
        }

        [AnonymousUnclassifiedFeature]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AnonymousUnclassifiedFeature]
        public ActionResult SignoutCleanup(string sid)
        {
            return Content(string.Empty);
        }
    }
}
