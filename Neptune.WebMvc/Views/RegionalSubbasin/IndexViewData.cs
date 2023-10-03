using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.RegionalSubbasin
{
    public class IndexViewData : NeptuneViewData
    {
        public MapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }
        public string RegionalSubbasinLayerName { get; }
        public bool HasAdminPermissions { get; }
        public string RefreshUrl { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, MapInitJson mapInitJson, string geoServerUrl, string regionalSubbasinLayerName) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            MapInitJson = mapInitJson;
            GeoServerUrl = geoServerUrl;
            RegionalSubbasinLayerName = regionalSubbasinLayerName;
            EntityName = "Regional Subbasins";
            PageTitle = "All Regional Subbasins";

            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            RefreshUrl = SitkaRoute<RegionalSubbasinController>.BuildUrlFromExpression(LinkGenerator, x => x.RefreshFromOCSurvey());
        }
    }
}
