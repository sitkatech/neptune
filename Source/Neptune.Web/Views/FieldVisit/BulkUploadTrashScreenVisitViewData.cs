using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class BulkUploadTrashScreenVisitViewData: NeptuneViewData
    {
        public string TemplateDownloadUrl { get; set; }
     
        public BulkUploadTrashScreenVisitViewData(Person currentPerson) : base(currentPerson, Models.NeptunePage.GetNeptunePageByPageType(NeptunePageType.BulkUploadFieldVisits), NeptuneArea.OCStormwaterTools)
        {
            EntityName = Models.FieldDefinition.FieldVisit.GetFieldDefinitionLabel();
            EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Index());
            PageTitle = "Bulk Upload Trash Screen Field Visits";
            TemplateDownloadUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.TrashScreenBulkUploadTemplate());
        }
    }
}