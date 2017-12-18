using System.Collections.Generic;
using Newtonsoft.Json;

namespace Neptune.Web.Views.Shared
{
    public class GoogleChartRowC
    {
        [JsonProperty(PropertyName = "c")]
        public List<GoogleChartRowV> GoogleChartRowVs { get; set; }

        /// <summary>
        /// Needed to deserialize json
        /// </summary>
        public GoogleChartRowC()
        {
        }

        public GoogleChartRowC(List<GoogleChartRowV> googleChartRowVs)
        {
            GoogleChartRowVs = googleChartRowVs;
        }
    }
}