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
        public HtmlString ScoreHtmlString { get; }
        public string NewUrl { get; }
        public string EditDetailsUrl { get; }
        public OnlandVisualTrashAssessmentIndexGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public bool UserHasAssessmentAreaManagePermission { get; }
        public OVTAAreaMapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }

        public DetailViewData(Person currentPerson, Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea, OVTAAreaMapInitJson mapInitJson, string newUrl, string editDetailsUrl) : base(currentPerson)
        {
            EntityName = "Trash Module";
            EntityUrl = NeptuneArea.Trash.GetHomeUrl();
            SubEntityName = "OVTA Areas";
            PageTitle = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName;
            OnlandVisualTrashAssessmentArea = onlandVisualTrashAssessmentArea;
            MapInitJson = mapInitJson;
            var completedAssessments = OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessments.Where(x => x.OnlandVisualTrashAssessmentStatus == OnlandVisualTrashAssessmentStatus.Complete).ToList();

            LastAssessmentDateHtmlString = new HtmlString( completedAssessments.Max(x => x.CompletedDate)?.ToShortDateString() ?? "<p class='systemText'>No completed assessments</p>");
            ScoreHtmlString = new HtmlString(OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentScore != null
                ? OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentScore
                    .OnlandVisualTrashAssessmentScoreDisplayName
                : "<p class='systemText'>No completed assessments</p>");
            NewUrl = newUrl;
            EditDetailsUrl = editDetailsUrl;

            UserHasAssessmentAreaManagePermission = new OnlandVisualTrashAssessmentAreaViewFeature().HasPermission(currentPerson, OnlandVisualTrashAssessmentArea).HasPermission;

            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new OnlandVisualTrashAssessmentIndexGridSpec(currentPerson, showDelete, showEdit, false)
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
