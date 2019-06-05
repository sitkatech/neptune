using Neptune.Web.Areas.DroolTool.Security;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Neptune.Web.Areas.DroolTool.Controllers
{
    public class BackboneController : NeptuneBaseController
    {
        [HttpGet]
        [DroolToolViewFeature]
        public ContentResult DownstreamBackboneFeatureCollection(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;

            var traceBackbizzleDownstrizzle = networkCatchment.TraceBackbizzleDownstrizzle();

            var jobject = JObject.FromObject(traceBackbizzleDownstrizzle).ToString(Formatting.None);

            return Content(jobject);
        }
    }
}
