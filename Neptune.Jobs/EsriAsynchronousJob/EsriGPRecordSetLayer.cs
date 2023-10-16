// Classes for preparing the Esri JSON input to the HRU service.

using System.Text.Json.Serialization;

namespace Neptune.Jobs.EsriAsynchronousJob
{
    public class EsriGPRecordSetLayer<T>
    {

        [JsonPropertyName("geometryType")]
        public string GeometryType { get; set; }
        [JsonPropertyName("exceededTransferLimit")]
        public string ExceededTransferLimit { get; set; }
        [JsonPropertyName("fields")]
        public List<EsriField> Fields { get; set; }
        [JsonPropertyName("spatialReference")]
        public EsriSpatialReference SpatialReference { get; set; }
        [JsonPropertyName("features")]
        public List<T> Features { get; set; }
        [JsonPropertyName("displayFieldName")]
        public string DisplayFieldName { get; set; }
    }
}
