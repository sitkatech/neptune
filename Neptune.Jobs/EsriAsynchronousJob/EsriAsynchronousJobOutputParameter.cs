using System.Text.Json.Serialization;

namespace Neptune.Jobs.EsriAsynchronousJob;

public class EsriAsynchronousJobOutputParameter<T>
{
    //Not from the service itself, but as a means to hold onto the HRULogID for later.
    //Doesn't need to be logged or included in serialization for now
    [JsonIgnore]
    public int HRULogID { get; set; }
    public string ResultURI { get; set; }

    [JsonPropertyName("paramName")]
    public string ParameterName { get; set; }
    [JsonPropertyName("dataType")]
    public string DataType { get; set; }
    [JsonPropertyName("value")]
    public T Value { get; set; }
}