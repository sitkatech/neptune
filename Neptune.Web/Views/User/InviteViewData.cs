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

using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;

namespace Neptune.Web.Views.User
{
    public class InviteViewData : NeptuneViewData
    {
        public IEnumerable<SelectListItem> AllOrganizations { get; }
        public string CancelUrl { get; }

        public InviteViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            List<EFModels.Entities.Organization> organizations, EFModels.Entities.NeptunePage neptunePage, string cancelUrl) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            CancelUrl = cancelUrl;
            PageTitle = "Invite User";
            EntityName = "Users";
            AllOrganizations = organizations.ToSelectList(x => x.OrganizationGuid.ToString(), x => x.OrganizationName);
        }
    }
}
