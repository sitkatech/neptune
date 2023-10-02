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
        public string TreatmentBMPLocationUrlTemplate { get; }
        public string NewRegionalSubbasinRevisionRequestUrlTemplate { get; }

        public DelineationMapConfig(LinkGenerator linkGenerator, string jurisdictionCQLFilter, string autoDelineateServiceUrl)
        {
            JurisdictionCQLFilter = jurisdictionCQLFilter;

            CatchmentTraceUrlTemplate = new UrlTemplate<int>(SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(linkGenerator, x => x.UpstreamDelineation(UrlTemplate.Parameter1Int))).UrlTemplateString;
            DeleteDelineationUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.MapDelete(UrlTemplate.Parameter1Int))).UrlTemplateString;
            ChangeDelineationStatusUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.ChangeDelineationStatus(UrlTemplate.Parameter1Int))).UrlTemplateString;
            TreatmentBMPDelineationUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.ForTreatmentBMP(UrlTemplate.Parameter1Int))).UrlTemplateString;
            TreatmentBMPLocationUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.EditLocationFromDelineationMap(UrlTemplate.Parameter1Int))).UrlTemplateString;
            AutoDelineateBaseUrl = autoDelineateServiceUrl;
            NewRegionalSubbasinRevisionRequestUrlTemplate = new UrlTemplate<int>(SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(linkGenerator, x => x.New(UrlTemplate.Parameter1Int))).UrlTemplateString;
        }
    }
}
