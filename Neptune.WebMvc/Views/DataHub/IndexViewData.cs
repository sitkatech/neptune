using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Shared;

namespace Neptune.WebMvc.Views.DataHub
{
    public class IndexViewData : NeptuneViewData
    {
        public bool HasManagePermission { get; }
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
        public ViewPageContentViewData ParcelPage { get; }
        public ViewPageContentViewData RegionalSubbasinsPage { get; }
        public ViewPageContentViewData LandUseStatisticsPage { get; }
        public ViewPageContentViewData ModelBasinsPage { get; }
        public ViewPageContentViewData PrecipitationZonesPage { get; }
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
        public string ParcelUploadUrl { get; set; }
        public string RegionalSubbasinRefreshUrl { get; }
        public string LandUseStatisticsRefreshUrl { get; }
        public string ModelBasinsRefreshUrl { get; }
        public string PrecipitationZonesRefreshUrl { get; }
        public DateTime? LastUpdatedRegionalSubbasins { get; set; }
        public DateTime? LastUpdatedHRUCharacteristics { get; set; }
        public DateTime? LastUpdatedModalBasins { get; set; }
        public DateTime? LastUpdatedPrecipitationZones { get; set; }
        public string BMPListUrl { get; set; }
        public string FieldTripUrl { get; set; }
        public string ModelingAttributeUrl { get; set; }
        public string BMPMapUrl { get; set; }
        public string BMPTypesUrl { get; set; }
        public string WQMPListUrl { get; set; }
        public string OVTAListUrl { get; set; }
        public string TrashGeneratingUnitsAuditListUrl { get; set; }
        public string LandUseBlocksUrl { get; set; }
        public string ParcelMapUrl { get; set; }
        public string RSBListUrl { get; set; }
        public string RSBGridUrl { get; set; }
        public string HRUCharacteristicsListUrl { get; set; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, List<EFModels.Entities.NeptunePage> neptunePages, WebServiceToken webServiceAccessToken, List<WebServiceDocumentation> serviceDocumentationList, DateTime? lastUpdatedRegionalSubbasin, DateTime? lastUpdatedDateModelBasins, DateTime? lastUpdatedDatePrecipitationZones, DateTime? lastUpdatedDateHRUCharacteristics)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            HasManagePermission = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
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
            ParcelPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.ParcelUploadDataHub), currentPerson);
            RegionalSubbasinsPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.RegionalSubbasinsDataHub), currentPerson);
            LandUseStatisticsPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.LandUseStatisticsDataHub), currentPerson);
            ModelBasinsPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.ModelBasinsDataHub), currentPerson);
            PrecipitationZonesPage = new ViewPageContentViewData(linkGenerator, neptunePages.Single(x => x.NeptunePageTypeID == (int)NeptunePageTypeEnum.PrecipitationZonesDataHub), currentPerson);
            UploadTreatmentBMPUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.UploadBMPs());
            DownloadTreatmentBMPUrl = ""; //SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.UploadBMPs());
            UploadDelineationUrl = SitkaRoute<DelineationGeometryController>.BuildUrlFromExpression(linkGenerator, x => x.UpdateDelineationGeometry());
            DownloadDelineationUrl = SitkaRoute<DelineationGeometryController>.BuildUrlFromExpression(linkGenerator, x => x.DownloadDelineationGeometry());
            UploadFieldTripUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.BulkUploadTrashScreenVisit());
            UploadWQMPUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.UploadWqmps());
            UploadSimplifiedBMPUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.UploadSimplifiedBMPs());
            UploadWQMPLocationsUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.UploadWqmpBoundaryFromAPNs());
            DownloadAssessmentAreasUrl = SitkaRoute<OnlandVisualTrashAssessmentExportController>.BuildUrlFromExpression(linkGenerator, x => x.ExportAssessmentGeospatialData());
            UploadOVTAUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.BulkUploadOTVAs());
            UploadLandUseBlocksUrl = SitkaRoute<LandUseBlockGeometryController>.BuildUrlFromExpression(linkGenerator, x => x.UpdateLandUseBlockGeometry());
            DownloadLandUseBlocksUrl = SitkaRoute<LandUseBlockGeometryController>.BuildUrlFromExpression(linkGenerator, x => x.DownloadLandUseBlockGeometry());
            ParcelUploadUrl = SitkaRoute<ParcelLayerUploadController>.BuildUrlFromExpression(linkGenerator, x => x.UpdateParcelLayerGeometry());
            RegionalSubbasinRefreshUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator, x => x.RefreshFromOCSurvey());
            LandUseStatisticsRefreshUrl = SitkaRoute<HRUCharacteristicController>.BuildUrlFromExpression(LinkGenerator, x => x.RefreshHRUCharacteristics());
            ModelBasinsRefreshUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.RefreshModelBasinsFromOCSurvey());
            PrecipitationZonesRefreshUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.RefreshPrecipitationZonesFromOCSurvey());

            LastUpdatedRegionalSubbasins = lastUpdatedRegionalSubbasin;
            LastUpdatedHRUCharacteristics = lastUpdatedDateHRUCharacteristics;
            LastUpdatedModalBasins = lastUpdatedDateModelBasins;
            LastUpdatedPrecipitationZones = lastUpdatedDatePrecipitationZones;

            BMPListUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            FieldTripUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            ModelingAttributeUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.ViewTreatmentBMPModelingAttributes());
            BMPMapUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            BMPTypesUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            
            WQMPListUrl = SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
           
            OVTAListUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            TrashGeneratingUnitsAuditListUrl = SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            LandUseBlocksUrl = SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            
            ParcelMapUrl = SitkaRoute<ParcelController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            
            RSBListUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            RSBGridUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(linkGenerator, x => x.Grid());
            HRUCharacteristicsListUrl = SitkaRoute<HRUCharacteristicController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
        }
    }
}