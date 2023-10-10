using System.Text.Json.Serialization;

namespace Neptune.API.Hangfire;

public class EsriQueryResponse
{
    [JsonPropertyName("exceededTransferLimit")]
    public bool ExceededTransferLimit { get; set; }
}