using LtInfo.Common;
using Neptune.Web.Areas.DroolTool.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.DroolTool.Views.Home
{
    public class IndexViewData : DroolToolModuleViewData
    {
        public bool FirstTimeVisit { get; }

        public DroolToolMapInitJson StormwaterMapInitJson { get; }
        public string GeoserverUrl { get; }
        public DroolToolMapConfig DroolToolMapConfig { get; }

        public IndexViewData(Person currentPerson, NeptunePage neptunePage, bool firstTimeVisit, DroolToolMapInitJson mapInitJson) : base(currentPerson, neptunePage, true)
        {
            FirstTimeVisit = firstTimeVisit;
            EntityName = "Urban Drool Tool";
            PageTitle = "Welcome";

            GeoserverUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;

            StormwaterMapInitJson = mapInitJson;
            DroolToolMapConfig = new DroolToolMapConfig(NeptuneWebConfiguration.NominatimApiKey, NeptuneWebConfiguration.ParcelMapServiceUrl);
        }
    }

    public class DroolToolMapConfig
    {
        public DroolToolMapConfig(string nominatimApiKey, string geoServerUrl)
        {
            NominatimApiKey = nominatimApiKey;
            GeoServerUrl = geoServerUrl;
            BackboneTraceUrlTemplate = new UrlTemplate<int>(SitkaRoute<BackboneController>.BuildUrlFromExpression(x => x.DownstreamBackboneFeatureCollection(UrlTemplate.Parameter1Int))).UrlTemplateString;
            StormshedUrlTemplate = new UrlTemplate<int>(SitkaRoute<BackboneController>.BuildUrlFromExpression(x => x.StormshedBackboneFeatureCollection(UrlTemplate.Parameter1Int))).UrlTemplateString;
            MetricUrlTemplate = new UrlTemplate<int>(SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(x =>
                    x.Metrics(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int, UrlTemplate.Parameter3Int)))
                .UrlTemplateString;
        }

        public string MetricUrlTemplate { get; }

        public string StormshedUrlTemplate { get; }

        public string NominatimApiKey { get; }
        public string GeoServerUrl { get; }
        public string BackboneTraceUrlTemplate { get; }
    }
}