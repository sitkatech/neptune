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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.EFModels;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.Models;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.SortOrder;
using Neptune.Web.Views.TreatmentBMPType;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPTypeController : NeptuneBaseController<TreatmentBMPTypeController>
    {
        public TreatmentBMPTypeController(NeptuneDbContext dbContext, ILogger<TreatmentBMPTypeController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
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
        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<TreatmentBMPType> TreatmentBMPTypeGridJsonData()
        {
            var countByTreatmentBMPType = TreatmentBMPs.ListCountByTreatmentBMPType(_dbContext);
            var gridSpec = new TreatmentBMPTypeGridSpec(_linkGenerator, CurrentPerson, countByTreatmentBMPType);
            var treatmentBMPTypes = TreatmentBMPTypes.List(_dbContext);
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
            var treatmentBMPType = new TreatmentBMPType()
            {
                TreatmentBMPTypeName = viewModel.TreatmentBMPTypeName, 
                TreatmentBMPTypeDescription = viewModel.TreatmentBMPTypeDescription,
                IsAnalyzedInModelingModule = false
            };
            await _dbContext.TreatmentBMPTypes.AddAsync(treatmentBMPType);
            await _dbContext.SaveChangesAsync();

            await _dbContext.TreatmentBMPTypeAssessmentObservationTypes.LoadAsync();
            var treatmentBMPTypeAssessmentObservationTypes = new List<TreatmentBMPTypeAssessmentObservationType>();
            var allTreatmentBMPTypeAssessmentObservationTypes = _dbContext.TreatmentBMPTypeAssessmentObservationTypes;

            await _dbContext.TreatmentBMPTypeCustomAttributeTypes.LoadAsync();
            var treatmentBMPTypeAttributeTypes = new List<TreatmentBMPTypeCustomAttributeType>();
            var allTreatmentBMPTypeCustomAttributeTypes = _dbContext.TreatmentBMPTypeCustomAttributeTypes;

            viewModel.UpdateModel(treatmentBMPType, treatmentBMPTypeAssessmentObservationTypes, allTreatmentBMPTypeAssessmentObservationTypes, treatmentBMPTypeAttributeTypes, allTreatmentBMPTypeCustomAttributeTypes);
           
            SetMessageForDisplay($"Treatment BMP Type {treatmentBMPType.TreatmentBMPTypeName} successfully created.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPTypeController>(_linkGenerator, x => x.Detail(treatmentBMPType.PrimaryKey)));
        }

        [HttpGet("{treatmentBMPTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPTypePrimaryKey")]
        public ViewResult Edit([FromRoute] TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
        {
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMPTypePrimaryKey);
            var viewModel = new EditViewModel(treatmentBMPType);
            return ViewEdit(viewModel, treatmentBMPType);
        }

        [HttpPost("{treatmentBMPTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPTypePrimaryKey")]
        public async Task<IActionResult> Edit([FromRoute] TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMPType = TreatmentBMPTypes.GetByIDWithChangeTracking(_dbContext, treatmentBMPTypePrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, treatmentBMPType);
            }

            await _dbContext.TreatmentBMPTypeAssessmentObservationTypes.LoadAsync();
            var treatmentBMPTypeAssessmentObservationTypes = treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.ToList();
            var allTreatmentBMPTypeAssessmentObservationTypes = _dbContext.TreatmentBMPTypeAssessmentObservationTypes;

            await _dbContext.TreatmentBMPTypeCustomAttributeTypes.LoadAsync();
            var treatmentBMPTypeAttributeTypes = treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.ToList();
            var allTreatmentBMPTypeCustomAttributeTypes = _dbContext.TreatmentBMPTypeCustomAttributeTypes;

            viewModel.UpdateModel(treatmentBMPType, treatmentBMPTypeAssessmentObservationTypes, allTreatmentBMPTypeAssessmentObservationTypes, treatmentBMPTypeAttributeTypes, allTreatmentBMPTypeCustomAttributeTypes);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(new SitkaRoute<TreatmentBMPTypeController>(_linkGenerator, x => x.Detail(treatmentBMPType.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel, TreatmentBMPType treatmentBMPType)
        {
            var instructionsNeptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ManageTreatmentBMPTypeInstructions);
            var submitUrl = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.TreatmentBMPTypeID) ? SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(_linkGenerator, x => x.Edit(viewModel.TreatmentBMPTypeID)) : SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(_linkGenerator, x => x.New());
            var observationTypes = treatmentBMPType?.TreatmentBMPTypeAssessmentObservationTypes.ToList() ?? new List<TreatmentBMPTypeAssessmentObservationType>();
            var customAttributeTypes = treatmentBMPType?.TreatmentBMPTypeCustomAttributeTypes.ToList() ?? new List<TreatmentBMPTypeCustomAttributeType>();
            var allTreatmentBMPAssessmentObservationTypes = _dbContext.TreatmentBMPAssessmentObservationTypes.ToList();
            var allCustomAttributeTypes = _dbContext.CustomAttributeTypes.ToList();
            var viewData = new EditViewData(HttpContext, _linkGenerator, CurrentPerson, observationTypes, submitUrl, instructionsNeptunePage, treatmentBMPType, customAttributeTypes, allTreatmentBMPAssessmentObservationTypes, allCustomAttributeTypes);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        public ViewResult Index()
        {
            var treatmentBMPTypes = TreatmentBMPTypes.List(_dbContext);
            var neptunePage = NeptunePages.GetNeptunePageByPageType( _dbContext, NeptunePageType.TreatmentBMPType);
            var countByTreatmentBMPType = TreatmentBMPs.ListCountByTreatmentBMPType(_dbContext);
            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson, neptunePage, treatmentBMPTypes, countByTreatmentBMPType);
            return RazorView<Views.TreatmentBMPType.Index, IndexViewData>(viewData);
        }

        [HttpGet("{treatmentBMPTypePrimaryKey}")]
        [NeptuneViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPTypePrimaryKey")]
        public ViewResult Detail([FromRoute] TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
        {
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMPTypePrimaryKey);
            var viewData = new DetailViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMPType);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{treatmentBMPTypePrimaryKey}")]
        [NeptuneViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPTypePrimaryKey")]
        public GridJsonNetJObjectResult<vTreatmentBMPDetailedWithTreatmentBMPEntity> TreatmentBMPsInTreatmentBMPTypeGridJsonData([FromRoute] TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
        {
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMPTypePrimaryKey);
            var stormwaterJurisdictionIDsPersonCanView = StormwaterJurisdictions.ListViewableIDsByPerson(_dbContext, CurrentPerson);
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(CurrentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new TreatmentBMPsInTreatmentBMPTypeGridSpec(CurrentPerson, showDelete, showEdit, treatmentBMPType, _linkGenerator);
            var treatmentBMPs = 
                TreatmentBMPs.GetNonPlanningModuleBMPs(_dbContext)
                    .Include(x => x.WaterQualityManagementPlan)
                    .Include(x => x.CustomAttributes)
                    .ThenInclude(x => x.CustomAttributeValues)
                    .Include(x => x.TreatmentBMPModelingAttributeTreatmentBMP)
                    .Include(x => x.Watershed)
                    .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID) && x.TreatmentBMPTypeID == treatmentBMPType.TreatmentBMPTypeID)
                    .ToList()
                    .Join(_dbContext.vTreatmentBMPDetaileds,
                        x => x.TreatmentBMPID,
                        y => y.TreatmentBMPID,
                        (x,y) => new vTreatmentBMPDetailedWithTreatmentBMPEntity(x, y))
                    .OrderBy(x => x.TreatmentBMP.TreatmentBMPName)
                    .ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vTreatmentBMPDetailedWithTreatmentBMPEntity>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{treatmentBMPTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPTypePrimaryKey")]
        public PartialViewResult Delete([FromRoute] TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMPType.TreatmentBMPTypeID);
            return ViewDeleteTreatmentBMPType(treatmentBMPType, viewModel);
        }

        private PartialViewResult ViewDeleteTreatmentBMPType(TreatmentBMPType treatmentBMPType, ConfirmDialogFormViewModel viewModel)
        {
            var countByTreatmentBMPType = TreatmentBMPs.ListCountByTreatmentBMPType(_dbContext);
            var treatmentBMPCount = countByTreatmentBMPType.TryGetValue(treatmentBMPType.TreatmentBMPTypeID, out var value) ? value : 0;
            var confirmMessage = $"Treatment BMP Type '{treatmentBMPType.TreatmentBMPTypeName}' has {treatmentBMPCount} Treatment BMPs.<br /><br />Are you sure you want to delete this Treatment BMP Type?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost("{treatmentBMPTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPTypePrimaryKey")]
        public ActionResult Delete([FromRoute] TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentBMPType(treatmentBMPType, viewModel);
            }

            var message = $"{FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabel()} '{treatmentBMPType.TreatmentBMPTypeName}' successfully deleted!";
            treatmentBMPType.DeleteFull(_dbContext);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet("{treatmentBMPTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPTypePrimaryKey")]
        public PartialViewResult EditObservationTypesSortOrder([FromRoute] TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            var viewModel = new EditSortOrderViewModel();
            return ViewEditObservationTypesSortOrder(treatmentBMPType, viewModel);
        }

        private PartialViewResult ViewEditObservationTypesSortOrder(TreatmentBMPType treatmentBMPType, EditSortOrderViewModel viewModel)
        {
            var treatmentBMPTypeAssessmentObservationTypes = TreatmentBMPTypeAssessmentObservationTypes.ListByTreatmentBMPTypeID(_dbContext, treatmentBMPType.TreatmentBMPTypeID);
            var viewData = new EditSortOrderViewData(new List<IHaveASortOrder>(treatmentBMPTypeAssessmentObservationTypes), "Observation Types");
            return RazorPartialView<EditSortOrder, EditSortOrderViewData, EditSortOrderViewModel>(viewData, viewModel);
        }

        [HttpPost("{treatmentBMPTypePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPTypePrimaryKey")]
        public async Task<IActionResult> EditObservationTypesSortOrder([FromRoute] TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey, EditSortOrderViewModel viewModel)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditObservationTypesSortOrder(treatmentBMPType, viewModel);
            }

            var treatmentBMPTypeAssessmentObservationTypes = TreatmentBMPTypeAssessmentObservationTypes.ListByTreatmentBMPTypeIDWithChangeTracking(_dbContext, treatmentBMPType.TreatmentBMPTypeID);
            viewModel.UpdateModel(new List<IHaveASortOrder>(treatmentBMPTypeAssessmentObservationTypes));
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Successfully Updated Observation Type Sort Order");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet("{treatmentBMPTypePrimaryKey}/{attributeTypePurposeID}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPTypePrimaryKey")]
        public PartialViewResult EditAttributeTypesSortOrder([FromRoute] TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey, [FromRoute] int attributeTypePurposeID)
        {
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMPTypePrimaryKey);
            var viewModel = new EditSortOrderViewModel();
            return ViewEditAttributeTypesSortOrder(treatmentBMPType, viewModel, attributeTypePurposeID);
        }

        private PartialViewResult ViewEditAttributeTypesSortOrder(TreatmentBMPType treatmentBMPType, EditSortOrderViewModel viewModel, int attributeTypePurposeID)
        {
            var haveASortOrders = new List<IHaveASortOrder>(treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Where(x=>x.CustomAttributeType.CustomAttributeTypePurposeID == attributeTypePurposeID));
            var viewData = new EditSortOrderViewData(haveASortOrders, "Attribute Types");
            return RazorPartialView<EditSortOrder, EditSortOrderViewData, EditSortOrderViewModel>(viewData, viewModel);
        }

        [HttpPost("{treatmentBMPTypePrimaryKey}/{attributeTypePurposeID}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPTypePrimaryKey")]
        public async Task<IActionResult> EditAttributeTypesSortOrder([FromRoute] TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey, [FromRoute] int attributeTypePurposeID, EditSortOrderViewModel viewModel)
        {
            var treatmentBMPType = TreatmentBMPTypes.GetByIDWithChangeTracking(_dbContext, treatmentBMPTypePrimaryKey);
            if (!ModelState.IsValid)
            {
                return ViewEditAttributeTypesSortOrder(treatmentBMPType, viewModel, attributeTypePurposeID);
            }

            viewModel.UpdateModel(new List<IHaveASortOrder>(treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes));
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Successfully Updated Attribute Type Sort Order");
            return new ModalDialogFormJsonResult();
        }
    }
}
