﻿using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.LandUseBlockGeometry
{
    public class UpdateLandUseBlockGeometryViewData : NeptuneViewData
    {
        public string NewGisUploadUrl { get; }
        public string DownloadLandUseBlockUrl { get; }
        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }


        public UpdateLandUseBlockGeometryViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, string newGisUploadUrl, string downloadLandUseBlockUrl, IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            NewGisUploadUrl = newGisUploadUrl;
            DownloadLandUseBlockUrl = downloadLandUseBlockUrl;
            EntityName = "Data Hub";
            EntityUrl = SitkaRoute<DataHubController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = "Update Land Use Block Geometry";
            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);
        }
    }
}
