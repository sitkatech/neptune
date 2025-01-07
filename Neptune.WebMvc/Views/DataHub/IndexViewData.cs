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
        public ViewPageContentViewData DelineationPage { get; }
        public ViewPageContentViewData FieldTripPage { get; }
        public string UploadTreatmentBMPUrl { get; set; }
        public string DownloadTreatmentBMPUrl { get; set; }
        public string UploadDelineationUrl { get; set; }
        public string DownloadDelineationUrl { get; set; }
        public string UploadFieldTripUrl { get; set; }
        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.NeptunePage treatmentBMPDataHubPage, EFModels.Entities.NeptunePage delineationDataHubPage, EFModels.Entities.NeptunePage fieldVisitDataHubPage)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = "Data Hub";
            TreatmentBMPPage = new ViewPageContentViewData(linkGenerator, treatmentBMPDataHubPage, currentPerson);
            DelineationPage = new ViewPageContentViewData(linkGenerator, delineationDataHubPage, currentPerson);
            FieldTripPage = new ViewPageContentViewData(linkGenerator, fieldVisitDataHubPage, currentPerson);
            UploadTreatmentBMPUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.UploadBMPs());
            DownloadTreatmentBMPUrl = ""; //SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.UploadBMPs());
            UploadDelineationUrl = SitkaRoute<DelineationGeometryController>.BuildUrlFromExpression(linkGenerator, x => x.UpdateDelineationGeometry());
            DownloadDelineationUrl = SitkaRoute<DelineationGeometryController>.BuildUrlFromExpression(linkGenerator, x => x.DownloadDelineationGeometry());
            UploadFieldTripUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.BulkUploadTrashScreenVisit());
        }
    }
}