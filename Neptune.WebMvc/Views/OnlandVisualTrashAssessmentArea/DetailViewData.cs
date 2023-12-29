using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessmentArea
{
    public class DetailViewData : TrashModuleViewData
    {
        public Neptune.EFModels.Entities.OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea { get; }
        public HtmlString LastAssessmentDateHtmlString { get; }
        public HtmlString BaselineScoreHtmlString { get; }
        public HtmlString ProgressScoreHtmlString { get; }
        public string NewUrl { get; }
        public string EditDetailsUrl { get; }
        public string ConfirmEditLocationUrl { get; }
        public OnlandVisualTrashAssessmentIndexGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public bool UserHasAssessmentAreaManagePermission { get; }
        public bool UserHasEditLocationPermission { get; }
        public OVTAAreaMapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }
        public string StormwaterJurisdictionDetailUrl { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            WebConfiguration webConfiguration,
            EFModels.Entities.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea,
            OVTAAreaMapInitJson mapInitJson, string newUrl, string editDetailsUrl, string confirmEditLocationUrl,
            string geoServerUrl, List<EFModels.Entities.OnlandVisualTrashAssessment> onlandVisualTrashAssessments) : base(httpContext, linkGenerator, currentPerson, webConfiguration)
        {
            EntityName = "OVTA Areas";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName;
            OnlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentArea;
            MapInitJson = mapInitJson;

            LastAssessmentDateHtmlString = new HtmlString( onlandVisualTrashAssessments.Max(x => x.CompletedDate)?.ToShortDateString() ?? "<p class='systemText'>No completed assessments</p>");
            ProgressScoreHtmlString = new HtmlString(OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScore != null
                ? OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScore
                    .OnlandVisualTrashAssessmentScoreDisplayName
                : "<p class='systemText'>No completed assessments</p>");
            BaselineScoreHtmlString = new HtmlString(OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore != null
                ? OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore
                    .OnlandVisualTrashAssessmentScoreDisplayName
                : "<p class='systemText'>No completed assessments</p>");
            NewUrl = newUrl;
            EditDetailsUrl = editDetailsUrl;
            ConfirmEditLocationUrl = confirmEditLocationUrl;


            UserHasAssessmentAreaManagePermission = new OnlandVisualTrashAssessmentAreaEditFeature().HasPermission(currentPerson, OnlandVisualTrashAssessmentArea).HasPermission;
            UserHasEditLocationPermission = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);

            GridSpec = new OnlandVisualTrashAssessmentIndexGridSpec(linkGenerator, currentPerson, false)
            {
                ObjectNameSingular = "Assessment",
                ObjectNamePlural = "Assessments",
                SaveFiltersInCookie = true
            };
            GridName = "onlandVisualTrashAssessmentsGrid";
            GridDataUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.OVTAGridJsonDataForAreaDetails(onlandVisualTrashAssessmentArea));

            GeoServerUrl = geoServerUrl;
            StormwaterJurisdictionDetailUrl = SitkaRoute<JurisdictionController>.BuildUrlFromExpression(LinkGenerator, x => x.Detail(onlandVisualTrashAssessmentArea.StormwaterJurisdictionID));
        }
    }

    public class OVTAAreaMapInitJson : MapInitJson
    {
        public LayerGeoJson AssessmentAreaLayerGeoJson { get; set; }
        public LayerGeoJson TransectLineLayerGeoJson { get; set; }
        public LayerGeoJson ObservationsLayerGeoJson { get; }

        public OVTAAreaMapInitJson(string mapDivID,
            LayerGeoJson assessmentAreaLayerGeoJson, LayerGeoJson transectLineLayerGeoJson,
            LayerGeoJson observationsLayerGeoJson, List<LayerGeoJson> layerGeoJsons, BoundingBoxDto boundingBoxDto) : base(mapDivID, DefaultZoomLevel,
            layerGeoJsons, boundingBoxDto)
        {
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
            TransectLineLayerGeoJson = transectLineLayerGeoJson;
            ObservationsLayerGeoJson = observationsLayerGeoJson;
        }

        // needed by serializer
        public OVTAAreaMapInitJson()
        {
        }
    }
}
