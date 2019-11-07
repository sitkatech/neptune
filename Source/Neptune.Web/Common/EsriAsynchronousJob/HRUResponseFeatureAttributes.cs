using Newtonsoft.Json;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class HRUResponseFeatureAttributes
    {
        [JsonProperty("OBJECTID")]
        public int ObjectID { get; set; }
        [JsonProperty("QueryFeatureID")]
        public int QueryFeatureID { get; set; }
        [JsonProperty("HRU_Composite_LSPC_LU_DESC")]
        public string LSPCLandUseDescription { get; set; }
        [JsonProperty("HRU_Composite_soil_hsg")]
        public string HydrologicSoilGroup { get; set; }
        [JsonProperty("HRU_Composite_slope_pct")]
        public int SlopePercentage { get; set; }
        [JsonProperty("SUM_imp_acres")]
        public double ImperviousAcres { get; set; }
        [JsonProperty("Shape_Length")]
        public double Length { get; set; }
        [JsonProperty("Shape_Area")]
        public double Area { get; set; }
    }
}