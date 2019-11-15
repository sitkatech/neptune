using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.NetworkCatchment
{
    public class GridViewData : NeptuneViewData
    {
        public NetworkCatchmentGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public bool HasAdminPermissions{ get; }
        public string RefreshUrl { get; }

        public GridViewData(Person currentPerson) : base(currentPerson, Models.NeptunePage.GetNeptunePageByPageType(NeptunePageType.NetworkCatchments), NeptuneArea.OCStormwaterTools)

        {
            EntityName = "Network Catchments";
            PageTitle = "Grid";
            GridSpec = new NetworkCatchmentGridSpec() { ObjectNameSingular = "Network Catchment", ObjectNamePlural = "Network Catchments", SaveFiltersInCookie = true };
            GridName = "absoluteUnitsGrid";
            GridDataUrl = SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(j => j.NetworkCatchmentGridJsonData());

            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            RefreshUrl = SitkaRoute<NetworkCatchmentController>.BuildUrlFromExpression(j => j.RefreshFromOCSurvey());
        }
    }
}