﻿/*-----------------------------------------------------------------------
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
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using LtInfo.Common;
using Neptune.Web.Security;

namespace Neptune.Web.Views.User
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly IndexGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;
        public readonly string KeystoneUrl;
        public readonly string KeystoneRegisterUserUrl;
        
        public readonly string PullUserFromKeystoneUrl;
        public readonly bool UserIsSitkaAdmin;

        public IndexViewData(Person currentPerson) : base(currentPerson)
        {
            PageTitle = "Users";
            var hasDeletePermission = new UserEditFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new IndexGridSpec(currentPerson) {ObjectNameSingular = "User", ObjectNamePlural = "Users", SaveFiltersInCookie = true};
            GridName = "UserGrid";
            GridDataUrl = SitkaRoute<UserController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());
            KeystoneUrl = NeptuneWebConfiguration.KeystoneUrl;
            KeystoneRegisterUserUrl = NeptuneWebConfiguration.KeystoneRegisterUserUrl;

            PullUserFromKeystoneUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.PullUserFromKeystone());
            UserIsSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(currentPerson);
        }
    }
}
