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

using LtInfo.Common;
using Neptune.Web.Models;
using Neptune.Web.Common;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using ParcelController = Neptune.Web.Areas.Trash.Controllers.ParcelController;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentArea
{
    public class EditLocationViewData : TrashModuleViewData
    {
        public EditLocationViewData(Person currentPerson, Models.OnlandVisualTrashAssessmentArea ovtaArea, RefineAssessmentAreaMapInitJson mapInitJson) : base(currentPerson, NeptunePage.GetNeptunePageByPageType(NeptunePageType.EditOVTAArea))
        {
            MapInitJson = mapInitJson;
            EntityName = "OVTA Areas";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index());
            SubEntityName = ovtaArea.OnlandVisualTrashAssessmentAreaName;
            SubEntityUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(x => x.Detail(ovtaArea));
            PageTitle = "Edit Location";

            MapFormID = "editAssessmentAreaMapForm";
            GeoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
            OnlandVisualTrashAssessmentAreaID = ovtaArea.OnlandVisualTrashAssessmentAreaID;

            ParcelUnionUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(x => x.Union());
            ParcelsViaTransectUrlTemplate = new UrlTemplate<int>(SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(x => x.ParcelsViaTransect(UrlTemplate.Parameter1Int))).UrlTemplateString;
            DetailUrl = ovtaArea.GetDetailUrl();
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
