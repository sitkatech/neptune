using Neptune.Web.Models;

namespace Neptune.Web.Views.DelineationUpload
{
    public class UpdateDelineationGeometryViewData : NeptuneViewData
    {
        public readonly string NewGisUploadUrl;
        public readonly string ApprovedGisUploadUrl;

        public UpdateDelineationGeometryViewData(Person currentPerson, string newGisUploadUrl, string approvedGisUploadUrl) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            NewGisUploadUrl = newGisUploadUrl;
            ApprovedGisUploadUrl = approvedGisUploadUrl;
            EntityName = Models.FieldDefinition.Delineation.FieldDefinitionDisplayName;
            PageTitle = "Update Delineation Geometry";
        }
    }
}
