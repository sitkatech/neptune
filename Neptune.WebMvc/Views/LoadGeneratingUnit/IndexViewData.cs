using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.Delineation;

namespace Neptune.WebMvc.Views.LoadGeneratingUnit
{
    public class IndexViewData : NeptuneViewData
    {
        public LoadGeneratingUnitGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public bool HasAdminPermissions { get; }
        public StormwaterMapInitJson MapInitJson { get; }
        public string GeoServerUrl { get; }
        public string JurisdictionCQLFilter { get; }
        public DelineationMapConfig DelineationMapConfig { get; set; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, 
            EFModels.Entities.NeptunePage neptunePage, StormwaterMapInitJson mapInitJson, string geoServerUrl, string stormwaterJurisdictionCqlFilter) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            
            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            EntityName = "Load Generating Unit";
            PageTitle = "Index";
            GridSpec = new LoadGeneratingUnitGridSpec(linkGenerator) { ObjectNameSingular = "Load Generating Unit", ObjectNamePlural = "Load Generating Units", SaveFiltersInCookie = true };
            GridName = "LoadGeneratingUnits";
            GridDataUrl = SitkaRoute<LoadGeneratingUnitController>.BuildUrlFromExpression(linkGenerator, x => x.LoadGeneratingUnitGridJsonData());

            MapInitJson = mapInitJson;
            GeoServerUrl = geoServerUrl;
            JurisdictionCQLFilter = stormwaterJurisdictionCqlFilter;
        }
    }
}