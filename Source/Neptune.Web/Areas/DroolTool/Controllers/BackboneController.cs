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
        public ContentResult DownstreamBackboneFeatureCollection(NeighborhoodPrimaryKey neighborhoodPrimaryKey)
        {
            var neighborhood = neighborhoodPrimaryKey.EntityObject;

            var traceBackboneDownstream = neighborhood.TraceBackboneDownstream();

            var jobject = JObject.FromObject(traceBackboneDownstream).ToString(Formatting.None);

            return Content(jobject);
        }

        [HttpGet]
        [DroolToolViewFeature]
        public ContentResult StormshedBackboneFeatureCollection(NeighborhoodPrimaryKey neighborhoodPrimaryKey)
        {
            var neighborhood = neighborhoodPrimaryKey.EntityObject;

            var traceBackboneDownstream = neighborhood.GetStormshedViaBackboneTrace();
            
            var jobject = JObject.FromObject(traceBackboneDownstream).ToString(Formatting.None);

            return Content(jobject);
        }
        
    }
}
