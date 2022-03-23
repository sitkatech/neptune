using Neptune.Web.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

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

        public LandSurface(vNereidLoadingInput vNereidLoadingInput, bool isBaselineCondition, List<int> projectDelineationIDs = null)
        {
            var landUseCode = isBaselineCondition
                ? vNereidLoadingInput.BaselineLandUseCode
                : vNereidLoadingInput.LandUseCode;

            var imperviousAcres = isBaselineCondition
                ? vNereidLoadingInput.BaselineImperviousAcres
                : vNereidLoadingInput.ImperviousAcres;

            NodeID = NereidUtilities.LandSurfaceNodeID(vNereidLoadingInput, projectDelineationIDs);
            SurfaceKey =
                $"{vNereidLoadingInput.ModelBasinKey}-{landUseCode}-{vNereidLoadingInput.HydrologicSoilGroup}-{vNereidLoadingInput.SlopePercentage}";
            Area = vNereidLoadingInput.Area;
            ImperviousArea = imperviousAcres;
        }
    }
}