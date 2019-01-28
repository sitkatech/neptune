using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class SelectOVTAAreaMapInitJson : MapInitJson
    {
        public LayerGeoJson AssessmentAreaLayerGeoJson { get; }

        public SelectOVTAAreaMapInitJson(string mapDivID, LayerGeoJson assessmentAreaLayerGeoJson) :
            base(mapDivID, DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(), BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(new List<LayerGeoJson>{assessmentAreaLayerGeoJson}))
        {
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
        }

        public static LayerGeoJson MakeAssessmentAreasLayerGeoJson(IEnumerable<OnlandVisualTrashAssessmentArea> observations)
        {
            var featureCollection = observations.ToGeoJsonFeatureCollection();
            var observationsLayerGeoJson = new LayerGeoJson("Observations", featureCollection, "#FF00FF", 1, LayerInitialVisibility.Show) { EnablePopups = false };
            return observationsLayerGeoJson;
        }
    }
}
