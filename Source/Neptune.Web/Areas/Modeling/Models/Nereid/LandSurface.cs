using Neptune.Web.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class LandSurface
    {
        [JsonProperty("node_id")]
        public string NodeID { get; set; }
        [JsonProperty("surface_key")]
        public string SurfaceKey { get; set; }
        [JsonProperty("area_acres")]
        public double Area { get; set; }
        [JsonProperty("imp_area_acres")]
        public double ImperviousArea { get; set; }

        public LandSurface()
        {

        }

        public LandSurface(vNereidLoadingInput vNereidLoadingInput)
        {
            NodeID = NereidUtilities.LandSurfaceNodeID(vNereidLoadingInput);
            SurfaceKey =
                $"{vNereidLoadingInput.LSPCBasinKey}-{vNereidLoadingInput.HRUCharacteristicLandUseCodeName}-{vNereidLoadingInput.HydrologicSoilGroup}-{vNereidLoadingInput.SlopePercentage}";
            Area = vNereidLoadingInput.Area;
            ImperviousArea = vNereidLoadingInput.ImperviousAcres;
        }
    }
}