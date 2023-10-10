using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Neptune.API.Models.EsriAsynchronousJob
{
    public class EsriJobStatusResponse
    {
        public string jobId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EsriJobStatus jobStatus { get; set; }

        public List<EsriJobMessage> messages { get; set; }
    }
}