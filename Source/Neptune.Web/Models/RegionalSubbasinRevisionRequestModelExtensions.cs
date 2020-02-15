using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class RegionalSubbasinRevisionRequestModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(
            SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildAbsoluteUrlHttpsFromExpression(
                t => t.Detail(UrlTemplate.Parameter1Int), NeptuneWebConfiguration.CanonicalHostNameRoot));

        public static string GetDetailUrl(this RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest)
        {
            return DetailUrlTemplate.ParameterReplace(regionalSubbasinRevisionRequest.TreatmentBMPID);
        }
    }
}