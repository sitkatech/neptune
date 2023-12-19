using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Areas.Trash.Views.Home
{
    public class TrashModuleMapInitJson : MapInitJson
    {
        public LayerGeoJson TreatmentBMPLayerGeoJson { get; }
        public LayerGeoJson ParcelLayerGeoJson { get; }

        public TrashModuleMapInitJson(string mapDivID, LayerGeoJson treatmentBMPLayerGeoJson,
            LayerGeoJson parcelLayerGeoJson, BoundingBoxDto boundingBox, List<LayerGeoJson> layerGeoJsons)
            : base(mapDivID, DefaultZoomLevel, layerGeoJsons, 
                boundingBox)
        {
            TreatmentBMPLayerGeoJson = treatmentBMPLayerGeoJson;
            ParcelLayerGeoJson = parcelLayerGeoJson;
        }

        // needed by serializer
        public TrashModuleMapInitJson()
        {
        }
    }
}