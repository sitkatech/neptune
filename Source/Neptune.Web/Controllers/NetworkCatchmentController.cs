using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using Hangfire;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using LtInfo.Common.MvcResults;
using Neptune.Web.Areas.Modeling.Views.HRUCharacteristic;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Views;
using Neptune.Web.Views.NetworkCatchment;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Index = Neptune.Web.Views.NetworkCatchment.Index;
using IndexViewData = Neptune.Web.Views.NetworkCatchment.IndexViewData;

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
        [NeptuneAdminFeature]
        public ActionResult RefreshFromOCSurvey()
        {
            //return Content(NetworkCatchmentRefreshScheduledBackgroundJob.RunRefresh(HttpRequestStorage.DatabaseEntities));

            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunNetworkCatchmentRefreshBackgroundJob(CurrentPerson.PersonID));

            SetMessageForDisplay("Network Catchment refresh will run in the background.");
            return Redirect(SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(x => x.Grid()));
        }


        [NeptuneAdminFeature]
        public ViewResult Grid()
        {
            var viewData = new Views.NetworkCatchment.GridViewData(CurrentPerson);
            return RazorView<Views.NetworkCatchment.Grid, Views.NetworkCatchment.GridViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<NetworkCatchment> NetworkCatchmentGridJsonData()
        {
            // ReSharper disable once InconsistentNaming
            List<NetworkCatchment> networkCatchments = GetNetworkCatchmentsAndGridSpec(out var gridSpec);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<NetworkCatchment>(networkCatchments, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<NetworkCatchment> GetNetworkCatchmentsAndGridSpec(out NetworkCatchmentGridSpec gridSpec)
        {
            gridSpec = new NetworkCatchmentGridSpec();

            return HttpRequestStorage.DatabaseEntities.NetworkCatchments.ToList();
        }
    }
}
