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

using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.NeptunePage;

public class IndexViewData : NeptuneViewData
{
    public NeptunePageGridSpec GridSpec { get; }
    public string GridName { get; }
    public string GridDataUrl { get; }
    public string NeptunePageUrl { get; }

    public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson) : base(httpContext, linkGenerator, currentPerson, null, NeptuneArea.OCStormwaterTools, webConfiguration)
    {
        EntityName = "Page Content";
        PageTitle = "Manage Page Content";

        GridSpec = new NeptunePageGridSpec(new NeptunePageViewListFeature().HasPermissionByPerson(currentPerson), linkGenerator)
        {
            ObjectNameSingular = "Page",
            ObjectNamePlural = "Pages",
            SaveFiltersInCookie = true
        };
        GridName = "neptunePagesGrid";
        GridDataUrl = SitkaRoute<NeptunePageController>.BuildUrlFromExpression(linkGenerator, tc => tc.IndexGridJsonData());
        NeptunePageUrl = SitkaRoute<NeptunePageController>.BuildUrlFromExpression(linkGenerator, x => x.NeptunePageDetails(UrlTemplate.Parameter1Int));
    }
}