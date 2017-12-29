﻿/*-----------------------------------------------------------------------
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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.ObservationType;
using Neptune.Web.Views.Shared;
using IndexGridSpec = Neptune.Web.Views.ObservationType.IndexGridSpec;

namespace Neptune.Web.Controllers
{
    public class ObservationTypeController : NeptuneBaseController
    {
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ObservationTypes);
            var viewData = new IndexViewData(CurrentPerson, neptunePage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<ObservationType> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var observationTypes = HttpRequestStorage.DatabaseEntities.ObservationTypes.ToList().OrderBy(x => x.ObservationTypeName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ObservationType>(observationTypes, gridSpec);
            return gridJsonNetJObjectResult;
        }


        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditViewModel();
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            var observationType = new ObservationType(String.Empty, MeasurementUnitType.Feet, String.Empty, ObservationTypeSpecification.MultipleTimeValue_HighTargetValue_Absolute);
            viewModel.UpdateModel(observationType, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.AllObservationTypes.Add(observationType);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"Observation Type {observationType.ObservationTypeName} succesfully created.");

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult Edit(ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var observationType = observationTypePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(observationType);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(ObservationTypePrimaryKey observationTypePrimaryKey, EditViewModel viewModel)
        {
            var observationType = observationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            viewModel.UpdateModel(observationType, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var measurementUnitTypesAsSelectListItems = MeasurementUnitType.All
                .OrderBy(x => x.MeasurementUnitTypeDisplayName)
                .ToSelectListWithEmptyFirstRow(x => x.MeasurementUnitTypeID.ToString(CultureInfo.InvariantCulture),
                    x => x.MeasurementUnitTypeDisplayName);

            var observationTypeSpecificationsAsSelectListItems = ObservationTypeSpecification.All
                .OrderBy(x => x.SortOrder)
                .ToSelectListWithEmptyFirstRow(x => x.ObservationTypeSpecificationID.ToString(CultureInfo.InvariantCulture),
                    x => x.ObservationTypeSpecificationDisplayName);

            var viewData = new EditViewData(measurementUnitTypesAsSelectListItems, observationTypeSpecificationsAsSelectListItems);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [NeptuneAdminFeature]
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
            var canDelete = !observationType.HasDependentObjects();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this {FieldDefinition.ObservationType.GetFieldDefinitionLabel()} '{observationType.ObservationTypeName}'?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"{FieldDefinition.ObservationType.GetFieldDefinitionLabel()}", SitkaRoute<ObservationTypeController>.BuildLinkFromExpression(x => x.Detail(observationType), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
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
            observationType.DeleteObservationType();
            return new ModalDialogFormJsonResult();
        }
    }
}
