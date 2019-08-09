using System.Collections.Generic;
using System.Linq;
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
            DroolToolMapConfig = new DroolToolMapConfig(NeptuneWebConfiguration.NominatimApiKey, NeptuneWebConfiguration.ParcelMapServiceUrl, HttpRequestStorage.DatabaseEntities.NetworkCatchments.Where(x => x.BackboneSegments.Any()).Select(x => x.NetworkCatchmentID).ToList());
        }

    }

    public class DroolToolMapConfig
    {
        public DroolToolMapConfig(string nominatimApiKey, string geoServerUrl, List<int> networkCatchmentsWhereItIsOkayToClickIDs)
        {
            NominatimApiKey = nominatimApiKey;
            GeoServerUrl = geoServerUrl;
            NetworkCatchmentsWhereItIsOkayToClickIDs = networkCatchmentsWhereItIsOkayToClickIDs;
            BackboneTraceUrlTemplate = new UrlTemplate<int>(SitkaRoute<BackboneController>.BuildUrlFromExpression(x => x.DownstreamBackboneFeatureCollection(UrlTemplate.Parameter1Int))).UrlTemplateString;
            StormshedUrlTemplate = new UrlTemplate<int>(SitkaRoute<BackboneController>.BuildUrlFromExpression(x => x.StormshedBackboneFeatureCollection(UrlTemplate.Parameter1Int))).UrlTemplateString;
            MetricUrlTemplate = new UrlTemplate<int>(SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(x =>
                    x.Metrics(UrlTemplate.Parameter1Int)))
                .UrlTemplateString;
        }

        public string MetricUrlTemplate { get; }

        public string StormshedUrlTemplate { get; }

        public string NominatimApiKey { get; }
        public string GeoServerUrl { get; }
        public string BackboneTraceUrlTemplate { get; }
        public List<int> NetworkCatchmentsWhereItIsOkayToClickIDs { get; }
    }
}