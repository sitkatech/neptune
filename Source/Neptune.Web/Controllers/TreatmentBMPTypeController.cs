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
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Models;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.SortOrder;
using Neptune.Web.Views.TreatmentBMPType;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPTypeController : NeptuneBaseController
    {
        [NeptuneAdminFeature]
        public ViewResult Manage()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageObservationTypesList);
            var viewData = new ManageViewData(CurrentPerson, neptunePage);
            return RazorView<Manage, ManageViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<TreatmentBMPType> TreatmentBMPTypeGridJsonData()
        {
            var gridSpec = new TreatmentBMPTypeGridSpec(CurrentPerson);
            var treatmentBMPTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.ToList().OrderBy(x => x.TreatmentBMPTypeName).ToList();
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
            var treatmentBMPType = new TreatmentBMPType(viewModel.TreatmentBMPTypeName, viewModel.TreatmentBMPTypeDescription);
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypes.Add(treatmentBMPType);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeAssessmentObservationTypes.Load();
            var treatmentBMPTypeAssessmentObservationTypes = new List<TreatmentBMPTypeAssessmentObservationType>();
            var allTreatmentBMPTypeAssessmentObservationTypes = HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeAssessmentObservationTypes.Local;

            HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeCustomAttributeTypes.Load();
            var treatmentBMPTypeAttributeTypes = new List<TreatmentBMPTypeCustomAttributeType>();
            var allTreatmentBMPTypeCustomAttributeTypes = HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeCustomAttributeTypes.Local;

            viewModel.UpdateModel(treatmentBMPType, treatmentBMPTypeAssessmentObservationTypes, allTreatmentBMPTypeAssessmentObservationTypes, treatmentBMPTypeAttributeTypes, allTreatmentBMPTypeCustomAttributeTypes);
           
            SetMessageForDisplay($"Treatment BMP Type {treatmentBMPType.TreatmentBMPTypeName} succesfully created.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPTypeController>(c => c.Detail(treatmentBMPType.PrimaryKey)));
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Edit(TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(treatmentBMPType);
            return ViewEdit(viewModel, treatmentBMPType);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey, EditViewModel viewModel)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, treatmentBMPType);
            }

            HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeAssessmentObservationTypes.Load();
            var treatmentBMPTypeAssessmentObservationTypes = treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.ToList();
            var allTreatmentBMPTypeAssessmentObservationTypes = HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeAssessmentObservationTypes.Local;

            HttpRequestStorage.DatabaseEntities.TreatmentBMPTypeCustomAttributeTypes.Load();
            var treatmentBMPTypeAttributeTypes = treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.ToList();
            var allTreatmentBMPTypeCustomAttributeTypes = HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeCustomAttributeTypes.Local;

            viewModel.UpdateModel(treatmentBMPType, treatmentBMPTypeAssessmentObservationTypes, allTreatmentBMPTypeAssessmentObservationTypes, treatmentBMPTypeAttributeTypes, allTreatmentBMPTypeCustomAttributeTypes);

            return RedirectToAction(new SitkaRoute<TreatmentBMPTypeController>(c => c.Detail(treatmentBMPType.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel, TreatmentBMPType treatmentBMPType)
        {
            var instructionsNeptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManageTreatmentBMPTypeInstructions);
            var submitUrl = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.TreatmentBMPTypeID) ? SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(x => x.Edit(viewModel.TreatmentBMPTypeID)) : SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(x => x.New());
            var observationTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes.ToList();
            var customAttributeTypes = HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.ToList();
            var viewData = new EditViewData(CurrentPerson, observationTypes, submitUrl, instructionsNeptunePage, treatmentBMPType, customAttributeTypes);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var treatmentBMPTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.ToList();
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TreatmentBMPType);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, treatmentBMPTypes);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public ViewResult Detail(TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, treatmentBMPType);
            return RazorView<Detail, DetailViewData>(viewData);
        }


        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult DeleteTreatmentBMPType(TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMPType.TreatmentBMPTypeID);
            return ViewDeleteTreatmentBMPType(treatmentBMPType, viewModel);
        }

        private PartialViewResult ViewDeleteTreatmentBMPType(TreatmentBMPType treatmentBMPType, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPLabel = treatmentBMPType.TreatmentBMPs.Count == 1 ? FieldDefinition.TreatmentBMP.GetFieldDefinitionLabel() : FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized();
            var confirmMessage = $"{FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabel()} '{treatmentBMPType.TreatmentBMPTypeName}' has {treatmentBMPType.TreatmentBMPs.Count} {treatmentBMPLabel}.<br /><br />Are you sure you want to delete this {FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabel()}?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteTreatmentBMPType(TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentBMPType(treatmentBMPType, viewModel);
            }

            var message = $"{FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabel()} '{treatmentBMPType.TreatmentBMPTypeName}' successfully deleted!";
            treatmentBMPType.DeleteFull();
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [NeptuneAdminFeature]
        public PartialViewResult EditObservationTypesSortOrder(TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            EditSortOrderViewModel viewModel = new EditSortOrderViewModel();
            return ViewEditObservationTypesSortOrder(treatmentBMPType, viewModel);
        }

        private PartialViewResult ViewEditObservationTypesSortOrder(TreatmentBMPType treatmentBMPType, EditSortOrderViewModel viewModel)
        {
            EditSortOrderViewData viewData = new EditSortOrderViewData(new List<IHaveASortOrder>(treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes), "Observation Types");
            return RazorPartialView<EditSortOrder, EditSortOrderViewData, EditSortOrderViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditObservationTypesSortOrder(TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey, EditSortOrderViewModel viewModel)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditObservationTypesSortOrder(treatmentBMPType, viewModel);
            }

            viewModel.UpdateModel(new List<IHaveASortOrder>(treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes));
            SetMessageForDisplay("Successfully Updated Observation Type Sort Order");
            return new ModalDialogFormJsonResult();
        }

        [NeptuneAdminFeature]
        public PartialViewResult EditAttributeTypesSortOrder(TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            EditSortOrderViewModel viewModel = new EditSortOrderViewModel();
            return ViewEditAttributeTypesSortOrder(treatmentBMPType, viewModel);
        }

        private PartialViewResult ViewEditAttributeTypesSortOrder(TreatmentBMPType treatmentBMPType, EditSortOrderViewModel viewModel)
        {
            EditSortOrderViewData viewData = new EditSortOrderViewData(new List<IHaveASortOrder>(treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes), "Attribute Types");
            return RazorPartialView<EditSortOrder, EditSortOrderViewData, EditSortOrderViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditAttributeTypesSortOrder(TreatmentBMPTypePrimaryKey treatmentBMPTypePrimaryKey, EditSortOrderViewModel viewModel)
        {
            var treatmentBMPType = treatmentBMPTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditAttributeTypesSortOrder(treatmentBMPType, viewModel);
            }

            viewModel.UpdateModel(new List<IHaveASortOrder>(treatmentBMPType.TreatmentBMPTypeCustomAttributeTypes));
            SetMessageForDisplay("Successfully Updated Attribute Type Sort Order");
            return new ModalDialogFormJsonResult();
        }
    }
}
