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
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.Organization
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly IndexGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;

        public readonly string PullOrganizationFromKeystoneUrl;
        public readonly string NewOrganizationUrl;
        public readonly bool UserIsSitkaAdmin;
        public readonly bool UserCanAddOrganization;

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            NeptunePage neptunePage)
            : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools) {
            EntityName = FieldDefinitionType.Organization.GetFieldDefinitionLabelPluralized();
            PageTitle = "All Organizations";

            var hasOrganizationManagePermissions = new OrganizationManageFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new IndexGridSpec(currentPerson, hasOrganizationManagePermissions, linkGenerator)
            {
                ObjectNameSingular = $"{FieldDefinitionType.Organization.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{FieldDefinitionType.Organization.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };

            GridName = "organizationsGrid";
            GridDataUrl = LinkGenerator.GetPathByAction("IndexGridJsonData", "Organization");

            PullOrganizationFromKeystoneUrl = ""; //todo: SitkaRoute<OrganizationController>.BuildUrlFromExpression(_linkGenerator, x => x.PullOrganizationFromKeystone());
            UserIsSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(currentPerson);
            UserCanAddOrganization = new OrganizationManageFeature().HasPermissionByPerson(currentPerson);

            NewOrganizationUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, t => t.New());
        }
    }
}
