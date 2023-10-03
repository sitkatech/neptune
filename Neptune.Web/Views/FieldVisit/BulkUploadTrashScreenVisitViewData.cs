using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.FieldVisit
{
    public class BulkUploadTrashScreenVisitViewData: NeptuneViewData
    {
        public string TemplateDownloadUrl { get; set; }
     
        public BulkUploadTrashScreenVisitViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = FieldDefinitionType.FieldVisit.GetFieldDefinitionLabel();
            EntityUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = "Bulk Upload Trash Screen Field Visits";
            TemplateDownloadUrl = "";// todo: SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.TrashScreenBulkUploadTemplate());
        }
    }
}