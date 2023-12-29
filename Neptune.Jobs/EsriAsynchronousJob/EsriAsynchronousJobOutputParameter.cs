using System.Text.Json.Serialization;

namespace Neptune.Jobs.EsriAsynchronousJob;

public class EsriAsynchronousJobOutputParameter<T>
{
    [JsonPropertyName("paramName")]
    public string ParameterName { get; set; }
    [JsonPropertyName("dataType")]
    public string DataType { get; set; }
    [JsonPropertyName("value")]
    public T Value { get; set; }
}