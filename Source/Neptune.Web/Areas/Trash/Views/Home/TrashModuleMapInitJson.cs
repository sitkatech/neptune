using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.Home
{
    public class TrashModuleMapInitJson : MapInitJson
    {
        public LayerGeoJson TreatmentBMPLayerGeoJson { get; }
        public LayerGeoJson ParcelLayerGeoJson { get; }

        public TrashModuleMapInitJson(string mapDivID, LayerGeoJson treatmentBMPLayerGeoJson,
            LayerGeoJson parcelLayerGeoJson, BoundingBox boundingBox)
            : base(mapDivID, DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
                boundingBox)
        {
            TreatmentBMPLayerGeoJson = treatmentBMPLayerGeoJson;
            ParcelLayerGeoJson = parcelLayerGeoJson;
        }
    }
}