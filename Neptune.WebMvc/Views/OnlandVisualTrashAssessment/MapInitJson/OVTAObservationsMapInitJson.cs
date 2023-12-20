using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment.MapInitJson
{
    public class OVTAObservationsMapInitJson : Common.MapInitJson
    {
        public LayerGeoJson ObservationsLayerGeoJson { get; set; }

        public LayerGeoJson AssessmentAreaLayerGeoJson { get; set; }
        public LayerGeoJson TransectLineLayerGeoJson { get; }

        public OVTAObservationsMapInitJson(string mapDivID,
            LayerGeoJson observationsLayerGeoJson, LayerGeoJson assessmentAreaLayerGeoJson,
            LayerGeoJson transectLineLayerGeoJson, List<LayerGeoJson> layerGeoJsons, BoundingBoxDto boundingBoxDto) : base(mapDivID,
            DefaultZoomLevel, layerGeoJsons, boundingBoxDto)
        {
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
            TransectLineLayerGeoJson = transectLineLayerGeoJson;
            ObservationsLayerGeoJson = observationsLayerGeoJson;
        }

        // needed by serializer
        public OVTAObservationsMapInitJson()
        {
        }
    }
}
