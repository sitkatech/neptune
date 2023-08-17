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

using System.Text.Json;
using Microsoft.AspNetCore.Html;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.JurisdictionControls;

namespace Neptune.Web.Views.Home
{
    public class IndexViewData : NeptuneViewData
    {
        public ViewPageContentViewData CustomHomePageTextViewData { get; }
        public ViewPageContentViewData CustomHomePageAdditionalInfoTextViewData { get; }
        public ViewPageContentViewData CustomHomePageMapTextViewData { get; }
        
        public JurisdictionsMapViewData JurisdictionsMapViewData { get; }
        public JurisdictionsMapInitJson JurisdictionsMapInitJson { get; }
        public List<NeptuneHomePageImage> NeptuneHomePageCarouselImages { get; }
        public LaunchPadViewData LaunchPadViewData { get; }
        public UrlTemplate<string> DisplayFileResourceUrl { get; }

        public IndexViewData(Person currentPerson, NeptunePage neptunePageHomePage,
            NeptunePage neptunePageAdditionalInfo, NeptunePage neptunePageMapInfo,
            List<NeptuneHomePageImage> neptuneHomePageImages, JurisdictionsMapViewData jurisdictionsMapViewData,
            JurisdictionsMapInitJson jurisdictionsMapInitJson, LaunchPadViewData launchPadViewData, LinkGenerator linkGenerator) : base(currentPerson, neptunePageHomePage, true, NeptuneArea.OCStormwaterTools, linkGenerator)
        {
            PageTitle = "Orange County Stormwater Tools";

            CustomHomePageTextViewData = new ViewPageContentViewData(neptunePageHomePage, currentPerson);
            CustomHomePageAdditionalInfoTextViewData = new ViewPageContentViewData(neptunePageAdditionalInfo, currentPerson);
            CustomHomePageMapTextViewData = new ViewPageContentViewData(neptunePageMapInfo, currentPerson);
            JurisdictionsMapViewData = jurisdictionsMapViewData;
            JurisdictionsMapInitJson = jurisdictionsMapInitJson;
            NeptuneHomePageCarouselImages = neptuneHomePageImages;
            LaunchPadViewData = launchPadViewData;
            DisplayFileResourceUrl = new UrlTemplate<string>(new SitkaRoute<FileResourceController>(t => t.DisplayResource(UrlTemplate.Parameter1String),
                linkGenerator).BuildUrlFromExpression());
        }
    }
}
