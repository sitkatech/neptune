using Newtonsoft.Json;

namespace Neptune.Web.Views.Shared
{
    public class GoogleChartViewWindow
    {
        [JsonProperty(PropertyName = "min", NullValueHandling = NullValueHandling.Ignore)]
        public int MinValue { get; set; }
    }
}