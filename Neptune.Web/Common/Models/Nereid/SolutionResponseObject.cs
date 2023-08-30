using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Neptune.Web.Areas.Modeling.Models.Nereid;

namespace Neptune.Web.Common.Models.Nereid;

public class SolutionResponseObject : GenericNeriedResponse
{
    [JsonPropertyName("results")]
    public List<JsonObject> Results { get; set; }

    [JsonPropertyName("leaf_results")]
    public List<JsonObject> LeafResults { get; set; }

    [JsonPropertyName("previous_results_keys")]
    public List<string> PreviousResultsKeys { get; set; }


}