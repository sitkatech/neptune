using Neptune.Web.Models;

namespace Neptune.Web.Views.LandUseBlockUpload
{
    public class UpdateLandUseBlockGeometryViewData : NeptuneViewData
    {
        public readonly string NewGisUploadUrl;
        public readonly string ApprovedGisUploadUrl;

        public UpdateLandUseBlockGeometryViewData(Person currentPerson, string newGisUploadUrl, string approvedGisUploadUrl) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            NewGisUploadUrl = newGisUploadUrl;
            ApprovedGisUploadUrl = approvedGisUploadUrl;
            EntityName = Models.FieldDefinition.Delineation.FieldDefinitionDisplayName;
            PageTitle = "Update Delineation Geometry";
        }
    }
}
