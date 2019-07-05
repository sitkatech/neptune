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
        }

        public string NominatimApiKey { get; }
        public string GeoServerUrl { get; }
        public string BackboneTraceUrlTemplate { get; }
    }
}