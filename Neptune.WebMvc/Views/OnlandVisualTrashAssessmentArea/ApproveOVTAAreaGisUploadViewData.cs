using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessmentArea
{
    public class ApproveOVTAAreaGisUploadViewData : NeptuneViewData
    {
        //public ApproveOVTAAreaGisUploadReportJsonResult DelineationUpoadGisReportFromStaging { get; }
        public string DelineationMapUrl { get; }

        public ApproveOVTAAreaGisUploadViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, DelineationUploadGisReportJsonResult delineationUpoadGisReportFromStaging) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            //DelineationUpoadGisReportFromStaging = delineationUpoadGisReportFromStaging;
            DelineationMapUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.DelineationMap(null));
        }
    }
}