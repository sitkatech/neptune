using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class LandSurfaceLoadingRequest
    {
        [JsonProperty("land_surfaces")]
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