using System.Linq;
using LtInfo.Common;
using Neptune.Web.Areas.DroolTool.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.DroolTool.Views.Home
{
    public class IndexViewData : DroolToolModuleViewData
    {
        public IndexViewData(Person currentPerson, NeptunePage neptunePage) : base(currentPerson, neptunePage, true)
        {
            EntityName = "Urban Drool Tool";
            PageTitle = "Welcome";

            GeoserverUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
            StormwaterMapInitJson = new StormwaterMapInitJson("droolToolMap", MapInitJson.DefaultZoomLevel,
                MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(), new BoundingBox(HttpRequestStorage.DatabaseEntities.Watersheds.Select(x=>x.WatershedGeometry)));

            DroolToolMapConfig = new DroolToolMapConfig(NeptuneWebConfiguration.NominatimApiKey, NeptuneWebConfiguration.ParcelMapServiceUrl);
        }

        public StormwaterMapInitJson StormwaterMapInitJson { get; }
        public string GeoserverUrl { get; }
        public DroolToolMapConfig DroolToolMapConfig { get; }
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