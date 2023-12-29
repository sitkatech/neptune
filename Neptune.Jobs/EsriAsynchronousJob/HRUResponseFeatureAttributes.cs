using System.Text.Json.Serialization;

namespace Neptune.Jobs.EsriAsynchronousJob
{
    public class HRUResponseFeatureAttributes
    {
        [JsonPropertyName("OBJECTID")]
        public int ObjectID { get; set; }
        [JsonPropertyName("QueryFeatureID")]
        public int QueryFeatureID { get; set; }
        [JsonPropertyName("LSPC_LU_EDIT")]
        public string ModelBasinLandUseDescription { get; set; }
        [JsonPropertyName("LU_2002")]
        public string BaselineLandUseDescription { get; set; }
        [JsonPropertyName("soil_hsg")]
        public string HydrologicSoilGroup { get; set; }
        [JsonPropertyName("slope_pct")]
        public int? SlopePercentage { get; set; }
        [JsonPropertyName("imp_acres")]
        public double? ImperviousAcres { get; set; }
        [JsonPropertyName("imp_acres_02")]
        public double? BaselineImperviousAcres { get; set; }
        [JsonPropertyName("Shape_Length")]
        public double? Length { get; set; }
        [JsonPropertyName("acres")]
        public double? Acres { get; set; }
    }
}