// Classes for preparing the Esri JSON input to the HRU service.

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class EsriGPRecordSetLayer<T>
    {

        [JsonProperty("geometryType")]
        public string GeometryType { get; set; }
        [JsonProperty("exceededTransferLimit")]
        public string ExceededTransferLimit { get; set; }
        [JsonProperty("fields")]
        public List<EsriField> Fields { get; set; }
        [JsonProperty("spatialReference")]
        public EsriSpatialReference SpatialReference { get; set; }
        [JsonProperty("features")]
        public List<T> Features { get; set; }
    }
}
