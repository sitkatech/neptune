﻿/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Security;

namespace Neptune.Web.Views.Tenant
{
    public class DetailViewData : NeptuneViewData
    {
        public readonly Models.Tenant Tenant;
        public readonly TenantAttribute TenantAttribute;
        public readonly string EditBasicsUrl;
        public readonly string EditBoundingBoxUrl;
        public readonly bool UserHasTenantManagePermissions;
        public readonly SitkaRoute<UserController> PrimaryContactRoute;
        public readonly string DeleteTenantStyleSheetFileResourceUrl;
        public readonly string DeleteTenantSquareLogoFileResourceUrl;
        public readonly string DeleteTenantBannerLogoFileResourceUrl;
        public readonly bool IsCurrentTenant;
        public readonly string EditBoundingBoxFormID;
        public readonly MapInitJson MapInitJson;
        public readonly DetailGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;

        public DetailViewData(Person currentPerson, Models.Tenant tenant, TenantAttribute tenantAttribute, string editBasicsUrl, string editBoundingBoxUrl, string deleteTenantStyleSheetFileResourceUrl, string deleteTenantSquareLogoFileResourceUrl, string deleteTenantBannerLogoFileResourceUrl, string editBoundingBoxFormID, MapInitJson mapInitJson, DetailGridSpec gridSpec, string gridName, string gridDataUrl)
            : base(currentPerson)
        {
            EntityName = "Tenant Configuration";
            PageTitle = tenantAttribute.TenantDisplayName;
            Tenant = tenant;
            TenantAttribute = tenantAttribute;
            EditBasicsUrl = editBasicsUrl;
            EditBoundingBoxUrl = editBoundingBoxUrl;
            PrimaryContactRoute = tenantAttribute.PrimaryContactPerson != null ? new SitkaRoute<UserController>(c => c.Detail(tenantAttribute.PrimaryContactPersonID)) : null;
            UserHasTenantManagePermissions = new SitkaAdminFeature().HasPermissionByPerson(CurrentPerson);
            DeleteTenantStyleSheetFileResourceUrl = deleteTenantStyleSheetFileResourceUrl;
            DeleteTenantSquareLogoFileResourceUrl = deleteTenantSquareLogoFileResourceUrl;
            DeleteTenantBannerLogoFileResourceUrl = deleteTenantBannerLogoFileResourceUrl;
            IsCurrentTenant = HttpRequestStorage.Tenant == tenant;
            EditBoundingBoxFormID = editBoundingBoxFormID;
            MapInitJson = mapInitJson;
            GridSpec = gridSpec;
            GridName = gridName;
            GridDataUrl = gridDataUrl;
        }
    }
}
