using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Views.RegionalSubbasin
{
    public class RegionalSubbasinMapInitJson : MapInitJson
    {
        public RegionalSubbasinMapInitJson(string mapDivID) : base(mapDivID, DefaultZoomLevel, new List<LayerGeoJson>(), BoundingBox.MakeNewDefaultBoundingBox())
        {
        }
    }
}