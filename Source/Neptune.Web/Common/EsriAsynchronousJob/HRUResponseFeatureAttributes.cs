using Newtonsoft.Json;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class HRUResponseFeatureAttributes
    {
        [JsonProperty("OBJECTID")]
        public int ObjectID { get; set; }
        [JsonProperty("QueryFeatureID")]
        public int QueryFeatureID { get; set; }
        [JsonProperty("LSPC_LU_EDIT")]
        public string LSPCLandUseDescription { get; set; }
        [JsonProperty("LU_2002")]
        public string BaselineLandUseDescription { get; set; }
        [JsonProperty("soil_hsg")]
        public string HydrologicSoilGroup { get; set; }
        [JsonProperty("slope_pct")]
        public int SlopePercentage { get; set; }
        [JsonProperty("imp_acres")]
        public double ImperviousAcres { get; set; }
        [JsonProperty("imp_acres_02")]
        public double BaselineImperviousAcres { get; set; }
        [JsonProperty("Shape_Length")]
        public double Length { get; set; }
        [JsonProperty("Shape_Area")]
        public double Area { get; set; }
    }
}