/*-----------------------------------------------------------------------
<copyright file="RefineAreaViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using NetTopologySuite.Features;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class RefineAreaViewData : NeptuneViewData
    {
        public RefineAreaViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.NeptunePage neptunePage,
            EFModels.Entities.WaterQualityManagementPlan waterQualityManagementPlan,
            MapInitJson mapInitJson, string geoServerUrl, FeatureCollection? boundaryLayerGeoJson) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            MapInitJson = mapInitJson;
            EntityName = "Water Quality Management Plan";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            SubEntityName = waterQualityManagementPlan.WaterQualityManagementPlanName;
            var detailUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(waterQualityManagementPlan));
            SubEntityUrl = detailUrl;
            PageTitle = "Refine Area";

            MapFormID = "editAreaMapForm";
            GeoServerUrl = geoServerUrl;
            BoundaryLayerGeoJson = boundaryLayerGeoJson;
            WaterQualityManagementPlanID = waterQualityManagementPlan.WaterQualityManagementPlanID;

            DetailUrl = detailUrl;
        }


        public string MapFormID { get; }
        public string GeoServerUrl { get; }
        public MapInitJson MapInitJson { get; }
        public FeatureCollection? BoundaryLayerGeoJson { get; }
        public int WaterQualityManagementPlanID { get; }
        public string DetailUrl { get; }
    }
}
