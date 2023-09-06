using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;

namespace Neptune.Web.Views.RegionalSubbasin
{
    public class RegionalSubbasinMapInitJson : MapInitJson
    {
        public RegionalSubbasinMapInitJson(string mapDivID) : base(mapDivID, DefaultZoomLevel, new List<LayerGeoJson>(), new BoundingBoxDto())
        {
        }
    }
}