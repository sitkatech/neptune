using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessmentExport
{
    public class ExportAssessmentGeospatialDataViewData : NeptuneViewData
    {
        public IEnumerable<SelectListItem> StormwaterJurisdictions { get; }
        public string MapServiceUrl { get; }
        public ILookup<int, EFModels.Entities.OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreas { get; }
        public ILookup<int, EFModels.Entities.OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; }
        public string UploadOVTAAreaUrl { get; }

        public ExportAssessmentGeospatialDataViewData(HttpContext httpContext, LinkGenerator linkGenerator,
            Person currentPerson, WebConfiguration webConfiguration, EFModels.Entities.NeptunePage neptunePage,
            List<StormwaterJurisdiction> stormwaterJurisdictions, string mapServiceUrl,
            ILookup<int, EFModels.Entities.OnlandVisualTrashAssessmentArea> onlandVisualTrashAssessmentAreas,
            ILookup<int, EFModels.Entities.OnlandVisualTrashAssessment> onlandVisualTrashAssessments, string uploadOVTAAreaUrl) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            MapServiceUrl = mapServiceUrl;
            OnlandVisualTrashAssessmentAreas = onlandVisualTrashAssessmentAreas;
            OnlandVisualTrashAssessments = onlandVisualTrashAssessments;
            EntityName = "Data Hub";
            EntityUrl = SitkaRoute<DataHubController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            PageTitle = "Download OVTA Areas";
            StormwaterJurisdictions =
                stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                    .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), y => y.Organization.OrganizationName);
            UploadOVTAAreaUrl=uploadOVTAAreaUrl;
        }
    }
}
