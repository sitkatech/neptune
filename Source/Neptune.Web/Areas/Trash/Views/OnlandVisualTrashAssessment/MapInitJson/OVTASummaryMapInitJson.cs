using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTASummaryMapInitJson : OVTAObservationsMapInitJson
    {
        public OVTASummaryMapInitJson(string mapDivID, LayerGeoJson observationsLayerGeoJson, LayerGeoJson assessmentAreaLayerGeoJson) :
            base(mapDivID, observationsLayerGeoJson, BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(new List<LayerGeoJson>{assessmentAreaLayerGeoJson}))
        {
            AssessmentAreaLayerGeoJson = assessmentAreaLayerGeoJson;
        }
    }
}
