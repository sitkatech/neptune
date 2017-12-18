using Newtonsoft.Json;

namespace Neptune.Web.Views.Shared
{
    public class GoogleChartErrorBars
    {
        [JsonProperty(PropertyName = "errorType")]
        public string ErrorType { get; set; }
        [JsonProperty(PropertyName = "magnitude")]
        public int Magnitude { get; set; }
    }
}