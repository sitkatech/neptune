using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Views;
using Neptune.Web.Views.NetworkCatchment;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Neptune.Web.Controllers
{
    public class NetworkCatchmentController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var geoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;
            var networkCatchmentLayerName = NeptuneWebConfiguration.NetworkCatchmentLayerName;

            var viewData = new IndexViewData(CurrentPerson, new NetworkCatchmentMapInitJson("networkCatchmentMap"), geoServerUrl, networkCatchmentLayerName);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public JsonResult UpstreamCatchments(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;
            return Json(new {networkCatchmentIDs = networkCatchment.TraceUpstreamCatchmentsReturnIDList()}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public ContentResult UpstreamDelineation(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;
            var networkCatchmentIDs = networkCatchment.TraceUpstreamCatchmentsReturnIDList();

            networkCatchmentIDs.Add(networkCatchment.NetworkCatchmentID);

            var unionOfUpstreamNetworkCatchments = HttpRequestStorage.DatabaseEntities.NetworkCatchments
                .Where(x => networkCatchmentIDs.Contains(x.NetworkCatchmentID)).Select(x => x.CatchmentGeometry)
                .ToList().UnionListGeometries();

            var asText = unionOfUpstreamNetworkCatchments.AsText();
            var networkCatchments = unionOfUpstreamNetworkCatchments.ExteriorRing.AsText().Replace("LINESTRING", "POLYGON").Replace("(", "((").Replace(")", "))");

            var dbGeometry = DbGeometry.FromText(networkCatchments, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID);

            var featureCollection = new FeatureCollection();
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionChecc(dbGeometry);
            featureCollection.Features.Add(feature);

            return Content(JObject.FromObject(feature).ToString(Formatting.None));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ActionResult RefreshHRUCharacteristics(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;
            HRUHelper.RetrieveAndSaveHRUCharacteristics(networkCatchment);
            return Redirect(
                SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(x => x.Detail(networkCatchmentPrimaryKey)));
        }
        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Detail(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            return RazorView<Detail, DetailViewData>(new DetailViewData(CurrentPerson, networkCatchmentPrimaryKey.EntityObject));
        }

        [HttpGet]
        [SitkaAdminFeature]
        public ContentResult TestRefresh()
        {
            return Content(NetworkCatchmentRefreshScheduledBackgroundJob.TestRunJob(HttpRequestStorage.DatabaseEntities));
        }
    }
}
