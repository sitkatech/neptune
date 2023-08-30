using System.Text.Json.Serialization;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Common.Models.Nereid
{
    public class LandSurface
    {
        [JsonPropertyName("node_id")]
        public string NodeID { get; set; }
        [JsonPropertyName("surface_key")]
        public string SurfaceKey { get; set; }
        [JsonPropertyName("area_acres")]
        public double Area { get; set; }
        [JsonPropertyName("imp_area_acres")]
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