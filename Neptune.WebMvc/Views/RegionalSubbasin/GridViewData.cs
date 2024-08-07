﻿using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.RegionalSubbasin
{
    public class GridViewData : NeptuneViewData
    {
        public RegionalSubbasinGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public bool HasAdminPermissions{ get; }
        public string RefreshUrl { get; }

        public GridViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)

        {
            EntityName = "Regional Subbasins";
            PageTitle = "Grid";
            GridSpec = new RegionalSubbasinGridSpec(LinkGenerator) { ObjectNameSingular = "Regional Subbasin", ObjectNamePlural = "Regional Subbasins", SaveFiltersInCookie = true };
            GridName = "absoluteUnitsGrid";
            GridDataUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator, x => x.RegionalSubbasinGridJsonData());

            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            RefreshUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator, x => x.RefreshFromOCSurvey());
        }
    }
}