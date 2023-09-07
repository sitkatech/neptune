/*-----------------------------------------------------------------------
<copyright file="EditWqmpBoundaryViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.WaterQualityManagementPlan.BoundaryMapInitJson;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class EditWqmpBoundaryViewData : NeptuneViewData
    {
        public EditWqmpBoundaryViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.NeptunePage neptunePage, EFModels.Entities.WaterQualityManagementPlan wqmp, BoundaryAreaMapInitJson mapInitJson, string geoServerUrl) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            WaterQualityManagementPlan = wqmp;
            MapInitJson = mapInitJson;
            EntityName = "Water Quality Management Plan";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            SubEntityName = wqmp.WaterQualityManagementPlanName;
            var wqmpUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(wqmp));
            SubEntityUrl = wqmpUrl;
            PageTitle = "Refine Area";

            MapFormID = "editAreaMapForm";
            GeoServerUrl = geoServerUrl;
            WaterQualityManagementPlanID = wqmp.WaterQualityManagementPlanID;

            DetailUrl = wqmpUrl;
        }


        public EFModels.Entities.WaterQualityManagementPlan WaterQualityManagementPlan { get; }
        public string MapFormID { get; }
        public string GeoServerUrl { get; }
        public BoundaryAreaMapInitJson MapInitJson { get; }
        public int WaterQualityManagementPlanID { get; }
        public string DetailUrl { get; }

    }
}
