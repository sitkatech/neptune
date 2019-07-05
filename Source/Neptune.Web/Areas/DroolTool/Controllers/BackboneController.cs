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

            var traceBackboneDownstream = networkCatchment.TraceBackboneDownstream();

            var jobject = JObject.FromObject(traceBackboneDownstream).ToString(Formatting.None);

            return Content(jobject);
        }

        [HttpGet]
        [DroolToolViewFeature]
        public ContentResult StormshedBackboneFeatureCollection(NetworkCatchmentPrimaryKey networkCatchmentPrimaryKey)
        {
            var networkCatchment = networkCatchmentPrimaryKey.EntityObject;

            var traceBackboneDownstream = networkCatchment.GetStormshedViaBackboneTrace();
            
            var jobject = JObject.FromObject(traceBackboneDownstream).ToString(Formatting.None);

            return Content(jobject);
        }
        
    }
}
