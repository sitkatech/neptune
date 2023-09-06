using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;

namespace Neptune.Web.Views.WaterQualityManagementPlan.BoundaryMapInitJson
{
    public class BoundaryAreaMapInitJson : MapInitJson
    {
        public LayerGeoJson BoundaryLayerGeoJson { get; set; }

        public BoundaryAreaMapInitJson(string mapDivID, LayerGeoJson boundaryLayerGeoJson, List<LayerGeoJson> layerGeoJsons, BoundingBoxDto boundingBoxDto) : base(mapDivID, DefaultZoomLevel, layerGeoJsons, boundingBoxDto)
        {
            BoundaryLayerGeoJson = boundaryLayerGeoJson;
        }
    }
}

