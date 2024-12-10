using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.DelineationGeometry
{
    public class ApproveDelineationGisUploadViewData : NeptuneViewData
    {
        public DelineationUploadGisReportJsonResult DelineationUpoadGisReportFromStaging { get; }
        public string DelineationMapUrl { get; }

        public ApproveDelineationGisUploadViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, DelineationUploadGisReportJsonResult delineationUpoadGisReportFromStaging) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            DelineationUpoadGisReportFromStaging = delineationUpoadGisReportFromStaging;
            DelineationMapUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.DelineationMap(null));
        }
    }
}