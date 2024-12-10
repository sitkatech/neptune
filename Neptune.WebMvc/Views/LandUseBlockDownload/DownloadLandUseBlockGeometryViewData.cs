using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.LandUseBlockDownload
{
    public class DownloadLandUseBlockGeometryViewData : TrashModuleViewData
    {
        public string NewGisUploadUrl { get; }
        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }


        public DownloadLandUseBlockGeometryViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, string newGisUploadUrl, IEnumerable<StormwaterJurisdiction> stormwaterJurisdictions) : base(httpContext, linkGenerator, currentPerson, webConfiguration)
        {
            NewGisUploadUrl = newGisUploadUrl;
            EntityName = "Land Use Block";
            EntityUrl = SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = "Download Land Use Block Geometry";
            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);

        }
    }
}
