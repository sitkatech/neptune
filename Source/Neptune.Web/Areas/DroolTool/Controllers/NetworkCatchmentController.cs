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
        public JsonResult Metrics(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey, int year, int month)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;

            var vDroolMetric = HttpRequestStorage.DatabaseEntities.vDroolMetrics.SingleOrDefault(x=>x.OCSurveyCatchmentID == networkCatchment.OCSurveyCatchmentID && x.Year == year && x.Month == month);

            //todo: gregs

            return Json(vDroolMetric, JsonRequestBehavior.AllowGet);
        }
    }
}
