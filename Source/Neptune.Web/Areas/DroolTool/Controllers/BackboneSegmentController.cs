using System.Collections.Generic;
using System.Web.Mvc;
using Neptune.Web.Areas.DroolTool.Security;
using Neptune.Web.Areas.DroolTool.Views.Home;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Areas.DroolTool.Controllers
{
    public class BackboneController : NeptuneBaseController
    {
        [HttpGet]
        [DroolToolViewFeature]
        public JsonResult NameMe(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;

            List<BackboneSegment> traceBackbizzleDownstrizzle = networkCatchment.TraceBackbizzleDownstrizzle();

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}
