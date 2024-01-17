using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.ParcelLayerUpload
{
    public class UploadParcelLayerViewData : NeptuneViewData
    {
        public UploadParcelLayerViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson) : base(httpContext, linkGenerator, currentPerson, null, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = "Parcel Layer";
            EntityUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(LinkGenerator, x => x.Index());
            PageTitle = "Upload New Parcel Layer";
        }
    }
}
