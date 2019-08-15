using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
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

            var networkCatchments = HttpRequestStorage.DatabaseEntities.NetworkCatchments
                .Where(x => networkCatchmentIDs.Contains(x.NetworkCatchmentID)).Select(x => x.CatchmentGeometry)
                .ToList().UnionListGeometries().ExteriorRing.AsText().Replace("LINESTRING", "POLYGON").Replace("(", "((").Replace(")", "))");

            var dbGeometry = DbGeometry.FromText(networkCatchments, 4326);

            var featureCollection = new FeatureCollection();
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometryWithReprojectionChec(dbGeometry);
            featureCollection.Features.Add(feature);

            return Content(JObject.FromObject(feature).ToString(Formatting.None));
        }
    }

    public class NetworkCatchmentMapInitJson : MapInitJson
    {
        public NetworkCatchmentMapInitJson(string mapDivID) : base(mapDivID, DefaultZoomLevel, new List<LayerGeoJson>(), BoundingBox.MakeNewDefaultBoundingBox())
        {
        }
    }
}