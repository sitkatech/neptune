using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.DelineationGeometry
{
    public class DownloadDelineationGeometryViewData : NeptuneViewData
    {
        public string GisDownloadUrl { get; }

        public string GisUploadUrl { get; }

        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }

        public DownloadDelineationGeometryViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, string newGisUploadUrl, IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions, string gisUploadUrl) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            GisDownloadUrl = newGisUploadUrl;
            GisUploadUrl = gisUploadUrl;
            EntityName = FieldDefinitionType.Delineation.FieldDefinitionTypeDisplayName;
            EntityUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.DelineationMap(null));

            PageTitle = "Download Delineation Geometry";

            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);

        }
    }
}
