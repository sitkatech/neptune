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
using Neptune.Web.Views.ObservationType;
using Neptune.Web.Views.Shared;
using Newtonsoft.Json;
using Detail = Neptune.Web.Views.ObservationType.Detail;
using DetailViewData = Neptune.Web.Views.ObservationType.DetailViewData;

namespace Neptune.Web.Controllers
{
    public class ObservationTypeController : NeptuneBaseController
    {
        [NeptuneAdminFeature]
        public ViewResult Manage()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageObservationTypesList);
            var viewData = new ManageViewData(CurrentPerson, neptunePage);
            return RazorView<Manage, ManageViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<ObservationType> ObservationTypeGridJsonData()
        {
            var gridSpec = new ObservationTypeGridSpec(CurrentPerson);
            var observationTypes = HttpRequestStorage.DatabaseEntities.ObservationTypes.ToList().OrderBy(x => x.ObservationTypeName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ObservationType>(observationTypes, gridSpec);
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
            var observationType = new ObservationType(String.Empty, ObservationTypeSpecification.PassFail_PassFail_None, String.Empty);
            viewModel.UpdateModel(observationType, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.AllObservationTypes.Add(observationType);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"Observation Type {observationType.ObservationTypeName} succesfully created.");

            return RedirectToAction(new SitkaRoute<ObservationTypeController>(c => c.Detail(observationType.PrimaryKey)));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Edit(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var observationType = observationTypePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(observationType);
            return ViewEdit(viewModel, observationType);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(ObservationTypePrimaryKey observationTypePrimaryKey, EditViewModel viewModel)
        {
            var observationType = observationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, observationType);
            }
            viewModel.UpdateModel(observationType, CurrentPerson);

            return RedirectToAction(new SitkaRoute<ObservationTypeController>(c => c.Detail(observationType.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel, ObservationType observationType)
        {
            var instructionsNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageObservationTypeInstructions);
            var observationInstructionsNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageObservationTypeObservationInstructions);
            var labelAndUnitsInstructionsNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageObservationTypeLabelsAndUnitsInstructions);

            var submitUrl = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.ObservationTypeID) ? SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(x => x.Edit(viewModel.ObservationTypeID)) : SitkaRoute<ObservationTypeController>.BuildUrlFromExpression(x => x.New());
            var viewData = new EditViewData(CurrentPerson, MeasurementUnitType.All, ObservationTypeSpecification.All, ObservationThresholdType.All, ObservationTargetType.All, ObservationTypeCollectionMethod.All.Except(new []{ObservationTypeCollectionMethod.Rate}).ToList(), submitUrl, instructionsNeptunePage, observationInstructionsNeptunePage, labelAndUnitsInstructionsNeptunePage, observationType);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [NeptuneViewFeature]
        public ViewResult Detail(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var observationType = observationTypePrimaryKey.EntityObject;

            var viewData = new DetailViewData(CurrentPerson, observationType);
            return RazorView<Detail, DetailViewData>(viewData);
        }
        
        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult DeleteObservationType(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var observationType = observationTypePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(observationType.ObservationTypeID);
            return ViewDeleteObservationType(observationType, viewModel);
        }

        private PartialViewResult ViewDeleteObservationType(ObservationType observationType, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.ObservationType.GetFieldDefinitionLabel()} '{observationType.ObservationTypeName}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteObservationType(ObservationTypePrimaryKey observationTypePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var observationType = observationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteObservationType(observationType, viewModel);
            }
            observationType.TreatmentBMPObservations.DeleteTreatmentBMPObservation();
            observationType.TreatmentBMPBenchmarkAndThresholds.DeleteTreatmentBMPBenchmarkAndThreshold();
            observationType.TreatmentBMPTypeObservationTypes.DeleteTreatmentBMPTypeObservationType();

            observationType.DeleteObservationType();
            return new ModalDialogFormJsonResult();
        }
        
        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult DiscreteDetailSchema(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var observationType = observationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<DiscreteObservationTypeSchema>(observationType.ObservationTypeSchema);
            var viewData = new ViewDiscreteValueSchemaDetailViewData(schema);
            return RazorPartialView<ViewDiscreteValueSchemaDetail, ViewDiscreteValueSchemaDetailViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult RateDetailSchema(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var observationType = observationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<RateObservationTypeSchema>(observationType.ObservationTypeSchema);
            var viewData = new ViewRateSchemaDetailViewData(schema);
            return RazorPartialView<ViewRateSchemaDetail, ViewRateSchemaDetailViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult PassFailDetailSchema(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var observationType = observationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<PassFailObservationTypeSchema>(observationType.ObservationTypeSchema);
            var viewData = new ViewPassFailSchemaDetailViewData(schema);
            return RazorPartialView<ViewPassFailSchemaDetail, ViewPassFailSchemaDetailViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult PercentageDetailSchema(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var observationType = observationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<PercentageObservationTypeSchema>(observationType.ObservationTypeSchema);
            var viewData = new ViewPercentageSchemaDetailViewData(schema);
            return RazorPartialView<ViewPercentageSchemaDetail, ViewPercentageSchemaDetailViewData>(viewData);
        }
    }
}
