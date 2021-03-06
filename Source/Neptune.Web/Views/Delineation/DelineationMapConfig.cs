﻿
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
        public string TreatmentBMPLocationUrlTemplate { get; }
        public string NewRegionalSubbasinRevisionRequestUrlTemplate { get; }

        public DelineationMapConfig(string jurisdictionCQLFilter)
        {
            JurisdictionCQLFilter = jurisdictionCQLFilter;

            CatchmentTraceUrlTemplate = new UrlTemplate<int>(SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(x => x.UpstreamDelineation(UrlTemplate.Parameter1Int))).UrlTemplateString;
            DeleteDelineationUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(x => x.MapDelete(UrlTemplate.Parameter1Int))).UrlTemplateString;
            ChangeDelineationStatusUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(x=>x.ChangeDelineationStatus(UrlTemplate.Parameter1Int))).UrlTemplateString;
            TreatmentBMPDelineationUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(x=>x.ForTreatmentBMP(UrlTemplate.Parameter1Int))).UrlTemplateString;
            TreatmentBMPLocationUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.EditLocationFromDelineationMap(UrlTemplate.Parameter1Int))).UrlTemplateString;
            AutoDelineateBaseUrl =
                "https://ocgis.com/arcpub/rest/services/Flood/OCPWGlobalStormwaterDelineationServiceSurfaceOnly/GPServer/Global%20Surface%20Delineation/";
            NewRegionalSubbasinRevisionRequestUrlTemplate = new UrlTemplate<int>(SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(x=>x.New(UrlTemplate.Parameter1Int))).UrlTemplateString;
        }
    }
}
