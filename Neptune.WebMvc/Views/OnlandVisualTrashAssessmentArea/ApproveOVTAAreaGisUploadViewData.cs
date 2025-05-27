using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessmentArea
{
    public class ApproveOVTAAreaGisUploadViewData : NeptuneViewData
    {
        public OVTAAreaUploadGisReportJsonResult OvtaAreaUploadGisReportFromStaging { get; }
        public string OVTAIndexUrl { get; }

        public ApproveOVTAAreaGisUploadViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, OVTAAreaUploadGisReportJsonResult ovtaAreaUploadGisReportFromStaging) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            OvtaAreaUploadGisReportFromStaging = ovtaAreaUploadGisReportFromStaging;
            OVTAIndexUrl = SitkaRoute<DataHubController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
        }
    }
}