using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.Shared.ModeledPerformance
{
    public class ModeledPerformanceViewData : NeptuneUserControlViewData
    {
        public string AboutModelingBMPPerformanceURL { get; }
        public string ModelingResultsUrl { get; }
        public string InflowLabel { get; }
        public bool IsSitkaAdmin { get; }
        public string? NereidRequest { get; }
        public string? NereidResponse { get; }

        public ModeledPerformanceViewData(LinkGenerator linkGenerator, string modelingResultsUrl, string inflowLabel,
            bool isSitkaAdmin, string? nereidRequest, string? nereidResponse)
        {
            InflowLabel = inflowLabel;
            IsSitkaAdmin = isSitkaAdmin;
            NereidRequest = nereidRequest;
            NereidResponse = nereidResponse;
            AboutModelingBMPPerformanceURL = SitkaRoute<HomeController>.BuildUrlFromExpression(linkGenerator, x => x.AboutModelingBMPPerformance());
            ModelingResultsUrl = modelingResultsUrl;
        }
    }
}