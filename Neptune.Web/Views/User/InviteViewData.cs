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

using LtInfo.Common.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.User
{
    public class InviteViewData : NeptuneViewData
    {
        public IEnumerable<SelectListItem> AllOrganizations { get; }
        public string CancelUrl { get; }

        public InviteViewData(Person currentPerson, List<EFModels.Entities.Organization> organizations, NeptunePage neptunePage, string cancelUrl, LinkGenerator linkGenerator, HttpContext httpContext) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, linkGenerator, httpContext)
        {
            CancelUrl = cancelUrl;
            PageTitle = "Invite User";
            EntityName = "Users";
            AllOrganizations = organizations.ToSelectList(x => x.OrganizationGuid.ToString(), x => x.OrganizationName);
        }
    }
}