/*-----------------------------------------------------------------------
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

using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Common;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.Organization
{
    public class DetailViewData : NeptuneViewData
    {
        public readonly Models.Organization Organization;
        public readonly bool UserHasOrganizationManagePermissions;
        public readonly string EditOrganizationUrl;
        public readonly string EditBoundaryUrl;
        public readonly string DeleteOrganizationBoundaryUrl;

        public readonly string ManageFundingSourcesUrl;
        public readonly string IndexUrl;

        public readonly MapInitJson MapInitJson;
        public readonly bool HasSpatialData;
        
        public readonly string NewFundingSourceUrl;
        public readonly bool CanCreateNewFundingSource;

        public DetailViewData(Person currentPerson,
            Models.Organization organization,
            MapInitJson mapInitJson,
            bool hasSpatialData) : base(currentPerson)
        {
            Organization = organization;
            PageTitle = organization.DisplayName;
            EntityName = $"{Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}";
            UserHasOrganizationManagePermissions = new OrganizationManageFeature().HasPermissionByPerson(CurrentPerson);

            EditOrganizationUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(c => c.Edit(organization));
            
            IndexUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(c => c.Index());

            MapInitJson = mapInitJson;
            HasSpatialData = hasSpatialData;
        }

    }
}
