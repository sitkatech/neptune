using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using LtInfo.Common.Mvc;
using Neptune.Web.Controllers;
using Neptune.Web.Views;
using Neptune.Web.Views.NetworkCatchment;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Neptune.Web.Controllers
{
    public class NetworkCatchmentController : NeptuneBaseController
    {
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
        [SitkaAdminFeature]
        public ActionResult RefreshHRUCharacteristics(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;
            HRUHelper.RetrieveAndSaveHRUCharacteristics(networkCatchment);
            return Redirect(
                SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(x => x.Detail(networkCatchmentPrimaryKey)));
        }
        [HttpGet]
        [SitkaAdminFeature]
        public ViewResult Detail(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            return RazorView<Detail, DetailViewData>(new DetailViewData(CurrentPerson, networkCatchmentPrimaryKey.EntityObject));
        }
    }

    public class NetworkCatchmentMapInitJson : MapInitJson
    {
        public NetworkCatchmentMapInitJson(string mapDivID) : base(mapDivID, DefaultZoomLevel, new List<LayerGeoJson>(), BoundingBox.MakeNewDefaultBoundingBox())
        {
        }
    }
}

namespace Neptune.Web.Views.NetworkCatchment
{

    public class DetailViewData : NeptuneViewData
    {
        public Models.NetworkCatchment NetworkCatchment { get; }
        public string HRURefreshUrl { get; }

        public DetailViewData(Person currentPerson, Models.NetworkCatchment networkCatchment) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            EntityName = "Network Catchment";
            PageTitle = $"{networkCatchment.Watershed} - {networkCatchment.DrainID}";

            NetworkCatchment = networkCatchment;
            HRURefreshUrl =
                SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(x =>
                    x.RefreshHRUCharacteristics(NetworkCatchment));
        }
    }

    public abstract class Detail : TypedWebViewPage<DetailViewData>
    {
    }

}