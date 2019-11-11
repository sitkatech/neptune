using Neptune.Web.Areas.DroolTool.Security;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace Neptune.Web.Areas.DroolTool.Controllers
{
    public class NeighborhoodController : NeptuneBaseController
    {
        [HttpGet]
        [DroolToolViewFeature]
        public JsonResult Metrics(NeighborhoodPrimaryKey neighborhoodPrimaryKey)
        {
            var neighborhood = neighborhoodPrimaryKey.EntityObject;

            var databaseEntitiesVDroolMetrics = HttpRequestStorage.DatabaseEntities.vDroolMetrics.Where(x=>x.OCSurveyCatchmentID == neighborhood.OCSurveyNeighborhoodID).ToList();
            var vDroolMetric = databaseEntitiesVDroolMetrics.OrderByDescending(x=>x.MetricDate).FirstOrDefault(x=>x.NumberOfReshoaAccounts != null)?.ToDroolMetricSimple();

            return Json(vDroolMetric, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [DroolToolViewFeature]
        public JsonResult MetricsForMonth(NeighborhoodPrimaryKey neighborhoodPrimaryKey, int year, int month)
        {
            var neighborhood = neighborhoodPrimaryKey.EntityObject;

            var vDroolMetric = HttpRequestStorage.DatabaseEntities.vDroolMetrics.SingleOrDefault(x =>
                x.OCSurveyCatchmentID == neighborhood.OCSurveyNeighborhoodID && x.MetricYear == year &&
                x.MetricMonth == month).ToDroolMetricSimple();

            return Json(vDroolMetric, JsonRequestBehavior.AllowGet);
        }
    }
}
