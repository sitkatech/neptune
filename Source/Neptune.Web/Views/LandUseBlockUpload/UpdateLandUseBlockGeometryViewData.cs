using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.LandUseBlockUpload
{
    public class UpdateLandUseBlockGeometryViewData : NeptuneViewData
    {
        public string NewGisUploadUrl { get; }
        

        public UpdateLandUseBlockGeometryViewData(Person currentPerson, string newGisUploadUrl) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            NewGisUploadUrl = newGisUploadUrl;
            EntityName = "Land Use Block";
            EntityUrl = SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(x => x.Index());
            PageTitle = "Update Land Use Block Geometry";
        }
    }
}
