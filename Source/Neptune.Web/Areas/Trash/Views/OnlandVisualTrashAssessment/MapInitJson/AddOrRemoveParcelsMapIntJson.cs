using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class AddOrRemoveParcelsMapIntJson : MapInitJson
    {
        public LayerGeoJson ObservationsLayerGeoJson { get; set; }
        public LayerGeoJson TransectLineLayerGeoJson { get; }

        public AddOrRemoveParcelsMapIntJson(string mapDivID, LayerGeoJson observationsLayerGeoJson,
            LayerGeoJson transectLineLayerGeoJson) : base(mapDivID,
            DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
            BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(new List<LayerGeoJson>
            {
                observationsLayerGeoJson
            }))
        {
            ObservationsLayerGeoJson = observationsLayerGeoJson;
            TransectLineLayerGeoJson = transectLineLayerGeoJson;
        }
    }
}
