using Neptune.Web.Common;
using Neptune.Web.Models;
using ParcelController = Neptune.Web.Controllers.ParcelController;

namespace Neptune.Web.Views.ParcelLayerUpload
{
    public class UploadParcelLayerViewData : NeptuneViewData
    {
        public string NewGisUploadUrl { get; }
        

        public UploadParcelLayerViewData(Person currentPerson, string newGisUploadUrl) : base(currentPerson, null, NeptuneArea.OCStormwaterTools)
        {
            NewGisUploadUrl = newGisUploadUrl;
            EntityName = "Parcel Layer";
            EntityUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(x => x.Index());
            PageTitle = "Upload New Parcel Layer";
        }
    }
}
