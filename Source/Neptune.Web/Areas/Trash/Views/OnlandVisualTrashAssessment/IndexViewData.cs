using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views;
using TreatmentBMPController = Neptune.Web.Controllers.TreatmentBMPController;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class IndexViewData : NeptuneViewData
    {
        public OVTAIndexGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public string NewUrl { get; }
        public bool HasManagePermissions { get; }


        public IndexViewData(Person currentPerson, NeptunePage neptunePage)
            : base(currentPerson, neptunePage)
        {
            PageTitle = "All OVTAs";
            EntityName = $"{FieldDefinition.OnlandVisualTrashAssessment.GetFieldDefinitionLabelPluralized()}";
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new OVTAIndexGridSpec(currentPerson, showDelete, showEdit)
            {
                ObjectNameSingular = "Assessment",
                ObjectNamePlural = "Assessments",
                SaveFiltersInCookie = true
            };
            GridName = "treatmentBMPsGrid";
            GridDataUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(j => j.OVTAGridJsonData());
            NewUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Instructions(null));
            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);

        }
    }
}