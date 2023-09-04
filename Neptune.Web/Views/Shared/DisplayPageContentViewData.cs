/*-----------------------------------------------------------------------
<copyright file="DisplayPageContentViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace Neptune.Web.Views.Shared
{
    public class DisplayPageContentViewData : NeptuneViewData
    {
        public readonly ViewPageContentViewData ViewWholePageContentViewData;

        public DisplayPageContentViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Stormwater Tools";
            PageTitle = neptunePage.NeptunePageType.NeptunePageTypeDisplayName;
            ViewWholePageContentViewData = new ViewPageContentViewData(neptunePage, currentPerson);
        }
    }
}
