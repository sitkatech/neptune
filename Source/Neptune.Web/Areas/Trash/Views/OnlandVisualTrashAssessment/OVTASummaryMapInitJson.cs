using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTASummaryMapInitJson : OVTAObservationsMapInitJson
    {
        public LayerGeoJson AssessmentAreaLayerGeoJson { get; }

        public OVTASummaryMapInitJson(string mapDivID, LayerGeoJson observationsLayerGeoJson, LayerGeoJson assessmentAreaLayerGeoJson) : base(mapDivID, observationsLayerGeoJson)
        {
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
        }

        public static LayerGeoJson MakeObservationsLayerGeoJson(Models.OnlandVisualTrashAssessment ovta)
        {
            var featureCollection = ovta.GetAssessmentAreaFeatureCollection();
            var observationsLayerGeoJson = new LayerGeoJson("Observations", featureCollection, "#FF00FF", 1, LayerInitialVisibility.Show) { EnablePopups = false };
            return observationsLayerGeoJson;
        }
    }
}