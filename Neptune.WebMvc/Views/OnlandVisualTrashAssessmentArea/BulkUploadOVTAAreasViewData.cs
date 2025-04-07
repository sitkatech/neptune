using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessmentArea
{
    public class BulkUploadOVTAAreasViewData: NeptuneViewData
    {
        public string NewGisUploadUrl { get; }
        public string ApprovedGisUploadUrl { get; }
        public string DownloadGisUrl { get; }

        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }

        public BulkUploadOVTAAreasViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, string newGisUploadUrl, string approvedGisUploadUrl, IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions, string downloadGisUrl) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            NewGisUploadUrl = newGisUploadUrl;
            ApprovedGisUploadUrl = approvedGisUploadUrl;
            DownloadGisUrl = downloadGisUrl;
            EntityName = "Data Hub";
            EntityUrl = SitkaRoute<DataHubController>.BuildUrlFromExpression(linkGenerator, x => x.Index());

            PageTitle = "OVTA Area Upload";

            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);

        }
    }
}