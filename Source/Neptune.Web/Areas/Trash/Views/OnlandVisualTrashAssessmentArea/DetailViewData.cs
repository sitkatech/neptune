using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentArea
{
    public class DetailViewData : TrashModuleViewData
    {
        public Models.OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea { get; }
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

        public DetailViewData(Person currentPerson, Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, OVTAAreaMapInitJson mapInitJson, string newUrl, string editDetailsUrl, string confirmEditLocationUrl) : base(currentPerson)
        {
            EntityName = "OVTA Areas";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index());
            PageTitle = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName;
            OnlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentArea;
            MapInitJson = mapInitJson;
            var completedAssessments = OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Where(x => x.OnlandVisualTrashAssessmentStatus == OnlandVisualTrashAssessmentStatus.Complete).ToList();

            LastAssessmentDateHtmlString = new HtmlString( completedAssessments.Max(x => x.CompletedDate)?.ToShortDateString() ?? "<p class='systemText'>No completed assessments</p>");
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


            UserHasAssessmentAreaManagePermission = new OnlandVisualTrashAssessmentAreaViewFeature().HasPermission(currentPerson, OnlandVisualTrashAssessmentArea).HasPermission;
            UserHasEditLocationPermission = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);


            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            var userCanView = new OnlandVisualTrashAssessmentViewFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new OnlandVisualTrashAssessmentIndexGridSpec(currentPerson, showDelete, showEdit, false, userCanView)
            {
                ObjectNameSingular = "Assessment",
                ObjectNamePlural = "Assessments",
                SaveFiltersInCookie = true
            };
            GridName = "onlandVisualTrashAssessmentsGrid";
            GridDataUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(j => j.OVTAGridJsonDataForAreaDetails(onlandVisualTrashAssessmentArea));

            GeoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
        }
    }

    public class OVTAAreaMapInitJson : MapInitJson
    {
        public LayerGeoJson AssessmentAreaLayerGeoJson { get; set; }
        public LayerGeoJson TransectLineLayerGeoJson { get; set; }
        public LayerGeoJson ObservationsLayerGeoJson { get; }

        public OVTAAreaMapInitJson(string mapDivID,
            LayerGeoJson assessmentAreaLayerGeoJson, LayerGeoJson transectLineLayerGeoJson,
            LayerGeoJson observationsLayerGeoJson) : base(mapDivID, DefaultZoomLevel,
            MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
            BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(new List<LayerGeoJson> { assessmentAreaLayerGeoJson }))
        {
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
            TransectLineLayerGeoJson = transectLineLayerGeoJson;
            ObservationsLayerGeoJson = observationsLayerGeoJson;
        }
    }
}
