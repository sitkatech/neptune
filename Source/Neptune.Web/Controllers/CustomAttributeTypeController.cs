using System;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Models;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.CustomAttributeType;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPType;
using Detail = Neptune.Web.Views.CustomAttributeType.Detail;
using DetailViewData = Neptune.Web.Views.CustomAttributeType.DetailViewData;
using Edit = Neptune.Web.Views.CustomAttributeType.Edit;
using EditViewData = Neptune.Web.Views.CustomAttributeType.EditViewData;
using EditViewModel = Neptune.Web.Views.CustomAttributeType.EditViewModel;
using Manage = Neptune.Web.Views.CustomAttributeType.Manage;
using ManageViewData = Neptune.Web.Views.CustomAttributeType.ManageViewData;

namespace Neptune.Web.Controllers
{
    public class CustomAttributeTypeController : NeptuneBaseController
    {
        [NeptuneAdminFeature]
        public ViewResult Manage()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageCustomAttributeTypesList);
            var viewData = new ManageViewData(CurrentPerson, neptunePage);
            return RazorView<Manage, ManageViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<CustomAttributeType> CustomAttributeTypeGridJsonData()
        {
            var gridSpec = new CustomAttributeTypeGridSpec();
            var customAttributeTypes = HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.ToList().OrderBy(x => x.CustomAttributeTypeName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<CustomAttributeType>(customAttributeTypes, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<TreatmentBMPType> TreatmentBMPTypeGridJsonData(CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey)
        {
            var gridSpec = new TreatmentBMPTypeGridSpec(CurrentPerson);
            var customAttributeType = customAttributeTypePrimaryKey.EntityObject;
            var treatmentBMPTypes = customAttributeType.TreatmentBMPTypeAttributeTypes.Select(x => x.TreatmentBMPType).OrderBy(x => x.TreatmentBMPTypeName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMPType>(treatmentBMPTypes, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult New()
        {
            var viewModel = new EditViewModel();
            return ViewEdit(viewModel, null);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, null);
            }

            var customAttributeTypePurpose = CustomAttributeTypePurpose.AllLookupDictionary[viewModel.CustomAttributeTypePurposeID.GetValueOrDefault()];

            var customAttributeType = new CustomAttributeType(String.Empty, CustomAttributeDataType.String, false, customAttributeTypePurpose);
            viewModel.UpdateModel(customAttributeType, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.AllCustomAttributeTypes.Add(customAttributeType);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"Custom Attribute Type {customAttributeType.CustomAttributeTypeName} succesfully created.");

            return RedirectToAction(new SitkaRoute<CustomAttributeTypeController>(c => c.Detail(customAttributeType.PrimaryKey)));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Edit(CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey)
        {
            var customAttributeType = customAttributeTypePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(customAttributeType);
            return ViewEdit(viewModel, customAttributeType);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey, EditViewModel viewModel)
        {
            var customAttributeType = customAttributeTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, customAttributeType);
            }
            viewModel.UpdateModel(customAttributeType, CurrentPerson);

            return RedirectToAction(new SitkaRoute<CustomAttributeTypeController>(c => c.Detail(customAttributeType.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel, CustomAttributeType customAttributeType)
        {
            var instructionsNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageCustomAttributeTypeInstructions);
            var customAttributeInstructionsNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageCustomAttributeInstructions);

            var submitUrl = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.CustomAttributeTypeID) ? SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(x => x.Edit(viewModel.CustomAttributeTypeID)) : SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(x => x.New());
            var viewData = new EditViewData(CurrentPerson, MeasurementUnitType.All, CustomAttributeDataType.All, submitUrl, instructionsNeptunePage, customAttributeInstructionsNeptunePage, customAttributeType);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [NeptuneAdminFeature]
        public ViewResult Detail(CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey)
        {
            var customAttributeType = customAttributeTypePrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, customAttributeType);
            return RazorView<Detail, DetailViewData>(viewData);
        }


        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult DeleteCustomAttributeType(CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey)
        {
            var customAttributeType = customAttributeTypePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(customAttributeType.CustomAttributeTypeID);
            return ViewDeleteCustomAttributeType(customAttributeType, viewModel);
        }

        private PartialViewResult ViewDeleteCustomAttributeType(CustomAttributeType customAttributeType, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPTypeLabel = customAttributeType.TreatmentBMPTypeAttributeTypes.Count == 1 ? FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabel() : FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabelPluralized();
            var treatmentBMPLabel = customAttributeType.CustomAttributes.Count == 1 ? FieldDefinition.TreatmentBMP.GetFieldDefinitionLabel() : FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized();
            var confirmMessage = $"{FieldDefinition.CustomAttributeType.GetFieldDefinitionLabel()} '{customAttributeType.CustomAttributeTypeName}' is associated with {customAttributeType.TreatmentBMPTypeAttributeTypes.Count} {treatmentBMPTypeLabel} and {customAttributeType.CustomAttributes.Count} {treatmentBMPLabel}.<br /><br />Are you sure you want to delete this {FieldDefinition.CustomAttributeType.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteCustomAttributeType(CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var customAttributeType = customAttributeTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteCustomAttributeType(customAttributeType, viewModel);
            }

            var message = $"{FieldDefinition.CustomAttributeType.GetFieldDefinitionLabel()} '{customAttributeType.CustomAttributeTypeName}' successfully deleted!";
            customAttributeType.DeleteFull();
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }
    }
}