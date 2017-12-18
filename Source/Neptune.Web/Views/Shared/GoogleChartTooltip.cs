using Newtonsoft.Json;

namespace Neptune.Web.Views.Shared
{
    public class GoogleChartTooltip
    {
        [JsonProperty(PropertyName = "isHtml")]
        public bool IsHtml { get; set; }

        public GoogleChartTooltip()
        {
        }

        public GoogleChartTooltip(bool isHtml)
        {
            IsHtml = isHtml;
        }
    }
}