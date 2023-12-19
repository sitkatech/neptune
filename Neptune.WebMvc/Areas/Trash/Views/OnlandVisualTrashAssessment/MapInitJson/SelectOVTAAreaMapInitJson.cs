using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment.MapInitJson
{
    public class SelectOVTAAreaMapInitJson : Common.MapInitJson
    {
        public LayerGeoJson AssessmentAreaLayerGeoJson { get; }

        public SelectOVTAAreaMapInitJson(string mapDivID, LayerGeoJson assessmentAreaLayerGeoJson, List<LayerGeoJson> layerGeoJsons, BoundingBoxDto boundingBoxDto) :
            base(mapDivID, DefaultZoomLevel, layerGeoJsons, boundingBoxDto)
        {
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
        }

        // needed by serializer
        public SelectOVTAAreaMapInitJson()
        {
        }
    }
}
