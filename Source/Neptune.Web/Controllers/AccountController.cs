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

using System;
using System.Net;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Security.Shared;
using System.Web;
using Neptune.Web.Security;

namespace Neptune.Web.Controllers
{
    public class AccountController : SitkaController
    {
        protected override bool IsCurrentUserAnonymous()
        {
            return HttpRequestStorage.Person.IsAnonymousUser();
        }

        protected override string LoginUrl
        {
            get { return SitkaRoute<AccountController>.BuildAbsoluteUrlHttpsFromExpression(c => c.LogOn(), NeptuneWebConfiguration.CanonicalHostNameRoot); }
        }

        protected override ISitkaDbContext SitkaDbContext => HttpRequestStorage.DatabaseEntities;

        protected string HomeUrl
        {
            get { return SitkaRoute<HomeController>.BuildUrlFromExpression(c => c.Index()); }
        }


        [AnonymousUnclassifiedFeature]
        public ContentResult NotAuthorized()
        {
            return Content("Not Authorized");
        }

        [LoggedInUnclassifiedFeature]
        public ActionResult LogOn()
        {
            var returnUrl = Request.Cookies["NeptuneReturnURL"];
            if (!string.IsNullOrWhiteSpace(returnUrl?.Value))
            {
                Response.Cookies.Add(returnUrl);
                returnUrl.Expires = DateTime.Now.AddDays(-1d);

                return Redirect(HttpUtility.UrlDecode(returnUrl.Value));
            }
            return Redirect(HomeUrl);
        }

        [AnonymousUnclassifiedFeature]
        public ActionResult Register()
        {
            return RedirectPermanent(HomeUrl);
        }

        [AnonymousUnclassifiedFeature]
        public ActionResult LogOff()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

        [AnonymousUnclassifiedFeature]
        public ActionResult SignoutCleanup(string sid)
        {
            return Content(string.Empty);
        }
    }
}
