using Newtonsoft.Json;

namespace Neptune.Web.Views.Shared
{
    public class GoogleChartAnnotations
    {
        [JsonProperty(PropertyName = "style")]
        public string Style { get; set; }
        public GoogleChartAnnotations()
        {
            Style = "line";
        }
    }
}