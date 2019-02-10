using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTASummaryMapInitJson : MapInitJson
    {
        public LayerGeoJson ObservationsLayerGeoJson { get; set; }
        public LayerGeoJson AssessmentAreaLayerGeoJson { get; set; }

        public OVTASummaryMapInitJson(string mapDivID, LayerGeoJson observationsLayerGeoJson,
            LayerGeoJson assessmentAreaLayerGeoJson) : base(mapDivID, DefaultZoomLevel,
            MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
            BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(new List<LayerGeoJson> {assessmentAreaLayerGeoJson}))
        {
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
            ObservationsLayerGeoJson = observationsLayerGeoJson;
        }
    }
}
