using System.Collections.Generic;
using Newtonsoft.Json;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class EsriPolygonGeometry
    {
        [JsonProperty("rings")]
        public List<List<double[]>> Rings { get; set; }
    }
}