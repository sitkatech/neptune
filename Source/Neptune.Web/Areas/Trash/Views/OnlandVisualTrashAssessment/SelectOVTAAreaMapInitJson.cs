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
    }
}
