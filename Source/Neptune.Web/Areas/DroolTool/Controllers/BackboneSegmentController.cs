using Neptune.Web.Areas.DroolTool.Security;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Neptune.Web.Areas.DroolTool.Controllers
{
    public class BackboneController : NeptuneBaseController
    {
        [HttpGet]
        [DroolToolViewFeature]
        public JsonResult DownstreamBackboneFeatureCollection(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;

            var traceBackbizzleDownstrizzle = networkCatchment.TraceBackbizzleDownstrizzle();

            return Json(traceBackbizzleDownstrizzle, JsonRequestBehavior.AllowGet);
        }
    }
}
