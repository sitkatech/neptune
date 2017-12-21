using Neptune.Web.Models;

namespace Neptune.Web.Views.ModeledCatchment
{
    public class UpdateModeledCatchmentGeometryViewData : NeptuneViewData
    {
        public readonly string NewGisUploadUrl;
        public readonly string ApprovedGisUploadUrl;

        public UpdateModeledCatchmentGeometryViewData(Person currentPerson, string newGisUploadUrl, string approvedGisUploadUrl) : base(currentPerson, StormwaterBreadCrumbEntity.ModeledCatchment)
        {
            NewGisUploadUrl = newGisUploadUrl;
            ApprovedGisUploadUrl = approvedGisUploadUrl;
            PageTitle = "Update Catchment Geometry";
        }
    }
}
