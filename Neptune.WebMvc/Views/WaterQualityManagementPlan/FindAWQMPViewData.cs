/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Shared;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class FindAWQMPViewData : NeptuneViewData
    {
        public const int MaxNumberOfBmpsInList = 40;

        public MapInitJson MapInitJson { get; }
        public string FindWQMPByNameUrl { get; }
        public string NewUrl { get; }
        public bool HasManagePermissions { get; }
        public ViewDataForAngular ViewDataForAngular { get; set; }
        public string AllWQMPsUrl { get; }
        public bool HasEditPermissions { get; set; }
        public string BulkUploadWQMPUrl { get; }
        public string BulkUploadSimplifiedBMPs { get; }
        public string BulkWqmpBoundaryFromAPNs { get; }
        public string NewWaterQualityManagementPlanUrl { get; }
        public bool CurrentPersonCanCreate { get; }
        public string GeoServerUrl { get; }


        public FindAWQMPViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            SearchMapInitJson mapInitJson, EFModels.Entities.NeptunePage neptunePage,
            List<StormwaterJurisdictionDisplayDto> jurisdictions, List<EFModels.Entities.WaterQualityManagementPlan> wqmpBoundaries,
            string geoServerUrl)
            : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PageTitle = "WQMP Map";
            EntityName = $"{FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabelPluralized()}";
            EntityUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            MapInitJson = mapInitJson;
            FindWQMPByNameUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.FindByName(null));
            NewUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.New());
            AllWQMPsUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            ViewDataForAngular = new ViewDataForAngular(mapInitJson, FindWQMPByNameUrl, jurisdictions, wqmpBoundaries.Select(x => x.AsGeometryDto()).ToList(), geoServerUrl);
            HasEditPermissions = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            BulkUploadWQMPUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x =>
                x.UploadWqmps());
            BulkUploadSimplifiedBMPs =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.UploadSimplifiedBMPs());
            BulkWqmpBoundaryFromAPNs =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator,
                    x => x.UploadWqmpBoundaryFromAPNs());
            NewWaterQualityManagementPlanUrl =
                SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(LinkGenerator, x => x.New());
            CurrentPersonCanCreate = new WaterQualityManagementPlanCreateFeature().HasPermissionByPerson(currentPerson);
            GeoServerUrl = geoServerUrl;
        }
    }

    public class ViewDataForAngular
    {
        public SearchMapInitJson MapInitJson { get; }
        public string FindWQMPByNameUrl { get; }
        public List<StormwaterJurisdictionDisplayDto> Jurisdictions { get; set; }
        public List<WaterQualityManagementPlanWithGeometryDto> WaterQualityManagementPlans { get; set; }
        public string GeoServerUrl { get; }


        public ViewDataForAngular(SearchMapInitJson mapInitJson, string findWQMPByNameUrl, 
            List<StormwaterJurisdictionDisplayDto> jurisdictions, List<WaterQualityManagementPlanWithGeometryDto> wqmpBoundaries,
            string geoServerUrl)
        {
            MapInitJson = mapInitJson;
            FindWQMPByNameUrl = findWQMPByNameUrl;
            WaterQualityManagementPlans = wqmpBoundaries;
            Jurisdictions = jurisdictions;
            GeoServerUrl = geoServerUrl;

        }

    }
}
