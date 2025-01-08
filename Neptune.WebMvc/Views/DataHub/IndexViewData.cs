using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views.Shared;

namespace Neptune.WebMvc.Views.DataHub
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly WebServiceToken WebServiceAccessToken;
        public readonly List<WebServiceDocumentation> ServiceDocumentationList;
        public ViewPageContentViewData TreatmentBMPPage { get; }
        public ViewPageContentViewData DelineationPage { get; }
        public ViewPageContentViewData FieldTripPage { get; }
        public ViewPageContentViewData WQMPPage { get; }
        public ViewPageContentViewData SimplifiedBMPPage { get; }
        public ViewPageContentViewData WQMPLocationPage { get; }
        public ViewPageContentViewData AssessmentAreaPage { get; }
        public ViewPageContentViewData OVTAsPage { get; }
        public ViewPageContentViewData LandUseBlocksPage { get; }
        public string UploadTreatmentBMPUrl { get; set; }
        public string DownloadTreatmentBMPUrl { get; set; }
        public string UploadDelineationUrl { get; set; }
        public string DownloadDelineationUrl { get; set; }
        public string UploadFieldTripUrl { get; set; }
        public string UploadWQMPUrl { get; set; }
        public string UploadSimplifiedBMPUrl { get; set; }
        public string UploadWQMPLocationsUrl { get; set; }
        public string DownloadAssessmentAreasUrl { get; set; }
        public string UploadOVTAUrl { get; set; }
        public string UploadLandUseBlocksUrl { get; set; }
        public string DownloadLandUseBlocksUrl { get; set; }
        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, List<EFModels.Entities.NeptunePage> neptunePages, WebServiceToken webServiceAccessToken, List<WebServiceDocumentation> serviceDocumentationList)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = "Data Hub";
            PageTitle = "Index";
            WebServiceAccessToken = webServiceAccessToken;
            ServiceDocumentationList = serviceDocumentationList;
            TreatmentBMPPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.BMPDataHub), currentPerson);
            DelineationPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.DelineationDataHub), currentPerson);
            FieldTripPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.FieldVisitDataHub), currentPerson);
            WQMPPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.WQMPDataHub), currentPerson);
            SimplifiedBMPPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.SimplifiedBMPsDataHub), currentPerson);
            WQMPLocationPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.WQMPLocationsDataHub), currentPerson);
            AssessmentAreaPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.AssessmentAreasDataHub), currentPerson);
            OVTAsPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.OVTADataHub), currentPerson);
            LandUseBlocksPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.LandUseBlockDataHub), currentPerson);
            UploadTreatmentBMPUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.UploadBMPs());
            DownloadTreatmentBMPUrl = ""; //SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.UploadBMPs());
            UploadDelineationUrl = SitkaRoute<DelineationGeometryController>.BuildUrlFromExpression(linkGenerator, x => x.UpdateDelineationGeometry());
            DownloadDelineationUrl = SitkaRoute<DelineationGeometryController>.BuildUrlFromExpression(linkGenerator, x => x.DownloadDelineationGeometry());
            UploadFieldTripUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.BulkUploadTrashScreenVisit());
            UploadWQMPUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.UploadWqmps());
            UploadSimplifiedBMPUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.UploadSimplifiedBMPs());
            UploadWQMPLocationsUrl = ""; //SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.BulkUploadTrashScreenVisit());
            DownloadAssessmentAreasUrl = SitkaRoute<OnlandVisualTrashAssessmentExportController>.BuildUrlFromExpression(linkGenerator, x => x.ExportAssessmentGeospatialData());
            UploadOVTAUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.BulkUploadOTVAs());
            UploadLandUseBlocksUrl = SitkaRoute<LandUseBlockGeometryController>.BuildUrlFromExpression(linkGenerator, x => x.UpdateLandUseBlockGeometry());
            DownloadLandUseBlocksUrl = SitkaRoute<LandUseBlockGeometryController>.BuildUrlFromExpression(linkGenerator, x => x.DownloadLandUseBlockGeometry());
        }
    }
}