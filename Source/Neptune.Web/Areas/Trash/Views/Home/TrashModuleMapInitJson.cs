using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.Home
{
    public class TrashModuleMapInitJson : MapInitJson
    {
        public LayerGeoJson TreatmentBMPLayerGeoJson { get; }
        public LayerGeoJson ParcelLayerGeoJson { get; }

        public TrashModuleMapInitJson(string mapDivID, LayerGeoJson treatmentBMPLayerGeoJson,
            LayerGeoJson parcelLayerGeoJson)
            : base(mapDivID, DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
                BoundingBox.MakeNewDefaultBoundingBox())
        {
            TreatmentBMPLayerGeoJson = treatmentBMPLayerGeoJson;
            ParcelLayerGeoJson = parcelLayerGeoJson;
        }

        public static LayerGeoJson MakeTreatmentBMPLayerGeoJsonForTrashMap(IEnumerable<Models.TreatmentBMP> treatmentBMPs, bool enablePopups)
        {
            var featureCollection = treatmentBMPs.ToGeoJsonFeatureCollectionForTrashMap();

            var treatmentBMPLayerGeoJson = new LayerGeoJson("Treatment BMPs", featureCollection, "blue", 1, LayerInitialVisibility.Show) {EnablePopups = enablePopups};
            return treatmentBMPLayerGeoJson;
        }

        public static LayerGeoJson MakeParcelLayerGeoJsonForTrashMap(IEnumerable<Models.Parcel> parcels, bool enablePopups)
        {
            var featureCollection = parcels.ToGeoJsonFeatureCollectionForTrashMap();

            var parcelLayerGeoJson = new LayerGeoJson("Parcels", featureCollection, "blue", 1, LayerInitialVisibility.Show) {EnablePopups = enablePopups};
            return parcelLayerGeoJson;
        }
    }
}