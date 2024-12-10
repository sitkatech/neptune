using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.LandUseBlockDownload
{
    public class DownloadLandUseBlockGeometryViewData : TrashModuleViewData
    {
        public string NewGisUploadUrl { get; }
        

        public DownloadLandUseBlockGeometryViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, string newGisUploadUrl) : base(httpContext, linkGenerator, currentPerson, webConfiguration)
        {
            NewGisUploadUrl = newGisUploadUrl;
            EntityName = "Land Use Block";
            EntityUrl = SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = "Download Land Use Block Geometry";
        }
    }
}
