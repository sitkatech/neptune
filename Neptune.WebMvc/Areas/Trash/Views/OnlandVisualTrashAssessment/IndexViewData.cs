using Neptune.EFModels.Entities;
using Neptune.WebMvc.Areas.Trash.Controllers;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class IndexViewData : TrashModuleViewData
    {
        public OnlandVisualTrashAssessmentIndexGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public OnlandVisualTrashAssessmentAreaIndexGridSpec AreaGridSpec { get; }
        public string AreaGridName { get; }
        public string AreaGridDataUrl { get; }
        public string NewUrl { get; }
        public string ExportUrl { get; }
        public bool HasManagePermissions { get; }
        public bool HasEditPermissions { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, NeptunePage neptunePage, string exportUrl)
            : base(httpContext, linkGenerator, currentPerson, webConfiguration, neptunePage)
        {
            ExportUrl = exportUrl;
            PageTitle = "All OVTAs";
            EntityName = $"{FieldDefinitionType.OnlandVisualTrashAssessment.GetFieldDefinitionLabelPluralized()}";
            GridSpec = new OnlandVisualTrashAssessmentIndexGridSpec(linkGenerator, currentPerson, true)
            {
                ObjectNameSingular = "Assessment",
                ObjectNamePlural = "Assessments",
                SaveFiltersInCookie = true
            };
            GridName = "onlandVisualTrashAssessmentsGrid";
            GridDataUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.OVTAGridJsonData());
            AreaGridSpec = new OnlandVisualTrashAssessmentAreaIndexGridSpec(linkGenerator, currentPerson, null)
            {
                ObjectNameSingular = "Area",
                ObjectNamePlural = "Areas",
                SaveFiltersInCookie = true
            };
            AreaGridName = "onlandVisualTrashAssessmentsAreaGrid";
            AreaGridDataUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.OnlandVisualTrashAssessmentAreaGridData());
            NewUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Instructions(null));
            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            HasEditPermissions = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
        }
    }
}