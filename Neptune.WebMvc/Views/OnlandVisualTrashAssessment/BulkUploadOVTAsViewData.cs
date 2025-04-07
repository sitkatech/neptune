﻿using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment
{
    public class BulkUploadOVTAsViewData: NeptuneViewData
    {
        public string TemplateDownloadUrl { get; set; }
        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }


        public BulkUploadOVTAsViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.NeptunePage neptunePage, IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = "Data Hub";
            EntityUrl = SitkaRoute<DataHubController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = "Bulk Upload OTVAs";
            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);
            TemplateDownloadUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.OVTABulkUploadTemplate());
        }
    }
}