using System.Text.Json.Serialization;

namespace Neptune.Web.Common.Models.Nereid;

public class TreatmentSiteTable
{
    [JsonPropertyName("treatment_sites")]
    public List<TreatmentSite> TreatmentSites { get; set; }
}