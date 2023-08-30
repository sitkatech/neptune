using System.Text.Json.Serialization;

namespace Neptune.Web.Common.Models.Nereid;

public class TreatmentFacilityTable
{
    [JsonPropertyName("treatment_facilities")]
    public List<TreatmentFacility> TreatmentFacilities { get; set; }
}