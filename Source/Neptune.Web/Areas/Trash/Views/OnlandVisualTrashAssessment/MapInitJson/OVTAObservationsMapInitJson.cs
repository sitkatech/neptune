using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

// ReSharper disable once CheckNamespace
namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTAObservationsMapInitJson : MapInitJson
    {
        public LayerGeoJson ObservationsLayerGeoJson { get; set; }

        public LayerGeoJson AssessmentAreaLayerGeoJson { get; set; }
        public LayerGeoJson TransectLineLayerGeoJson { get; }

        public OVTAObservationsMapInitJson(string mapDivID,
            LayerGeoJson observationsLayerGeoJson, LayerGeoJson assessmentAreaLayerGeoJson,
            LayerGeoJson transectLineLayerGeoJson) : base(mapDivID,
            DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
            BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(new List<LayerGeoJson>
            {
                observationsLayerGeoJson,
                assessmentAreaLayerGeoJson
            }))
        {
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
            TransectLineLayerGeoJson = transectLineLayerGeoJson;
            ObservationsLayerGeoJson = observationsLayerGeoJson;
        }
    }
}
