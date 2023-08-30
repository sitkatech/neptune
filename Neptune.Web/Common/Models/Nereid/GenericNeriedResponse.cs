using System.Text.Json.Serialization;

namespace Neptune.Web.Common.Models.Nereid;

public class GenericNeriedResponse
{
    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; }

    [JsonPropertyName("warnings")]
    public List<string> Warnings { get; set; }
}