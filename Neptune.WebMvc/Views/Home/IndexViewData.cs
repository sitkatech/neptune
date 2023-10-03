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
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Views.Shared;
using Neptune.WebMvc.Views.Shared.JurisdictionControls;

namespace Neptune.WebMvc.Views.Home
{
    public class IndexViewData : NeptuneViewData
    {
        public ViewPageContentViewData CustomHomePageTextViewData { get; }
        public ViewPageContentViewData CustomHomePageAdditionalInfoTextViewData { get; }
        public ViewPageContentViewData CustomHomePageMapTextViewData { get; }
        
        public JurisdictionsMapViewData JurisdictionsMapViewData { get; }
        public JurisdictionsMapInitJson JurisdictionsMapInitJson { get; }
        public List<EFModels.Entities.NeptuneHomePageImage> NeptuneHomePageCarouselImages { get; }
        public LaunchPadViewData LaunchPadViewData { get; }
        public UrlTemplate<string> DisplayFileResourceUrl { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.NeptunePage neptunePageHomePage,
            EFModels.Entities.NeptunePage neptunePageAdditionalInfo, EFModels.Entities.NeptunePage neptunePageMapInfo,
            List<EFModels.Entities.NeptuneHomePageImage> neptuneHomePageImages, JurisdictionsMapViewData jurisdictionsMapViewData,
            JurisdictionsMapInitJson jurisdictionsMapInitJson, LaunchPadViewData launchPadViewData) : base(httpContext, linkGenerator, currentPerson, null, true, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "Orange County Stormwater Tools";

            CustomHomePageTextViewData = new ViewPageContentViewData(linkGenerator, neptunePageHomePage, currentPerson, 600);
            CustomHomePageAdditionalInfoTextViewData = new ViewPageContentViewData(linkGenerator, neptunePageAdditionalInfo, currentPerson);
            CustomHomePageMapTextViewData = new ViewPageContentViewData(linkGenerator, neptunePageMapInfo, currentPerson);
            JurisdictionsMapViewData = jurisdictionsMapViewData;
            JurisdictionsMapInitJson = jurisdictionsMapInitJson;
            NeptuneHomePageCarouselImages = neptuneHomePageImages;
            LaunchPadViewData = launchPadViewData;
            DisplayFileResourceUrl = new UrlTemplate<string>(SitkaRoute<FileResourceController>.BuildUrlFromExpression(linkGenerator, t => t.DisplayResource(UrlTemplate.Parameter1String)));
        }
    }
}