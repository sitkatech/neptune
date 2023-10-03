using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.RegionalSubbasinRevisionRequest
{
    public class IndexViewData : NeptuneViewData
    {
        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, RegionalSubbasinRevisionRequestGridSpec gridSpec) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = "Regional Subbasin Revision Requests";
            PageTitle = "Index";
            GridSpec = gridSpec;
            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            GridName = "rsbRevisionRequestsGrid";
            GridDataUrl = SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(linkGenerator, x => x.IndexGridJsonData());
            RefreshUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(linkGenerator, x => x.RefreshFromOCSurvey());
        }

        public bool HasAdminPermissions { get; }
        public RegionalSubbasinRevisionRequestGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get;  }
        public string RefreshUrl { get; set; }
    }
}