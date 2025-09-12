﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services.Filters;
using Neptune.WebMvc.Views.CustomAttributeType;
using Neptune.WebMvc.Views.Shared;
using Neptune.WebMvc.Views.TreatmentBMPType;
using Detail = Neptune.WebMvc.Views.CustomAttributeType.Detail;
using DetailViewData = Neptune.WebMvc.Views.CustomAttributeType.DetailViewData;
using Edit = Neptune.WebMvc.Views.CustomAttributeType.Edit;
using EditViewData = Neptune.WebMvc.Views.CustomAttributeType.EditViewData;
using EditViewModel = Neptune.WebMvc.Views.CustomAttributeType.EditViewModel;
using Manage = Neptune.WebMvc.Views.CustomAttributeType.Manage;
using ManageViewData = Neptune.WebMvc.Views.CustomAttributeType.ManageViewData;

namespace Neptune.WebMvc.Controllers
{
    public class CustomAttributeTypeController(
        NeptuneDbContext dbContext,
        ILogger<CustomAttributeTypeController> logger,
        IOptions<WebConfiguration> webConfiguration,
        LinkGenerator linkGenerator)
        : NeptuneBaseController<CustomAttributeTypeController>(dbContext, logger, linkGenerator, webConfiguration)
    {
        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Manage()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ManageCustomAttributeTypesList);
            var viewData = new ManageViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage);
            return RazorView<Manage, ManageViewData>(viewData);
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<CustomAttributeType> CustomAttributeTypeGridJsonData()
        {
            var gridSpec = new CustomAttributeTypeGridSpec(_linkGenerator);
            var customAttributeTypes = CustomAttributeTypes.List(_dbContext);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<CustomAttributeType>(customAttributeTypes, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{customAttributeTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("customAttributeTypePrimaryKey")]
        public GridJsonNetJObjectResult<TreatmentBMPType> TreatmentBMPTypeGridJsonData([FromRoute] CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey)
        {
            var countByTreatmentBMPType = TreatmentBMPs.ListCountByTreatmentBMPType(_dbContext);
            var gridSpec = new TreatmentBMPTypeGridSpec(_linkGenerator, CurrentPerson, countByTreatmentBMPType);
            var customAttributeType = CustomAttributeTypes.GetByID(_dbContext, customAttributeTypePrimaryKey);
            var treatmentBMPTypes = customAttributeType.TreatmentBMPTypeCustomAttributeTypes.Select(x => x.TreatmentBMPType).OrderBy(x => x.TreatmentBMPTypeName).ToList();
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

            if (viewModel.CustomAttributeTypePurposeID is (int)CustomAttributeTypePurposeEnum.Modeling)
            {
                SetErrorForDisplay("Because of their sensitive nature and ability to impact large-scale calculations, Modeling attributes may not be added through this editor. Please contact a system administrator if you need to add Modeling attributes.");
                return ViewEdit(viewModel, null);
            }

            var customAttributeType = new CustomAttributeType();
            viewModel.UpdateModel(customAttributeType, CurrentPerson);
            await _dbContext.CustomAttributeTypes.AddAsync(customAttributeType);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay($"Custom Attribute Type {customAttributeType.CustomAttributeTypeName} successfully created.");

            return RedirectToAction(new SitkaRoute<CustomAttributeTypeController>(_linkGenerator, x => x.Detail(customAttributeType.PrimaryKey)));
        }

        [HttpGet("{customAttributeTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("customAttributeTypePrimaryKey")]
        public ViewResult Edit([FromRoute] CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey)
        {
            var customAttributeType = customAttributeTypePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(customAttributeType);
            return ViewEdit(viewModel, customAttributeType);
        }

        [HttpPost("{customAttributeTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("customAttributeTypePrimaryKey")]
        public async Task<IActionResult> Edit([FromRoute] CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey, EditViewModel viewModel)
        {
            var customAttributeType = customAttributeTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, customAttributeType);
            }
            viewModel.UpdateModel(customAttributeType, CurrentPerson);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(new SitkaRoute<CustomAttributeTypeController>(_linkGenerator, x => x.Detail(customAttributeType.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel, CustomAttributeType customAttributeType)
        {
            var instructionsNeptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ManageCustomAttributeTypeInstructions);
            var customAttributeInstructionsNeptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ManageCustomAttributeInstructions);

            var submitUrl = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.CustomAttributeTypeID) ? SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(_linkGenerator, x => x.Edit(viewModel.CustomAttributeTypeID)) : SitkaRoute<CustomAttributeTypeController>.BuildUrlFromExpression(_linkGenerator, x => x.New());
            var viewData = new EditViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, MeasurementUnitType.All, CustomAttributeDataType.All, submitUrl, instructionsNeptunePage, customAttributeInstructionsNeptunePage, customAttributeType);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{customAttributeTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("customAttributeTypePrimaryKey")]
        public ViewResult Detail([FromRoute] CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey)
        {
            var customAttributeType = customAttributeTypePrimaryKey.EntityObject;
            var countByTreatmentBMPType = new Dictionary<int, int>();
            var viewData = new DetailViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, customAttributeType, countByTreatmentBMPType);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{customAttributeTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("customAttributeTypePrimaryKey")]
        public PartialViewResult Delete([FromRoute] CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey)
        {
            var customAttributeType = CustomAttributeTypes.GetByID(_dbContext, customAttributeTypePrimaryKey);
            var viewModel = new ConfirmDialogFormViewModel(customAttributeType.CustomAttributeTypeID);
            return ViewDeleteCustomAttributeType(customAttributeType, viewModel);
        }

        private PartialViewResult ViewDeleteCustomAttributeType(CustomAttributeType customAttributeType, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPTypeCustomAttributeTypes = customAttributeType.TreatmentBMPTypeCustomAttributeTypes;
            var treatmentBMPTypeLabel = treatmentBMPTypeCustomAttributeTypes.Count == 1 ? FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabel() : FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabelPluralized();
            string confirmMessage;
            if (customAttributeType.CustomAttributeTypePurpose != CustomAttributeTypePurpose.Maintenance)
            {
                var customAttributes = CustomAttributes.ListByCustomAttributeTypeID(_dbContext, customAttributeType.CustomAttributeTypeID);
                var treatmentBMPLabel = customAttributes.Count == 1 ? FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabel() : FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized();
                confirmMessage =
                    $"Attribute Type '{customAttributeType.CustomAttributeTypeName}' is associated with {treatmentBMPTypeCustomAttributeTypes.Count.ToGroupedNumeric()} {treatmentBMPTypeLabel} and {customAttributes.Count.ToGroupedNumeric()} {treatmentBMPLabel}.<br /><br />Are you sure you want to delete this {FieldDefinitionType.CustomAttributeType.GetFieldDefinitionLabel()}?";
            }
            else
            {
                
                var maintenanceRecordCount = MaintenanceRecordObservations.ListByCustomAttributeTypeID(_dbContext, customAttributeType.CustomAttributeTypeID).Count;
                var maintenanceRecordLabel = maintenanceRecordCount == 1
                    ? FieldDefinitionType.MaintenanceRecord.GetFieldDefinitionLabel()
                    : FieldDefinitionType.MaintenanceRecord.GetFieldDefinitionLabelPluralized();
                confirmMessage =
                    $"Attribute Type '{customAttributeType.CustomAttributeTypeName}' is associated with {treatmentBMPTypeCustomAttributeTypes.Count.ToGroupedNumeric()} {treatmentBMPTypeLabel} and {maintenanceRecordCount.ToGroupedNumeric()} {maintenanceRecordLabel}.<br /><br />Are you sure you want to delete this {FieldDefinitionType.CustomAttributeType.GetFieldDefinitionLabel()}?";
            }
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost("{customAttributeTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("customAttributeTypePrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var customAttributeType = customAttributeTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteCustomAttributeType(customAttributeType, viewModel);
            }

            if (customAttributeType.CustomAttributeTypePurposeID == (int)CustomAttributeTypePurposeEnum.Modeling)
            {
                SetErrorForDisplay("Because of their necessity in internal system calculations, modeling attributes may not be removed from the system. Please contact an admin to make changes to attributes of this type.");
                return new ModalDialogFormJsonResult();
            }

            var message = $"{FieldDefinitionType.CustomAttributeType.GetFieldDefinitionLabel()} '{customAttributeType.CustomAttributeTypeName}' successfully deleted!";
            await customAttributeType.DeleteFull(_dbContext);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }
    }
}