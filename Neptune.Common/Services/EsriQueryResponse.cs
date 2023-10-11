using System.Text.Json.Serialization;

namespace Neptune.Common.Services;

public class EsriQueryResponse
{
    [JsonPropertyName("exceededTransferLimit")]
    public bool ExceededTransferLimit { get; set; }
}