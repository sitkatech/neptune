using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment.MapInitJson
{
    public class AddOrRemoveParcelsMapIntJson : Common.MapInitJson
    {
        public LayerGeoJson ObservationsLayerGeoJson { get; set; }
        public LayerGeoJson TransectLineLayerGeoJson { get; }

        public AddOrRemoveParcelsMapIntJson(string mapDivID, LayerGeoJson observationsLayerGeoJson,
            LayerGeoJson transectLineLayerGeoJson, List<LayerGeoJson> layerGeoJsons, BoundingBoxDto boundingBoxDto) : base(mapDivID,
            DefaultZoomLevel, layerGeoJsons, boundingBoxDto)
        {
            ObservationsLayerGeoJson = observationsLayerGeoJson;
            TransectLineLayerGeoJson = transectLineLayerGeoJson;
        }

        // needed by serializer
        public AddOrRemoveParcelsMapIntJson()
        {
        }
    }
}
