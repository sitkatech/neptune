using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.NetworkCatchment
{
    public class IndexViewData : NeptuneViewData
    {
        public MapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }
        public string NetworkCatchmentLayerName { get; }
        public bool HasAdminPermissions { get; }
        public string RefreshUrl { get; }

        public IndexViewData(Person currentPerson, MapInitJson mapInitJson, string geoServerUrl, string networkCatchmentLayerName) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            MapInitJson = mapInitJson;
            GeoServerUrl = geoServerUrl;
            NetworkCatchmentLayerName = networkCatchmentLayerName;
            EntityName = "Network Catchments";
            PageTitle = "All Network Catchments";

            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            RefreshUrl = SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(j => j.RefreshFromOCSurvey());
        }
    }
}
