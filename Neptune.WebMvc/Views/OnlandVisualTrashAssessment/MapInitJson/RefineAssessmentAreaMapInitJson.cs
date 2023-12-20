using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment.MapInitJson
{
    public class RefineAssessmentAreaMapInitJson : Common.MapInitJson
    {
        public LayerGeoJson ObservationsLayerGeoJson { get; set; }
        public LayerGeoJson AssessmentAreaLayerGeoJson { get; set; }
        public LayerGeoJson TransectLineLayerGeoJson { get; }

        public RefineAssessmentAreaMapInitJson(string mapDivID, LayerGeoJson observationsLayerGeoJson,
            LayerGeoJson assessmentAreaLayerGeoJson, LayerGeoJson transectLineLayerGeoJson, List<LayerGeoJson> layerGeoJsons, BoundingBoxDto boundingBoxDto) : base(mapDivID,
            DefaultZoomLevel, layerGeoJsons, boundingBoxDto)
        {
            ObservationsLayerGeoJson = observationsLayerGeoJson;
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
            TransectLineLayerGeoJson = transectLineLayerGeoJson;
        }

        // needed by serializer
        public RefineAssessmentAreaMapInitJson()
        {
        }
    }
}
