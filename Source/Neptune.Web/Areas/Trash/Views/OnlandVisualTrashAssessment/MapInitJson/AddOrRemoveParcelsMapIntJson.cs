using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class AddOrRemoveParcelsMapIntJson : MapInitJson
    {
        public LayerGeoJson ObservationsLayerGeoJson { get; set; }

        public AddOrRemoveParcelsMapIntJson(string mapDivID, LayerGeoJson observationsLayerGeoJson) : base(mapDivID,
            DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
            BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(new List<LayerGeoJson>
            {
                observationsLayerGeoJson
            }))
        {
            ObservationsLayerGeoJson = observationsLayerGeoJson;
        }
    }
}