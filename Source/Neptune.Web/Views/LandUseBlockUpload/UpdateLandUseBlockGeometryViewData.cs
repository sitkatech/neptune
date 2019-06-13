using Neptune.Web.Models;

namespace Neptune.Web.Views.LandUseBlockUpload
{
    public class UpdateLandUseBlockGeometryViewData : NeptuneViewData
    {
        public string NewGisUploadUrl { get; }
        public string ApprovedGisUploadUrl { get; }

        public UpdateLandUseBlockGeometryViewData(Person currentPerson, string newGisUploadUrl, string approvedGisUploadUrl) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            NewGisUploadUrl = newGisUploadUrl;
            ApprovedGisUploadUrl = approvedGisUploadUrl;
            EntityName = "Land Use Block";
            PageTitle = "Update Land Use Block Geometry";
        }
    }
}
