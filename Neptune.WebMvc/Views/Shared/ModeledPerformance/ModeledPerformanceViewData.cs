using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.Shared.ModeledPerformance
{
    public class ModeledPerformanceViewData : NeptuneUserControlViewData
    {
        public string AboutModelingBMPPerformanceURL { get; }
        public string ModelingResultsUrl { get; }
        public string InflowLabel { get; }

        public ModeledPerformanceViewData(LinkGenerator linkGenerator, string modelingResultsUrl, string inflowLabel)
        {
            InflowLabel = inflowLabel;
            AboutModelingBMPPerformanceURL = SitkaRoute<HomeController>.BuildUrlFromExpression(linkGenerator, x => x.AboutModelingBMPPerformance());
            ModelingResultsUrl = modelingResultsUrl;
        }
    }
}