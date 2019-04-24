using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.Delineation
{
    public class DelineationMapConfig
    {
        public string AutoDelineateBaseUrl { get; }
        public string JurisdictionCQLFilter { get; }
        public string DeleteDelineationBaseUrl { get; }


        // todo: source these values from Web.config when appropriate
        public DelineationMapConfig(string jurisdictionCQLFilter)
        {
            JurisdictionCQLFilter = jurisdictionCQLFilter;
            DeleteDelineationBaseUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(x => x.MapDelete(null));
            AutoDelineateBaseUrl =
                "https://ocgis.com/arcpub/rest/services/Flood/OCPWGlobalStormwaterDelineationServiceSurfaceOnly/GPServer/Global%20Surface%20Delineation/";
        }
    }
}