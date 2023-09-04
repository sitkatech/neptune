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

using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;
using Neptune.Web.Common.Models;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPType;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Detail = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.Detail;
using DetailViewData = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.DetailViewData;
using Edit = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.Edit;
using EditViewData = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.EditViewData;
using EditViewModel = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.EditViewModel;
using Manage = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.Manage;
using ManageViewData = Neptune.Web.Views.TreatmentBMPAssessmentObservationType.ManageViewData;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPAssessmentObservationTypeController : NeptuneBaseController<TreatmentBMPAssessmentObservationTypeController>
    {
        public TreatmentBMPAssessmentObservationTypeController(NeptuneDbContext dbContext, ILogger<TreatmentBMPAssessmentObservationTypeController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        [HttpGet]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ManageObservationTypesList);
            var viewData = new ManageViewData(HttpContext, _linkGenerator, CurrentPerson, neptunePage);
            return RazorView<Manage, ManageViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Manage()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ManageObservationTypesList);
            var viewData = new ManageViewData(HttpContext, _linkGenerator, CurrentPerson, neptunePage);
            return RazorView<Manage, ManageViewData>(viewData);
        }

        [HttpGet]
        public GridJsonNetJObjectResult<TreatmentBMPAssessmentObservationType> ObservationTypeGridJsonData()
        {
            var gridSpec = new TreatmentBMPAssessmentObservationTypeGridSpec(_linkGenerator, CurrentPerson);
            var observationTypes = _dbContext.TreatmentBMPAssessmentObservationTypes.ToList().OrderBy(x => x.TreatmentBMPAssessmentObservationTypeName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMPAssessmentObservationType>(observationTypes, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        public GridJsonNetJObjectResult<TreatmentBMPType> TreatmentBMPTypeGridJsonData(TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var gridSpec = new TreatmentBMPTypeGridSpec(_linkGenerator, CurrentPerson);
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            var treatmentBMPTypes = treatmentBMPAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypes.Select(x => x.TreatmentBMPType).OrderBy(x => x.TreatmentBMPTypeName).ToList();
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
        public async Task<IActionResult> New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, null);
            }

            var treatmentBMPAssessmentObservationType = new TreatmentBMPAssessmentObservationType
            {
                ObservationTypeSpecificationID =
                    ObservationTypeSpecification.PassFail_PassFail_None.ObservationTypeSpecificationID
            };
            viewModel.UpdateModel(treatmentBMPAssessmentObservationType, CurrentPerson);
            await _dbContext.TreatmentBMPAssessmentObservationTypes.AddAsync(treatmentBMPAssessmentObservationType);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay($"Observation Type {treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName} successfully created.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentObservationTypeController>(_linkGenerator, x => x.Detail(treatmentBMPAssessmentObservationType.PrimaryKey)));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Edit(TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(treatmentBMPAssessmentObservationType);
            return ViewEdit(viewModel, treatmentBMPAssessmentObservationType);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public async Task<IActionResult> Edit(TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, treatmentBMPAssessmentObservationType);
            }
            viewModel.UpdateModel(treatmentBMPAssessmentObservationType, CurrentPerson);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentObservationTypeController>(_linkGenerator, x => x.Detail(treatmentBMPAssessmentObservationType.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var instructionsNeptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ManageObservationTypeInstructions);
            var observationInstructionsNeptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ManageObservationTypeObservationInstructions);
            var labelAndUnitsInstructionsNeptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ManageObservationTypeLabelsAndUnitsInstructions);

            var submitUrl = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.TreatmentBMPAssessmentObservationTypeID) ? SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(_linkGenerator, x => x.Edit(viewModel.TreatmentBMPAssessmentObservationTypeID)) : SitkaRoute<TreatmentBMPAssessmentObservationTypeController>.BuildUrlFromExpression(_linkGenerator, x => x.New());
            var viewData = new EditViewData(HttpContext, _linkGenerator, CurrentPerson, MeasurementUnitType.All, ObservationTypeSpecification.All, ObservationThresholdType.All, ObservationTargetType.All, ObservationTypeCollectionMethod.All.ToList(), submitUrl, instructionsNeptunePage, observationInstructionsNeptunePage, labelAndUnitsInstructionsNeptunePage, treatmentBMPAssessmentObservationType);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [AnonymousUnclassifiedFeature]
        public ViewResult Detail(TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;

            var viewData = new DetailViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMPAssessmentObservationType);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult Delete(TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            return ViewDeleteObservationType(treatmentBMPAssessmentObservationType, viewModel);
        }

        private PartialViewResult ViewDeleteObservationType(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPTypeLabel = treatmentBMPAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypes.Count == 1 ? FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabel() : FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabelPluralized();
            var treatmentBMPObservationLabel = treatmentBMPAssessmentObservationType.TreatmentBMPObservations.Count == 1 ? "Observation" : "Observations";
            var confirmMessage = $"{FieldDefinitionType.TreatmentBMPAssessmentObservationType.GetFieldDefinitionLabel()} '{treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName}' is related to {treatmentBMPAssessmentObservationType.TreatmentBMPTypeAssessmentObservationTypes.Count} {treatmentBMPTypeLabel} and has {treatmentBMPAssessmentObservationType.TreatmentBMPObservations.Count} {treatmentBMPObservationLabel}.<br /><br />Are you sure you want to delete this {FieldDefinitionType.TreatmentBMPAssessmentObservationType.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public async Task<IActionResult> Delete(TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteObservationType(treatmentBMPAssessmentObservationType, viewModel);
            }

            var message = $"{FieldDefinitionType.TreatmentBMPAssessmentObservationType.GetFieldDefinitionLabel()} '{treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName}' successfully deleted!";
            treatmentBMPAssessmentObservationType.DeleteFull(_dbContext);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public PartialViewResult DiscreteDetailSchema(TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<DiscreteObservationTypeSchema>(treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeSchema);
            var viewData = new ViewDiscreteValueSchemaDetailViewData(schema);
            return RazorPartialView<ViewDiscreteValueSchemaDetail, ViewDiscreteValueSchemaDetailViewData>(viewData);
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public PartialViewResult RateDetailSchema(TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<RateObservationTypeSchema>(treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeSchema);
            var viewData = new ViewRateSchemaDetailViewData(schema);
            return RazorPartialView<ViewRateSchemaDetail, ViewRateSchemaDetailViewData>(viewData);
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public PartialViewResult PassFailDetailSchema(TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<PassFailObservationTypeSchema>(treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeSchema);
            var viewData = new ViewPassFailSchemaDetailViewData(schema);
            return RazorPartialView<ViewPassFailSchemaDetail, ViewPassFailSchemaDetailViewData>(viewData);
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        public PartialViewResult PercentageDetailSchema(TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            var schema = JsonConvert.DeserializeObject<PercentageObservationTypeSchema>(treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeSchema);
            var viewData = new ViewPercentageSchemaDetailViewData(schema);
            return RazorPartialView<ViewPercentageSchemaDetail, ViewPercentageSchemaDetailViewData>(viewData);
        }

        // This Get has to exist so that the jQuery posting on the front-end will work
        [HttpGet]
        [JurisdictionEditFeature]
        public ContentResult PreviewObservationType()
        {
            return Content("");
        }

        // This Post looks like it has zero references, but it actually is consumed by the jQuery posting on the front-end
        [HttpPost]
        [JurisdictionEditFeature]
        public ActionResult PreviewObservationType(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var modelStateSerialized = JObject
                    .FromObject(ModelState.ToDictionary(x => x.Key,
                        x => x.Value.Errors.Select(y => y.ErrorMessage).ToList())).ToString(Formatting.None);
                Response.StatusCode = 400;
                Response.ContentType = "application/json";
                return Content(modelStateSerialized);
            }

            var observationTypeCollectionMethod = ObservationTypeCollectionMethod.All.Single(x => x.ObservationTypeCollectionMethodID == viewModel.ObservationTypeCollectionMethodID);
            var observationTypeSpecification = ObservationTypeSpecification.All.Single(x =>
                x.ObservationTargetTypeID == viewModel.ObservationTargetTypeID &&
                x.ObservationThresholdTypeID == viewModel.ObservationThresholdTypeID &&
                x.ObservationTypeCollectionMethodID == viewModel.ObservationTypeCollectionMethodID);
            var treatmentBMPAssessmentObservationType = new TreatmentBMPAssessmentObservationType
            {
                TreatmentBMPAssessmentObservationTypeName = viewModel.TreatmentBMPAssessmentObservationTypeName,
                ObservationTypeSpecificationID = observationTypeSpecification.ObservationTypeSpecificationID,
                TreatmentBMPAssessmentObservationTypeSchema = viewModel.TreatmentBMPAssessmentObservationTypeSchema
            };
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    var discreteCollectionMethodViewData = new DiscreteCollectionMethodViewData(treatmentBMPAssessmentObservationType);
                    return RazorPartialView<DiscreteCollectionMethod, DiscreteCollectionMethodViewData>(discreteCollectionMethodViewData);
                case ObservationTypeCollectionMethodEnum.PassFail:
                    var passFailCollectionMethodViewData = new PassFailCollectionMethodViewData(treatmentBMPAssessmentObservationType);
                    return RazorPartialView<PassFailCollectionMethod, PassFailCollectionMethodViewData>(passFailCollectionMethodViewData);
                case ObservationTypeCollectionMethodEnum.Percentage:
                    var percentageCollectionMethodViewData = new PercentageCollectionMethodViewData(treatmentBMPAssessmentObservationType);
                    return RazorPartialView<PercentageCollectionMethod, PercentageCollectionMethodViewData>(percentageCollectionMethodViewData);
                default:
                    throw new ArgumentException($"Observation Collection Method {observationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName} not supported by Observation Type Preview.");
            }
        }

    }
}
