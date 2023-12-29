using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.TrashGeneratingUnit
{
    public class IndexViewData : TrashModuleViewData
    {
        public TrashGeneratingUnitGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration) : base (httpContext, linkGenerator, currentPerson, webConfiguration)
        {
            EntityName = "Trash Generating Unit";
            PageTitle = "Index";
            GridSpec = new TrashGeneratingUnitGridSpec(linkGenerator) { ObjectNameSingular = "Trash Generating Unit", ObjectNamePlural = "Trash Generating Units", SaveFiltersInCookie = true };
            GridName = "absoluteUnitsGrid";
            GridDataUrl = SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(linkGenerator, x => x.TrashGeneratingUnitGridJsonData());
        }
    }
}