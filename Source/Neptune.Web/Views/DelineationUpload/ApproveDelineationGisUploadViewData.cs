using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.DelineationUpload
{
    public class ApproveDelineationGisUploadViewData : NeptuneViewData
    {
        public DelineationUploadGisReportJsonResult DelineationUpoadGisReportFromStaging { get; }
        public string DelineationMapUrl { get; }

        public ApproveDelineationGisUploadViewData(Person currentPerson,
            DelineationUploadGisReportJsonResult delineationUpoadGisReportFromStaging) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            DelineationUpoadGisReportFromStaging = delineationUpoadGisReportFromStaging;
            DelineationMapUrl = SitkaRoute<DelineationController>.BuildUrlFromExpression(x=>x.DelineationMap(null));
        }
    }
}