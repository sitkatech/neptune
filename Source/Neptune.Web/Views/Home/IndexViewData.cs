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

using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.Home
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly ViewPageContentViewData CustomHomePageTextViewData;
        public readonly ViewPageContentViewData CustomHomePageAdditionalInfoTextViewData;
        public readonly ViewPageContentViewData CustomHomePageMapTextViewData;
        public readonly string FullMapUrl;
        public readonly List<Models.NeptuneHomePageImage> NeptuneHomePageCarouselImages;

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePageHomePage, Models.NeptunePage neptunePageAdditionalInfo,
            Models.NeptunePage neptunePageMapInfo, List<Models.NeptuneHomePageImage> neptuneHomePageImages) : base(currentPerson, neptunePageHomePage)
        {
            PageTitle = MultiTenantHelpers.GetToolDisplayName();

            CustomHomePageTextViewData = new ViewPageContentViewData(neptunePageHomePage, currentPerson);
            CustomHomePageAdditionalInfoTextViewData = new ViewPageContentViewData(neptunePageAdditionalInfo, currentPerson);
            CustomHomePageMapTextViewData = new ViewPageContentViewData(neptunePageMapInfo, currentPerson);
            NeptuneHomePageCarouselImages = neptuneHomePageImages;
        }
    }
}
