using Neptune.Web.Areas.DroolTool.Security;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace Neptune.Web.Areas.DroolTool.Controllers
{
    public class NetworkCatchmentController : NeptuneBaseController
    {

        // TODO: This method doesn't actually use the year/month parameters, it just returns the most recent available datum
        [HttpGet]
        [DroolToolViewFeature]
        public JsonResult Metrics(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;

            var vDroolMetric = HttpRequestStorage.DatabaseEntities.vDroolMetrics.OrderByDescending(x=>x.MetricDate).First(x=>x.OCSurveyCatchmentID == networkCatchment.OCSurveyCatchmentID).ToDroolMetricSimple();

            return Json(vDroolMetric, JsonRequestBehavior.AllowGet);
        }
    }
}
