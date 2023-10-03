/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Security;

namespace Neptune.Web.Views.User
{
    public class IndexViewData : NeptuneViewData
    {
        public IndexGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public string KeystoneUrl { get; }
        public string KeystoneRegisterUserUrl { get; }

        public string InviteUserUrl { get; }
        public bool UserIsAdmin { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "All Users";
            EntityName = "Users";
            GridSpec = new IndexGridSpec(linkGenerator, currentPerson, new Dictionary<int, int>()) {ObjectNameSingular = "User", ObjectNamePlural = "Users", SaveFiltersInCookie = true};
            GridName = "UserGrid";
            GridDataUrl = SitkaRoute<UserController>.BuildUrlFromExpression(linkGenerator, x => x.IndexGridJsonData());
            KeystoneUrl = "";//todo: NeptuneWebConfiguration.KeystoneUrl;
            KeystoneRegisterUserUrl = "";//todo: $"{NeptuneWebConfiguration.KeystoneRegisterUrl}?ClientID={NeptuneWebConfiguration.KeystoneOpenIDClientID}&RedirectUrl={HttpUtility.UrlEncode(SitkaRoute<AccountController>.BuildUrlFromExpression(_linkGenerator, x => x.LogOn()))}";

            InviteUserUrl = "";//todo: SitkaRoute<UserController>.BuildUrlFromExpression(LinkGenerator, x => x.Invite());
            UserIsAdmin = new UserEditFeature().HasPermissionByPerson(currentPerson);
        }
    }
}
