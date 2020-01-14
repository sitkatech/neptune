using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.RegionalSubbasin
{
    public class GridViewData : NeptuneViewData
    {
        public RegionalSubbasinGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public bool HasAdminPermissions{ get; }
        public string RefreshUrl { get; }

        public GridViewData(Person currentPerson) : base(currentPerson, Models.NeptunePage.GetNeptunePageByPageType(NeptunePageType.RegionalSubbasins), NeptuneArea.OCStormwaterTools)

        {
            EntityName = "Regional Subbasins";
            PageTitle = "Grid";
            GridSpec = new RegionalSubbasinGridSpec() { ObjectNameSingular = "Regional Subbasin", ObjectNamePlural = "Regional Subbasins", SaveFiltersInCookie = true };
            GridName = "absoluteUnitsGrid";
            GridDataUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(j => j.RegionalSubbasinGridJsonData());

            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            RefreshUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(j => j.RefreshFromOCSurvey());
        }
    }
}