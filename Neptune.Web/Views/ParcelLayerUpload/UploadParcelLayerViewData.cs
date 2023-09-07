using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.ParcelLayerUpload
{
    public class UploadParcelLayerViewData : NeptuneViewData
    {
        public string NewGisUploadUrl { get; }
        

        public UploadParcelLayerViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, string newGisUploadUrl) : base(httpContext, linkGenerator, currentPerson, null, NeptuneArea.OCStormwaterTools)
        {
            NewGisUploadUrl = newGisUploadUrl;
            EntityName = "Parcel Layer";
            EntityUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            PageTitle = "Upload New Parcel Layer";
        }
    }
}
