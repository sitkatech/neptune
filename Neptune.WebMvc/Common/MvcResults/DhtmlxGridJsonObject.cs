using System.Text.Json.Serialization;

namespace Neptune.WebMvc.Common.MvcResults;

public class DhtmlxGridJsonObject
{
    [JsonPropertyName("rows")] public List<DhtmlxGridJsonRow> Rows { get; set; }
    public DhtmlxGridJsonObject(List<DhtmlxGridJsonRow> rows)
    {
        Rows = rows;
    }

    public DhtmlxGridJsonObject()
    {
    }
}