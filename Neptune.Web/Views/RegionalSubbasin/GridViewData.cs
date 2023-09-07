using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
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

        public GridViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, NeptunePage neptunePage) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)

        {
            EntityName = "Regional Subbasins";
            PageTitle = "Grid";
            GridSpec = new RegionalSubbasinGridSpec(LinkGenerator) { ObjectNameSingular = "Regional Subbasin", ObjectNamePlural = "Regional Subbasins", SaveFiltersInCookie = true };
            GridName = "absoluteUnitsGrid";
            GridDataUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator, x => x.RegionalSubbasinGridJsonData());

            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            RefreshUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator, x => x.RefreshFromOCSurvey());
        }
    }
}