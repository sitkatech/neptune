using System.Text.Json.Serialization;
using Neptune.EFModels.Entities;

namespace Neptune.EFModels.Nereid
{
    public class LandSurfaceLoadingRequest
    {
        [JsonPropertyName("land_surfaces")]
        public List<LandSurface> LandSurfaces { get; set; }

        public LandSurfaceLoadingRequest()
        {

        }

        public LandSurfaceLoadingRequest(List<vNereidLoadingInput> vNereidLoadingInputs, bool isBaselineCondition)
        {
            // this is only used by the test endpoints
            LandSurfaces = vNereidLoadingInputs.Select(x => new LandSurface(x, isBaselineCondition)).ToList();
        }
    }
}