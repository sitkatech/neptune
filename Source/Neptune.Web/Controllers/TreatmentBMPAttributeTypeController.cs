﻿using System;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Models;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPAttributeType;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPAttributeTypeController : NeptuneBaseController
    {
        [NeptuneAdminFeature]
        public ViewResult Manage()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageTreatmentBMPAttributeTypesList);
            var viewData = new ManageViewData(CurrentPerson, neptunePage);
            return RazorView<Manage, ManageViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAttributeType> TreatmentBMPAttributeTypeGridJsonData()
        {
            var gridSpec = new TreatmentBMPAttributeTypeGridSpec();
            var treatmentBMPAttributeTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPAttributeTypes.ToList().OrderBy(x => x.TreatmentBMPAttributeTypeName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMPAttributeType>(treatmentBMPAttributeTypes, gridSpec);
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
            var treatmentBMPAttributeType = new TreatmentBMPAttributeType(String.Empty, TreatmentBMPAttributeDataType.String, false);
            viewModel.UpdateModel(treatmentBMPAttributeType, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAttributeTypes.Add(treatmentBMPAttributeType);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"Treatment BMP Attribute Type {treatmentBMPAttributeType.TreatmentBMPAttributeTypeName} succesfully created.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPAttributeTypeController>(c => c.Detail(treatmentBMPAttributeType.PrimaryKey)));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Edit(TreatmentBMPAttributeTypePrimaryKey treatmentBMPAttributeTypePrimaryKey)
        {
            var treatmentBMPAttributeType = treatmentBMPAttributeTypePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(treatmentBMPAttributeType);
            return ViewEdit(viewModel, treatmentBMPAttributeType);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(TreatmentBMPAttributeTypePrimaryKey treatmentBMPAttributeTypePrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMPAttributeType = treatmentBMPAttributeTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, treatmentBMPAttributeType);
            }
            viewModel.UpdateModel(treatmentBMPAttributeType, CurrentPerson);

            return RedirectToAction(new SitkaRoute<TreatmentBMPAttributeTypeController>(c => c.Detail(treatmentBMPAttributeType.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel, TreatmentBMPAttributeType treatmentBMPAttributeType)
        {
            var instructionsNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageTreatmentBMPAttributeTypeInstructions);
            var treatmentBMPAttributeInstructionsNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageTreatmentBMPAttributeInstructions);

            var submitUrl = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.TreatmentBMPAttributeTypeID) ? SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(x => x.Edit(viewModel.TreatmentBMPAttributeTypeID)) : SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(x => x.New());
            var viewData = new EditViewData(CurrentPerson, MeasurementUnitType.All, TreatmentBMPAttributeDataType.All, submitUrl, instructionsNeptunePage, treatmentBMPAttributeInstructionsNeptunePage, treatmentBMPAttributeType);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [NeptuneViewFeature]
        public ViewResult Detail(TreatmentBMPAttributeTypePrimaryKey treatmentBMPAttributeTypePrimaryKey)
        {
            var treatmentBMPAttributeType = treatmentBMPAttributeTypePrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, treatmentBMPAttributeType);
            return RazorView<Detail, DetailViewData>(viewData);
        }


        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult DeleteTreatmentBMPAttributeType(TreatmentBMPAttributeTypePrimaryKey treatmentBMPAttributeTypePrimaryKey)
        {
            var treatmentBMPAttributeType = treatmentBMPAttributeTypePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMPAttributeType.TreatmentBMPAttributeTypeID);
            return ViewDeleteTreatmentBMPAttributeType(treatmentBMPAttributeType, viewModel);
        }

        private PartialViewResult ViewDeleteTreatmentBMPAttributeType(TreatmentBMPAttributeType treatmentBMPAttributeType, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !treatmentBMPAttributeType.HasDependentObjects();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this {FieldDefinition.TreatmentBMPAttributeType.GetFieldDefinitionLabel()} '{treatmentBMPAttributeType.TreatmentBMPAttributeTypeName}'?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"{FieldDefinition.TreatmentBMPAttributeType.GetFieldDefinitionLabel()}", SitkaRoute<TreatmentBMPAttributeTypeController>.BuildLinkFromExpression(x => x.Detail(treatmentBMPAttributeType), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteTreatmentBMPAttributeType(TreatmentBMPAttributeTypePrimaryKey treatmentBMPAttributeTypePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPAttributeType = treatmentBMPAttributeTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentBMPAttributeType(treatmentBMPAttributeType, viewModel);
            }
            treatmentBMPAttributeType.DeleteTreatmentBMPAttributeType();
            return new ModalDialogFormJsonResult();
        }
    }
}