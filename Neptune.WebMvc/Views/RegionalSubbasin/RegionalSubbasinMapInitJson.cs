using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.RegionalSubbasin
{
    public class RegionalSubbasinMapInitJson : MapInitJson
    {
        public RegionalSubbasinMapInitJson(string mapDivID) : base(mapDivID, DefaultZoomLevel, new List<LayerGeoJson>(), new BoundingBoxDto())
        {
        }
    }
}