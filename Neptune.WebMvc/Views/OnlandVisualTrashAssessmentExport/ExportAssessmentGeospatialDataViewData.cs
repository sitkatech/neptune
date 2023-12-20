using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessmentExport
{
    public class ExportAssessmentGeospatialDataViewData : TrashModuleViewData
    {
        public List<StormwaterJurisdiction> StormwaterJurisdictions { get; }
        public string MapServiceUrl { get; }

        public ExportAssessmentGeospatialDataViewData(HttpContext httpContext, LinkGenerator linkGenerator,
            Person currentPerson, WebConfiguration webConfiguration, EFModels.Entities.NeptunePage neptunePage,
            List<StormwaterJurisdiction> stormwaterJurisdictions, string mapServiceUrl) : base(httpContext, linkGenerator, currentPerson, webConfiguration, neptunePage)
        {
            MapServiceUrl = mapServiceUrl;
            EntityName = "On-land Visual Trash Assessment";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = "Export Geospatial Data";
            StormwaterJurisdictions = stormwaterJurisdictions.ToList().OrderBy(x => x.GetOrganizationDisplayName()).ToList();
        }
    }
}
