﻿using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views;
using TreatmentBMPController = Neptune.Web.Controllers.TreatmentBMPController;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
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
        public bool HasManagePermissions { get; }


        public IndexViewData(Person currentPerson, NeptunePage neptunePage)
            : base(currentPerson, neptunePage)
        {
            PageTitle = "All OVTAs";
            EntityName = $"{FieldDefinition.OnlandVisualTrashAssessment.GetFieldDefinitionLabelPluralized()}";
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new OnlandVisualTrashAssessmentIndexGridSpec(currentPerson, showDelete, showEdit, true)
            {
                ObjectNameSingular = "Assessment",
                ObjectNamePlural = "Assessments",
                SaveFiltersInCookie = true
            };
            GridName = "onlandVisualTrashAssessmentsGrid";
            GridDataUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(j => j.OVTAGridJsonData());
            AreaGridSpec = new OnlandVisualTrashAssessmentAreaIndexGridSpec(currentPerson)
            {
                ObjectNameSingular = "Area",
                ObjectNamePlural = "Areas",
                SaveFiltersInCookie = true
            };
            AreaGridName = "onlandVisualTrashAssessmentsAreaGrid";
            AreaGridDataUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(j => j.OnlandVisualTrashAssessmentAreaGridData());
            NewUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Instructions(null));
            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);

        }
    }
}