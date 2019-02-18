using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.NetworkCatchment
{
    public class IndexViewData : NeptuneViewData
    {
        public MapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }
        public string NetworkCatchmentLayerName { get; }

        public IndexViewData(Person currentPerson, MapInitJson mapInitJson, string geoServerUrl, string networkCatchmentLayerName) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            MapInitJson = mapInitJson;
            GeoServerUrl = geoServerUrl;
            NetworkCatchmentLayerName = networkCatchmentLayerName;
            EntityName = "Network Catchments";
            PageTitle = "All Network Catchments";

            
        }
    }
}
