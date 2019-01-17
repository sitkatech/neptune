using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class OVTAObservationsMapInitJson : MapInitJson
    {

        public OVTAObservationsMapInitJson(string mapDivID)
            : base(mapDivID, DefaultZoomLevel, MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(),
                BoundingBox.MakeNewDefaultBoundingBox())
        {
        }

    }
}