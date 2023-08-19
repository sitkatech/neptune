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

using Neptune.EFModels.Entities;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Common;
using Neptune.Web.Security;

namespace Neptune.Web.Views.Organization
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.Organization Organization { get; }
        public bool UserHasOrganizationManagePermissions { get; }
        public bool UserHasCreateFundingSourcePermissions { get; }
        public string EditOrganizationUrl { get; }
        public string NewFundingSourceUrl { get; }
        public string ManageFundingSourcesUrl { get; }
        public string? OrganizationLogoUrl { get; }

        public DetailViewData(Person currentPerson, EFModels.Entities.Organization organization, LinkGenerator linkGenerator) : base(currentPerson, NeptuneArea.OCStormwaterTools, linkGenerator)
        {
            Organization = organization;
            EntityName = FieldDefinitionType.Organization.GetFieldDefinitionLabelPluralized();
            PageTitle = organization.GetDisplayName();
            UserHasOrganizationManagePermissions = new OrganizationManageFeature().HasPermissionByPerson(CurrentPerson);
            UserHasCreateFundingSourcePermissions = false;//todo: new FundingSourceCreateFeature().HasPermissionByPerson(CurrentPerson);
            if (UserHasOrganizationManagePermissions)
            {
                EntityUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, c => c.Index());
            }
            EditOrganizationUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, c => c.Edit(organization));
            NewFundingSourceUrl = "";//todo: SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.New());
            ManageFundingSourcesUrl = "";//todo: SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.Index());
            OrganizationLogoUrl = organization.LogoFileResource == null ? string.Empty : SitkaRoute<FileResourceController>.BuildUrlFromExpression(linkGenerator, x => x.DisplayResource(organization.LogoFileResource.FileResourceGUID.ToString()));
        }
    }
}
