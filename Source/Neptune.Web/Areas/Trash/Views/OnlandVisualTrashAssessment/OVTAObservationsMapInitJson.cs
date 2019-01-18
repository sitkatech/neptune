using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTAObservationsMapInitJson : MapInitJson
    {
        public LayerGeoJson ObservationsLayerGeoJson { get; set; }
        

        public OVTAObservationsMapInitJson(string mapDivID, LayerGeoJson observationsLayerGeoJson)
            : base(mapDivID, DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
                BoundingBox.MakeNewDefaultBoundingBox())
        {
            ObservationsLayerGeoJson = observationsLayerGeoJson;
        }

        public static LayerGeoJson MakeObservationsLayerGeoJson(IEnumerable<OnlandVisualTrashAssessmentObservation> observations)
        {
            var featureCollection = observations.ToGeoJsonFeatureCollection();
            var observationsLayerGeoJson = new LayerGeoJson("Observations", featureCollection, "#FF00FF", 1, LayerInitialVisibility.Show) { EnablePopups = false };
            return observationsLayerGeoJson;
        }
    }
}