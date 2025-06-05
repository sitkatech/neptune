using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.RegionalSubbasin
{
    public class RegionalSubbasinMapInitJson : MapInitJson
    {
        public RegionalSubbasinMapInitJson(string mapDivID) : base(mapDivID, 13, new List<LayerGeoJson>(), new BoundingBoxDto())
        {
        }
    }
}