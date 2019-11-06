using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class EsriJobStatusResponse
    {
        public string jobId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EsriJobStatus jobStatus { get; set; }

        public List<EsriJobMessage> messages { get; set; }
    }
}