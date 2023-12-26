using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using NetTopologySuite.Features;

namespace Neptune.WebMvc.Views.TrashHome
{
    public class TrashModuleMapInitJson : MapInitJson
    {
        public FeatureCollection TreatmentBMPGeoJson { get; }
        public FeatureCollection WQMPGeoJson { get; }

        public TrashModuleMapInitJson(string mapDivID, FeatureCollection treatmentBMPGeoJson,
            FeatureCollection wqmpGeoJson, BoundingBoxDto boundingBox, List<LayerGeoJson> layerGeoJsons)
            : base(mapDivID, DefaultZoomLevel, layerGeoJsons, 
                boundingBox)
        {
            TreatmentBMPGeoJson = treatmentBMPGeoJson;
            WQMPGeoJson = wqmpGeoJson;
        }

        // needed by serializer
        public TrashModuleMapInitJson()
        {
        }
    }
}