using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.RegionalSubbasinRevisionRequest
{
    public class IndexViewData : NeptuneViewData
    {
        public IndexViewData(Person currentPerson, RegionalSubbasinRevisionRequestGridSpec gridSpec) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {

            EntityName = "Regional Subbasin Revision Requests";
            PageTitle = "Index";
            GridSpec = gridSpec;
            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            GridName = "rsbRevisionRequestsGrid";
            GridDataUrl =
                SitkaRoute<RegionalSubbasinRevisionRequestController>.BuildUrlFromExpression(x =>
                    x.RegionalSubbasinRevisionRequestGridJsonData());
            RefreshUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(j => j.RefreshFromOCSurvey());
        }

        public bool HasAdminPermissions { get; }
        public RegionalSubbasinRevisionRequestGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get;  }
        public string RefreshUrl { get; set; }
    }
}