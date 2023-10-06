using System.Text.Json.Serialization;

namespace Neptune.EFModels.Nereid;

public class TreatmentFacilityTable
{
    [JsonPropertyName("treatment_facilities")]
    public List<TreatmentFacility> TreatmentFacilities { get; set; }
}