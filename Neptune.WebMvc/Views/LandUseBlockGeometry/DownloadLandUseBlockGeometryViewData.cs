using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.LandUseBlockGeometry
{
    public class DownloadLandUseBlockGeometryViewData : TrashModuleViewData
    {
        public string NewGisUploadUrl { get; }
        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }
        public string UploadLandUseBlockUrl { get; }


        public DownloadLandUseBlockGeometryViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, string newGisUploadUrl, IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions, string uploadLandUseBlockUrl) : base(httpContext, linkGenerator, currentPerson, webConfiguration)
        {
            UploadLandUseBlockUrl = uploadLandUseBlockUrl;
            EntityName = "Land Use Block";
            EntityUrl = SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = "Download Land Use Block Geometry";
            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);

        }
    }
}
