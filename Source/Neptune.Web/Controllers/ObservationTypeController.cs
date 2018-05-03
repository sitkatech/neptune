/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPTypeController.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Models;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPType;
using Newtonsoft.Json;
using Detail = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.Detail;
using DetailViewData = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.DetailViewData;
using Edit = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.Edit;
using EditViewData = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.EditViewData;
using EditViewModel = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.EditViewModel;
using Manage = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.Manage;
using ManageViewData = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.ManageViewData;

namespace Neptune.Web.Controllers
{
    public class ObservationTypeController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageObservationTypesList);
            var viewData = new ManageViewData(CurrentPerson, neptunePage);
            return RazorView<Manage, ManageViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public ViewResult Manage()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageObservationTypesList);
            var viewData = new ManageViewData(CurrentPerson, neptunePage);
            return RazorView<Manage, ManageViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessmentObservationType> ObservationTypeGridJsonData()
        {
            var gridSpec = new ObservationTypeGridSpec(CurrentPerson);
            var observationTypes = HttpRequestStorage.DatabaseEntities.ObservationTypes.ToList().OrderBy(x => x.ObservationTypeName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMPAssessmentObservationType>(observationTypes, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPType> TreatmentBMPTypeGridJsonData(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var gridSpec = new TreatmentBMPTypeGridSpec(CurrentPerson);
            var TreatmentBMPAssessmentObservationType = observationTypePrimaryKey.EntityObject;
            var treatmentBMPTypes = TreatmentBMPAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypes.Select(x => x.TreatmentBMPType).OrderBy(x => x.TreatmentBMPTypeName).ToList();
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
            var TreatmentBMPAssessmentObservationType = new TreatmentBMPAssessmentObservationType(String.Empty, ObservationTypeSpecification.PassFail_PassFail_None, String.Empty);
            viewModel.UpdateModel(TreatmentBMPAssessmentObservationType, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.AllObservationTypes.Add(TreatmentBMPAssessmentObservationType);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"Observation Type {TreatmentBMPAssessmentObservationType.ObservationTypeName} succesfully created.");

            return RedirectToAction(new SitkaRoute<ObservationTypeController>(c => c.Detail(TreatmentBMPAssessmentObservationType.PrimaryKey)));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Edit(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var TreatmentBMPAssessmentObservationType = observationTypePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(TreatmentBMPAssessmentObservationType);
            return ViewEdit(viewModel, TreatmentBMPAssessmentObservationType);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(ObservationTypePrimaryKey observationTypePrimaryKey, EditViewModel viewModel)
        {
            var TreatmentBMPAssessmentObservationType = observationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, TreatmentBMPAssessmentObservationType);
            }
            viewModel.UpdateModel(TreatmentBMPAssessmentObservationType, CurrentPerson);

            return RedirectToAction(new SitkaRoute<ObservationTypeController>(c => c.Detail(TreatmentBMPAssessmentObservationType.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            var instructionsNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageObservationTypeInstructions);
            var observationInstructionsNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageObservationTypeObservationInstructions);
            var labelAndUnitsInstructionsNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageObservationTypeLabelsAndUnitsInstructions);

            var submitUrl = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.ObservationTypeID) ? SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(x => x.Edit(viewModel.ObservationTypeID)) : SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(x => x.New());
            var viewData = new EditViewData(CurrentPerson, MeasurementUnitType.All, ObservationTypeSpecification.All, ObservationThresholdType.All, ObservationTargetType.All, ObservationTypeCollectionMethod.All.Except(new []{ObservationTypeCollectionMethod.Rate}).ToList(), submitUrl, instructionsNeptunePage, observationInstructionsNeptunePage, labelAndUnitsInstructionsNeptunePage, TreatmentBMPAssessmentObservationType);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [NeptuneViewFeature]
        public ViewResult Detail(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var TreatmentBMPAssessmentObservationType = observationTypePrimaryKey.EntityObject;

            var viewData = new DetailViewData(CurrentPerson, TreatmentBMPAssessmentObservationType);
            return RazorView<Detail, DetailViewData>(viewData);
        }
        
        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult DeleteObservationType(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var TreatmentBMPAssessmentObservationType = observationTypePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(TreatmentBMPAssessmentObservationType.ObservationTypeID);
            return ViewDeleteObservationType(TreatmentBMPAssessmentObservationType, viewModel);
        }

        private PartialViewResult ViewDeleteObservationType(TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPTypeLabel = TreatmentBMPAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypes.Count == 1 ? FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabel() : FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabelPluralized();
            var treatmentBMPObservationLabel = TreatmentBMPAssessmentObservationType.TreatmentBMPObservations.Count == 1 ? "Observation" : "Observations";
            var confirmMessage = $"{FieldDefinition.TreatmentBMPAssessmentObservationType.GetFieldDefinitionLabel()} '{TreatmentBMPAssessmentObservationType.ObservationTypeName}' is related to {TreatmentBMPAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypes.Count} {treatmentBMPTypeLabel} and has {TreatmentBMPAssessmentObservationType.TreatmentBMPObservations.Count} {treatmentBMPObservationLabel}.<br /><br />Are you sure you want to delete this {FieldDefinition.TreatmentBMPAssessmentObservationType.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteObservationType(ObservationTypePrimaryKey observationTypePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var TreatmentBMPAssessmentObservationType = observationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteObservationType(TreatmentBMPAssessmentObservationType, viewModel);
            }

            var message = $"{FieldDefinition.TreatmentBMPAssessmentObservationType.GetFieldDefinitionLabel()} '{TreatmentBMPAssessmentObservationType.ObservationTypeName}' successfully deleted!";
            TreatmentBMPAssessmentObservationType.DeleteFull();
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }
        
        [HttpGet]
        [NeptuneViewFeature]
        public PartialViewResult DiscreteDetailSchema(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var TreatmentBMPAssessmentObservationType = observationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<DiscreteObservationTypeSchema>(TreatmentBMPAssessmentObservationType.ObservationTypeSchema);
            var viewData = new ViewDiscreteValueSchemaDetailViewData(schema);
            return RazorPartialView<ViewDiscreteValueSchemaDetail, ViewDiscreteValueSchemaDetailViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public PartialViewResult RateDetailSchema(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var TreatmentBMPAssessmentObservationType = observationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<RateObservationTypeSchema>(TreatmentBMPAssessmentObservationType.ObservationTypeSchema);
            var viewData = new ViewRateSchemaDetailViewData(schema);
            return RazorPartialView<ViewRateSchemaDetail, ViewRateSchemaDetailViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public PartialViewResult PassFailDetailSchema(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var TreatmentBMPAssessmentObservationType = observationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<PassFailObservationTypeSchema>(TreatmentBMPAssessmentObservationType.ObservationTypeSchema);
            var viewData = new ViewPassFailSchemaDetailViewData(schema);
            return RazorPartialView<ViewPassFailSchemaDetail, ViewPassFailSchemaDetailViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public PartialViewResult PercentageDetailSchema(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var TreatmentBMPAssessmentObservationType = observationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<PercentageObservationTypeSchema>(TreatmentBMPAssessmentObservationType.ObservationTypeSchema);
            var viewData = new ViewPercentageSchemaDetailViewData(schema);
            return RazorPartialView<ViewPercentageSchemaDetail, ViewPercentageSchemaDetailViewData>(viewData);
        }
    }
}
