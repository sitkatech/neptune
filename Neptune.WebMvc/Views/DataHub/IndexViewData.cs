using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Assessment;
using Neptune.WebMvc.Views.FieldVisit;
using Neptune.WebMvc.Views.MaintenanceRecord;
using Neptune.WebMvc.Views.Shared;

namespace Neptune.WebMvc.Views.DataHub
{
    public class IndexViewData : NeptuneViewData
    {
        public ViewPageContentViewData TreatmentBMPPage { get; }
        public string UploadTreatmentBMPUrl { get; set; }
        public string DownloadTreatmentBMPUrl { get; set; }
        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.NeptunePage treatmentBMPDataHubPage)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = "Data Hub";
            TreatmentBMPPage = new ViewPageContentViewData(linkGenerator, treatmentBMPDataHubPage, currentPerson);
            UploadTreatmentBMPUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.UploadBMPs());
            DownloadTreatmentBMPUrl = ""; //SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.UploadBMPs());
        }
    }
}