using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTAObservationsMapInitJson : MapInitJson
    {
        public LayerGeoJson ObservationsLayerGeoJson { get; set; }
        
        public LayerGeoJson AssessmentAreaLayerGeoJson { get; set; }

        public OVTAObservationsMapInitJson(string mapDivID, LayerGeoJson observationsLayerGeoJson, BoundingBox boundingBox)
            : base(mapDivID, DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
                boundingBox)
        {
            ObservationsLayerGeoJson = observationsLayerGeoJson;
        }

        public OVTAObservationsMapInitJson(string mapDivID,
            LayerGeoJson observationsLayerGeoJson, LayerGeoJson assessmentAreaLayerGeoJson) : this(mapDivID, observationsLayerGeoJson, BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(new List<LayerGeoJson>{observationsLayerGeoJson, assessmentAreaLayerGeoJson}))
        {
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
        }

        public static LayerGeoJson MakeObservationsLayerGeoJson(IEnumerable<OnlandVisualTrashAssessmentObservation> observations)
        {
            var featureCollection = observations.ToGeoJsonFeatureCollection();
            var observationsLayerGeoJson = new LayerGeoJson("Observations", featureCollection, "#FF00FF", 1, LayerInitialVisibility.Show) { EnablePopups = false };
            return observationsLayerGeoJson;
        }
    }
}
