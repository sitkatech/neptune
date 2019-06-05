
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.Delineation
{
    public class DelineationMapConfig
    {
        public string AutoDelineateBaseUrl { get; }
        public string JurisdictionCQLFilter { get; }
        public string DeleteDelineationUrlTemplate { get; }
        public string CatchmentTraceUrlTemplate { get; }
        public string ChangeDelineationStatusUrlTemplate { get; }
        public string TreatmentBMPDelineationUrlTemplate { get; }

        public DelineationMapConfig(string jurisdictionCQLFilter)
        {
            JurisdictionCQLFilter = jurisdictionCQLFilter;

            CatchmentTraceUrlTemplate = new UrlTemplate<int>(SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(x => x.UpstreamDelineation(UrlTemplate.Parameter1Int))).UrlTemplateString;
            DeleteDelineationUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(x => x.MapDelete(UrlTemplate.Parameter1Int))).UrlTemplateString;
            ChangeDelineationStatusUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(x=>x.ChangeDelineationStatus(UrlTemplate.Parameter1Int))).UrlTemplateString;
            TreatmentBMPDelineationUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(x=>x.ForTreatmentBMP(UrlTemplate.Parameter1Int))).UrlTemplateString;

            AutoDelineateBaseUrl =
                "https://ocgis.com/arcpub/rest/services/Flood/OCPWGlobalStormwaterDelineationServiceSurfaceOnly/GPServer/Global%20Surface%20Delineation/";
        }
    }
}
