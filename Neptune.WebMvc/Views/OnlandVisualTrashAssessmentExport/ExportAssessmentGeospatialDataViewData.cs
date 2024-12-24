using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessmentExport
{
    public class ExportAssessmentGeospatialDataViewData : TrashModuleViewData
    {
        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }
        public string MapServiceUrl { get; }
        public ILookup<int, EFModels.Entities.OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreas { get; }
        public ILookup<int, EFModels.Entities.OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; }

        public ExportAssessmentGeospatialDataViewData(HttpContext httpContext, LinkGenerator linkGenerator,
            Person currentPerson, WebConfiguration webConfiguration, EFModels.Entities.NeptunePage neptunePage,
            List<StormwaterJurisdiction> stormwaterJurisdictions, string mapServiceUrl,
            ILookup<int, EFModels.Entities.OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas,
            ILookup<int, EFModels.Entities.OnlandVisualTrashAssessment> onlandVisualTrashAssessments) : base(httpContext, linkGenerator, currentPerson, webConfiguration, neptunePage)
        {
            MapServiceUrl = mapServiceUrl;
            OnlandVisualTrashAssessmentAreas = onlandVisualTrashAssessmentAreas;
            OnlandVisualTrashAssessments = onlandVisualTrashAssessments;
            EntityName = "On-land Visual Trash Assessment";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = "Export Geospatial Data";
            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);
        }
    }
}
