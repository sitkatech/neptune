using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment.MapInitJson
{
    public class OVTASummaryMapInitJson : Common.MapInitJson
    {
        public LayerGeoJson ObservationsLayerGeoJson { get; set; }
        public LayerGeoJson AssessmentAreaLayerGeoJson { get; set; }
        public LayerGeoJson TransectLineLayerGeoJson { get; set; }

        public OVTASummaryMapInitJson(string mapDivID, LayerGeoJson observationsLayerGeoJson,
            LayerGeoJson assessmentAreaLayerGeoJson, LayerGeoJson transectLineLayerGeoJson, List<LayerGeoJson> layerGeoJsons, BoundingBoxDto boundingBoxDto) : base(mapDivID, DefaultZoomLevel,
            layerGeoJsons, boundingBoxDto)
        {
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
            TransectLineLayerGeoJson = transectLineLayerGeoJson;
            ObservationsLayerGeoJson = observationsLayerGeoJson;
        }

        // needed by serializer
        public OVTASummaryMapInitJson()
        {
        }
    }
}
