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
using Neptune.Web.Security;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Common;

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

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage)
            : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            EntityName = FieldDefinitionType.Organization.GetFieldDefinitionLabelPluralized();
            PageTitle = "All Organizations";

            var hasOrganizationManagePermissions = new OrganizationManageFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new IndexGridSpec(currentPerson, hasOrganizationManagePermissions)
            {
                ObjectNameSingular = $"{FieldDefinitionType.Organization.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{FieldDefinitionType.Organization.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };

            GridName = "organizationsGrid";
            GridDataUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());

            PullOrganizationFromKeystoneUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(x => x.PullOrganizationFromKeystone());
            UserIsSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(currentPerson);
            UserCanAddOrganization = new OrganizationManageFeature().HasPermissionByPerson(currentPerson);

            NewOrganizationUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(t => t.New());
        }
    }
}
