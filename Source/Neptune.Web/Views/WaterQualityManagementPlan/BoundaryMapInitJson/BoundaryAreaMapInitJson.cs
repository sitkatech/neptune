using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.WaterQualityManagementPlan.BoundaryMapInitJson
{
    public class BoundaryAreaMapInitJson : MapInitJson
    {
        public LayerGeoJson BoundaryLayerGeoJson { get; set; }

        public BoundaryAreaMapInitJson(string mapDivID, LayerGeoJson boundaryLayerGeoJson) : base(mapDivID,
            DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
            BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(new List<LayerGeoJson>
            {
                boundaryLayerGeoJson
            }))
        {
            BoundaryLayerGeoJson = boundaryLayerGeoJson;
        }
    }
}

