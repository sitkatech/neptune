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
        [HttpGet]
        [DroolToolViewFeature]
        public JsonResult Metrics(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;

            var databaseEntitiesVDroolMetrics = HttpRequestStorage.DatabaseEntities.vDroolMetrics.Where(x=>x.OCSurveyCatchmentID == networkCatchment.OCSurveyCatchmentID).ToList();
            var vDroolMetric = databaseEntitiesVDroolMetrics.OrderByDescending(x=>x.MetricDate).FirstOrDefault(x=>x.NumberOfReshoaAccounts != null)?.ToDroolMetricSimple();

            return Json(vDroolMetric, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [DroolToolViewFeature]
        public JsonResult MetricsForMonth(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey, int year, int month)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;

            var vDroolMetric = HttpRequestStorage.DatabaseEntities.vDroolMetrics.SingleOrDefault(x =>
                x.OCSurveyCatchmentID == networkCatchment.OCSurveyCatchmentID && x.MetricYear == year &&
                x.MetricMonth == month).ToDroolMetricSimple();

            return Json(vDroolMetric, JsonRequestBehavior.AllowGet);
        }
    }
}
