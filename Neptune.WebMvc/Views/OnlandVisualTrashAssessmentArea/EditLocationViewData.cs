/*-----------------------------------------------------------------------
<copyright file="EditVitalSignLocationViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment.MapInitJson;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessmentArea
{
    public class EditLocationViewData : TrashModuleViewData
    {
        public EditLocationViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            WebConfiguration webConfiguration, EFModels.Entities.NeptunePage neptunePage,
            EFModels.Entities.OnlandVisualTrashAssessmentArea ovtaArea, RefineAssessmentAreaMapInitJson mapInitJson) : base(httpContext, linkGenerator, currentPerson, webConfiguration, neptunePage)
        {
            MapInitJson = mapInitJson;
            EntityName = "OVTA Areas";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            SubEntityName = ovtaArea.OnlandVisualTrashAssessmentAreaName;
            var detailUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator,
                x => x.Detail(ovtaArea));
            SubEntityUrl = detailUrl;
            PageTitle = "Edit Location";

            MapFormID = "editAssessmentAreaMapForm";
            GeoServerUrl = webConfiguration.MapServiceUrl;
            OnlandVisualTrashAssessmentAreaID = ovtaArea.OnlandVisualTrashAssessmentAreaID;

            ParcelUnionUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(linkGenerator , x => x.Union());
            ParcelsViaTransectUrlTemplate = new UrlTemplate<int>(SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator, x => x.ParcelsViaTransect(UrlTemplate.Parameter1Int))).UrlTemplateString;
            DetailUrl = detailUrl;
        }

        public string ParcelsViaTransectUrlTemplate { get; set; }

        public string MapFormID { get; }
        public string GeoServerUrl{ get; }
        public RefineAssessmentAreaMapInitJson MapInitJson { get; }
        public int OnlandVisualTrashAssessmentAreaID { get; }
        public string ParcelUnionUrl { get; }
        public string DetailUrl { get; }
    }
}
