/*-----------------------------------------------------------------------
<copyright file="FieldVisitController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Microsoft.Ajax.Utilities;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.FieldVisit;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.EditAttributes;
using Neptune.Web.Views.Shared.SortOrder;

namespace Neptune.Web.Controllers
{
    public class FieldVisitController : NeptuneBaseController
    {
        [HttpGet]
        [FieldVisitViewFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.FieldRecords);
            var maintenanceAttributeTypes =
                HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.Where(x=>x.CustomAttributeTypePurposeID == CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID);
            var viewData = new IndexViewData(CurrentPerson, neptunePage, maintenanceAttributeTypes, HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [FieldVisitViewFeature]
        public GridJsonNetJObjectResult<FieldVisit> FieldVisitGridJsonData(
            TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey?.EntityObject;
            var fieldVisits = GetFieldVisitsAndGridSpec(out var gridSpec, CurrentPerson, treatmentBMP);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FieldVisit>(fieldVisits, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [FieldVisitViewFeature]
        public GridJsonNetJObjectResult<FieldVisit> AllFieldVisitsGridJsonData()
        {
            var fieldVisits = GetFieldVisitsAndGridSpec(out var gridSpec, CurrentPerson, null);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FieldVisit>(fieldVisits, gridSpec);

            return gridJsonNetJObjectResult;
        }

        /// <summary>
        /// Gets the Field Visits for a given Treatment BMP and out-returns the appropriate grid spec.
        /// If treatmentBMP is null, returns all Field Visits 
        /// </summary>
        /// <param name="gridSpec"></param>
        /// <param name="currentPerson"></param>
        /// <param name="treatmentBMP"></param>
        /// <returns></returns>
        private List<FieldVisit> GetFieldVisitsAndGridSpec(out FieldVisitGridSpec gridSpec, Person currentPerson,
            TreatmentBMP treatmentBMP)
        {
            gridSpec = new FieldVisitGridSpec(currentPerson);
            var fieldVisits = HttpRequestStorage.DatabaseEntities.FieldVisits;
            return (treatmentBMP != null
                ? fieldVisits.Where(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID)
                : fieldVisits).ToList();
        }

        [HttpGet]
        [FieldVisitCreateFeature]
        public PartialViewResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new NewFieldVisitViewModel(treatmentBMP.InProgressFieldVisit != null);
            return ViewNew(treatmentBMP, viewModel);
        }

        private PartialViewResult ViewNew(TreatmentBMP treatmentBMP, NewFieldVisitViewModel viewModel)
        {
            var viewData = new NewFieldVisitViewData(treatmentBMP);
            return RazorPartialView<NewFieldVisit, NewFieldVisitViewData, NewFieldVisitViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, NewFieldVisitViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNew(treatmentBMP, viewModel);
            }

            FieldVisit fieldVisit;
            if (viewModel.Continue == null)
            {
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, DateTime.Now, false);
                HttpRequestStorage.DatabaseEntities.AllFieldVisits.Add(fieldVisit);
            }
            else if (viewModel.Continue == false)
            {
                var oldFieldVisit = treatmentBMP.InProgressFieldVisit;
                oldFieldVisit.FieldVisitStatusID = FieldVisitStatus.Unresolved.FieldVisitStatusID;
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, DateTime.Now, false);
            }
            else // if Continue == true
            {
                fieldVisit = treatmentBMP.InProgressFieldVisit;
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult(
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Inventory(fieldVisit)));
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Inventory(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewData = new InventoryViewData(CurrentPerson, fieldVisit);
            return RazorView<Inventory, InventoryViewData>(viewData);
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Location(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewData = new LocationViewData(CurrentPerson, fieldVisit);
            return RazorView<Location, LocationViewData>(viewData);
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Photos(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewData = new PhotosViewData(CurrentPerson, fieldVisit);
            return RazorView<Photos, PhotosViewData>(viewData);
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Attributes(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewModel = new AttributesViewModel(fieldVisit);
            return ViewAttributes(fieldVisit, viewModel);
        }

        private ViewResult ViewAttributes(FieldVisit fieldVisit, AttributesViewModel viewModel)
        {
            EditAttributesViewData editAttributesViewData =
                new EditAttributesViewData(CurrentPerson, fieldVisit, true);
            var viewData = new AttributesViewData(CurrentPerson, fieldVisit, editAttributesViewData);
            return RazorView<Attributes, AttributesViewData, AttributesViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ViewResult Attributes(FieldVisitPrimaryKey fieldVisitPrimaryKey, AttributesViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;

            if (!ModelState.IsValid) {
                return ViewAttributes(fieldVisit, viewModel);
            }

            viewModel.UpdateModel(fieldVisit, CurrentPerson);
            fieldVisit.InventoryUpdated = true;

            return ViewAttributes(fieldVisit, viewModel);
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Assessment(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewData = new AssessmentViewData(CurrentPerson, fieldVisit);
            return RazorView<Assessment, AssessmentViewData>(viewData);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        public ActionResult Assessment(FieldVisitPrimaryKey fieldVisitPrimaryKey, AssessmentViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(FieldVisitAssessmentType.Initial);
            if (treatmentBMPAssessment == null)
            {
                treatmentBMPAssessment = CreatePlaceholderTreatmentBMPAssessment(fieldVisit.TreatmentBMP);
                SaveNewAssessmentToFieldVisit(treatmentBMPAssessment,fieldVisit,FieldVisitAssessmentType.Initial);
                return GetNextObservationTypeViewResult(fieldVisit, null, FieldVisitAssessmentType.Initial);
            }
            else
            {

                return GetNextObservationTypeViewResult(fieldVisit, null, FieldVisitAssessmentType.Initial);
            }
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Maintain(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewModel = new MaintainViewModel(fieldVisit);
            return ViewMaintain(fieldVisit, viewModel);
        }

        private ViewResult ViewMaintain(FieldVisit fieldVisit, MaintainViewModel viewModel)
        {
            var allMaintenanceRecordTypes = MaintenanceRecordType.All.ToSelectListWithDisabledEmptyFirstRow(
                x => x.MaintenanceRecordTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.MaintenanceRecordTypeDisplayName, "Choose a type");
            var viewData = new MaintainViewData(CurrentPerson, fieldVisit, allMaintenanceRecordTypes);
            return RazorView<Maintain, MaintainViewData, MaintainViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Maintain(FieldVisitPrimaryKey fieldVisitPrimaryKey, MaintainViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var maintenanceRecord = fieldVisit.MaintenanceRecord;

            if (!ModelState.IsValid)
            {
                return ViewMaintain(fieldVisit, viewModel);
            }

            if (maintenanceRecord == null)
            {
                maintenanceRecord = new MaintenanceRecord(fieldVisit.TreatmentBMPID, viewModel.MaintenanceRecordTypeID.GetValueOrDefault());
                HttpRequestStorage.DatabaseEntities.AllMaintenanceRecords.Add(maintenanceRecord);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                fieldVisit.MaintenanceRecordID = maintenanceRecord.MaintenanceRecordID;
            }
            return RedirectToAction(new SitkaRoute<FieldVisitController>(x => x.EditMaintenanceRecord(fieldVisitPrimaryKey)));
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult EditMaintenanceRecord(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var maintenanceRecord = fieldVisit.MaintenanceRecord;
            var viewModel = new EditMaintenanceRecordViewModel(maintenanceRecord);
            return ViewEditMaintenanceRecord(viewModel, maintenanceRecord.TreatmentBMP, false, fieldVisit);
        }

        private ViewResult ViewEditMaintenanceRecord(EditMaintenanceRecordViewModel viewModel, TreatmentBMP treatmentBMP, bool isNew,
            FieldVisit fieldVisit)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.OrderBy(x => x.OrganizationShortName)
                .ToList();
            var editMaintenanceRecordObservationsViewData = new EditMaintenanceRecordObservationsViewData(CurrentPerson,fieldVisit.TreatmentBMP,CustomAttributeTypePurpose.Maintenance, fieldVisit.MaintenanceRecord, true);
            var viewData = new EditMaintenanceRecordViewData(CurrentPerson, organizations, treatmentBMP, isNew, fieldVisit, editMaintenanceRecordObservationsViewData);
            return RazorView<EditMaintenanceRecord, EditMaintenanceRecordViewData,
                EditMaintenanceRecordViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditMaintenanceRecord(FieldVisitPrimaryKey fieldVisitPrimaryKey,
            EditMaintenanceRecordViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewEditMaintenanceRecord(viewModel, fieldVisit.TreatmentBMP, false, fieldVisit);
            }

            viewModel.UpdateModel(fieldVisit);

            SetMessageForDisplay($"{FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()} successfully updated.");

            return viewModel.AutoAdvance
                ? new RedirectResult(
                    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x =>
                        x.PostMaintenanceAssessment(fieldVisitPrimaryKey)))
                : new RedirectResult(
                    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x =>
                        x.EditMaintenanceRecord(fieldVisitPrimaryKey)));
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult PostMaintenanceAssessment(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewData = new PostMaintenanceAssessmentViewData(CurrentPerson, fieldVisit);
            return RazorView<PostMaintenanceAssessment, PostMaintenanceAssessmentViewData>(viewData);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        public ActionResult PostMaintenanceAssessment(FieldVisitPrimaryKey fieldVisitPrimaryKey, PostMaintenanceAssessmentViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(FieldVisitAssessmentType.PostMaintenance);
            if (treatmentBMPAssessment == null)
            {
                treatmentBMPAssessment = CreatePlaceholderTreatmentBMPAssessment(fieldVisit.TreatmentBMP);
                SaveNewAssessmentToFieldVisit(treatmentBMPAssessment, fieldVisit, FieldVisitAssessmentType.PostMaintenance);
                return GetNextObservationTypeViewResult(fieldVisit, null, FieldVisitAssessmentType.PostMaintenance);
            }
            else
            {

                return GetNextObservationTypeViewResult(fieldVisit, null, FieldVisitAssessmentType.PostMaintenance);
            }
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult WrapUpVisit(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewData = new WrapUpVisitViewData(CurrentPerson, fieldVisit);
            return RazorView<WrapUpVisit, WrapUpVisitViewData, WrapUpVisitViewModel>(viewData, new WrapUpVisitViewModel());
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult WrapUpVisit(FieldVisitPrimaryKey fieldVisitPrimaryKey, WrapUpVisitViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewData = new WrapUpVisitViewData(CurrentPerson, fieldVisit);
            if (!ModelState.IsValid)
            {
                return RazorView<WrapUpVisit, WrapUpVisitViewData, WrapUpVisitViewModel>(viewData, viewModel);
            }

            fieldVisit.FieldVisitStatusID = FieldVisitStatus.Complete.FieldVisitStatusID;

            SetMessageForDisplay($"Successfully completed the Field Visit for {fieldVisit.TreatmentBMP.GetDisplayNameAsUrl()}.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(x => x.Detail(fieldVisit.TreatmentBMP)));
        }

        #region Assessment-Related Actions
        #region Observation Types

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult DiscreteCollectionMethod(FieldVisitPrimaryKey fieldVisitPrimaryKey,
            TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey,
            int fieldVisitAssessmentTypeID)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().FirstOrDefault(x =>
                x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID ==
                treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            var viewModel =
                new DiscreteCollectionMethodViewModel(existingObservation, treatmentBMPAssessmentObservationType);
            var viewData = new DiscreteCollectionMethodViewData(fieldVisit, treatmentBMPAssessmentObservationType,
                fieldVisitAssessmentType, CurrentPerson);
            return RazorView<DiscreteCollectionMethod, DiscreteCollectionMethodViewData,
                DiscreteCollectionMethodViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DiscreteCollectionMethod(FieldVisitPrimaryKey fieldVisitPrimaryKey,
            TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey,
            int fieldVisitAssessmentTypeID, DiscreteCollectionMethodViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);

            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                var viewData = new DiscreteCollectionMethodViewData(fieldVisit, treatmentBMPAssessmentObservationType,
                    fieldVisitAssessmentType, CurrentPerson);
                return RazorView<DiscreteCollectionMethod, DiscreteCollectionMethodViewData,
                    DiscreteCollectionMethodViewModel>(viewData, viewModel);
            }

            var treatmentBMPObservation =
                GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMPAssessment,
                    treatmentBMPAssessmentObservationType);
            viewModel.UpdateModel(treatmentBMPObservation);
            SetMessageForDisplay("Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(fieldVisit, treatmentBMPAssessmentObservationType,
                    fieldVisitAssessmentType)
                : RedirectToAction(new SitkaRoute<FieldVisitController>(c =>
                    c.DiscreteCollectionMethod(fieldVisit, treatmentBMPAssessmentObservationType,
                        fieldVisitAssessmentTypeID)));
        }

        private static TreatmentBMPObservation GetExistingTreatmentBMPObservationOrCreateNew(
            TreatmentBMPAssessment treatmentBMPAssessment,
            TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList()
                .Find(x => x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID ==
                           treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            if (treatmentBMPObservation == null)
            {
                var treatmentBMPTypeAssessmentObservationType =
                    treatmentBMPAssessment.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.SingleOrDefault(
                        x =>
                            x.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationType
                                .TreatmentBMPAssessmentObservationTypeID);
                Check.RequireNotNull(treatmentBMPTypeAssessmentObservationType,
                    $"Not a valid Observation Type ID {treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID} for Treatment BMP Type ID {treatmentBMPAssessment.TreatmentBMPTypeID}");
                treatmentBMPObservation = new TreatmentBMPObservation(treatmentBMPAssessment,
                    treatmentBMPTypeAssessmentObservationType,
                    treatmentBMPAssessment.TreatmentBMPType, treatmentBMPAssessmentObservationType, string.Empty);
            }

            return treatmentBMPObservation;
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult RateCollectionMethod(FieldVisitPrimaryKey fieldVisitPrimaryKey,
            TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey,
            int fieldVisitAssessmentTypeID)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().FirstOrDefault(x =>
                x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID ==
                treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            var viewModel =
                new RateCollectionMethodViewModel(existingObservation, treatmentBMPAssessmentObservationType);
            var viewData = new RateCollectionMethodViewData(fieldVisit, treatmentBMPAssessmentObservationType,
                fieldVisitAssessmentType, CurrentPerson);
            return
                RazorView<RateCollectionMethod, RateCollectionMethodViewData, RateCollectionMethodViewModel>(
                    viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RateCollectionMethod(FieldVisitPrimaryKey fieldVisitPrimaryKey,
            TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey,
            int fieldVisitAssessmentTypeID, RateCollectionMethodViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                var viewData = new RateCollectionMethodViewData(fieldVisit, treatmentBMPAssessmentObservationType,
                    fieldVisitAssessmentType, CurrentPerson);
                return
                    RazorView<RateCollectionMethod, RateCollectionMethodViewData, RateCollectionMethodViewModel>(
                        viewData, viewModel);
            }

            var treatmentBMPObservation =
                GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMPAssessment,
                    treatmentBMPAssessmentObservationType);
            viewModel.UpdateModel(treatmentBMPObservation);

            SetMessageForDisplay("Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(fieldVisit, treatmentBMPAssessmentObservationType,
                    fieldVisitAssessmentType)
                : RedirectToAction(new SitkaRoute<FieldVisitController>(c =>
                    c.RateCollectionMethod(fieldVisit, treatmentBMPAssessmentObservationType,
                        fieldVisitAssessmentTypeID)));
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult PassFailCollectionMethod(FieldVisitPrimaryKey fieldVisitPrimaryKey,
            TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey,
            int fieldVisitAssessmentTypeID)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().FirstOrDefault(x =>
                x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID ==
                treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            var viewModel =
                new PassFailCollectionMethodViewModel(existingObservation, treatmentBMPAssessmentObservationType);
            var viewData = new PassFailCollectionMethodViewData(fieldVisit,
                treatmentBMPAssessmentObservationType, fieldVisitAssessmentType, CurrentPerson);
            return
                RazorView<PassFailCollectionMethod, PassFailCollectionMethodViewData, PassFailCollectionMethodViewModel
                >(
                    viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult PassFailCollectionMethod(FieldVisitPrimaryKey fieldVisitPrimaryKey,
            TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey,
            int fieldVisitAssessmentTypeID, PassFailCollectionMethodViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                var viewData = new PassFailCollectionMethodViewData(fieldVisit,
                    treatmentBMPAssessmentObservationType, fieldVisitAssessmentType, CurrentPerson);
                return
                    RazorView<PassFailCollectionMethod, PassFailCollectionMethodViewData,
                        PassFailCollectionMethodViewModel>(
                        viewData, viewModel);
            }

            var treatmentBMPObservation =
                GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMPAssessment,
                    treatmentBMPAssessmentObservationType);
            viewModel.UpdateModel(treatmentBMPObservation);

            SetMessageForDisplay("Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(fieldVisit, treatmentBMPAssessmentObservationType,
                    fieldVisitAssessmentType)
                : RedirectToAction(new SitkaRoute<FieldVisitController>(c =>
                    c.PassFailCollectionMethod(fieldVisit, treatmentBMPAssessmentObservationType,
                        fieldVisitAssessmentTypeID)));
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult PercentageCollectionMethod(FieldVisitPrimaryKey fieldVisitPrimaryKey,
            TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey,
            int fieldVisitAssessmentTypeID)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().FirstOrDefault(x =>
                x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID ==
                treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            var viewModel =
                new PercentageCollectionMethodViewModel(existingObservation, treatmentBMPAssessmentObservationType);
            var viewData = new PercentageCollectionMethodViewData(fieldVisit,
                treatmentBMPAssessmentObservationType, fieldVisitAssessmentType, CurrentPerson);
            return
                RazorView<PercentageCollectionMethod, PercentageCollectionMethodViewData,
                    PercentageCollectionMethodViewModel>(
                    viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult PercentageCollectionMethod(FieldVisitPrimaryKey fieldVisitPrimaryKey,
            TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey,
            int fieldVisitAssessmentTypeID, PercentageCollectionMethodViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                var viewData = new PercentageCollectionMethodViewData(fieldVisit,
                    treatmentBMPAssessmentObservationType, fieldVisitAssessmentType, CurrentPerson);
                return
                    RazorView<PercentageCollectionMethod, PercentageCollectionMethodViewData,
                        PercentageCollectionMethodViewModel>(
                        viewData, viewModel);
            }

            var treatmentBMPObservation =
                GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMPAssessment,
                    treatmentBMPAssessmentObservationType);
            viewModel.UpdateModel(treatmentBMPObservation);

            SetMessageForDisplay("Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(fieldVisit, treatmentBMPAssessmentObservationType,
                    fieldVisitAssessmentType)
                : RedirectToAction(new SitkaRoute<FieldVisitController>(c =>
                    c.PercentageCollectionMethod(fieldVisit, treatmentBMPAssessmentObservationType,
                        fieldVisitAssessmentTypeID)));
        }

        #endregion
        #region Helper methods for Assessment

        private TreatmentBMPAssessment CreatePlaceholderTreatmentBMPAssessment(TreatmentBMP treatmentBMP)
        {
            return new TreatmentBMPAssessment(treatmentBMP, treatmentBMP.TreatmentBMPType);
        }

        private RedirectResult GetNextObservationTypeViewResult(FieldVisit fieldVisit,
            TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType,
            FieldVisitAssessmentType fieldVisitAssessmentType)
        {
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);

            var orderedObservationTypes = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType
                .TreatmentBMPTypeAssessmentObservationTypes.SortByOrderThenName()
                .Select(x => x.TreatmentBMPAssessmentObservationType).ToList();

            var nextObservationType = treatmentBMPAssessmentObservationType == null
                ? orderedObservationTypes.First()
                : orderedObservationTypes.ElementAtOrDefault(
                    orderedObservationTypes.IndexOf(treatmentBMPAssessmentObservationType) + 1);
            var isLastPage = nextObservationType == null;

            var nextObservationTypeViewResult = isLastPage
                ? (fieldVisitAssessmentType == FieldVisitAssessmentType.Initial
                    ? RedirectToAction(new SitkaRoute<FieldVisitController>(x => x.Maintain(fieldVisit)))
                    : RedirectToAction(new SitkaRoute<FieldVisitController>(x => x.WrapUpVisit(fieldVisit))))
                : Redirect(nextObservationType.AssessmentUrl(fieldVisit, fieldVisitAssessmentType));
            return nextObservationTypeViewResult;
        }

        private static void SaveNewAssessmentToFieldVisit(TreatmentBMPAssessment treatmentBMPAssessment, FieldVisit fieldVisit,
            FieldVisitAssessmentType fieldVisitAssessmentType)
        {
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessments
                .AddOrUpdate(treatmentBMPAssessment); //todo - AddOrUpdate??
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            switch (fieldVisitAssessmentType)
            {
                case FieldVisitAssessmentType.Initial:
                    fieldVisit.InitialAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
                    break;
                case FieldVisitAssessmentType.PostMaintenance:
                    fieldVisit.PostMaintenanceAssessmentID = treatmentBMPAssessment.TreatmentBMPAssessmentID;
                    break;
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();
        }

        #endregion
        #endregion



        [HttpGet]
        [FieldVisitDeleteFeature]
        public PartialViewResult Delete(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(fieldVisit.FieldVisitID);
            return ViewDeleteFieldVisit(fieldVisit, viewModel);
        }

        [HttpPost]
        [FieldVisitDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(FieldVisitPrimaryKey fieldVisitPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFieldVisit(fieldVisit, viewModel);
            }

            fieldVisit.InitialAssessment?.DeleteFull();
            fieldVisit.MaintenanceRecord?.DeleteFull();
            fieldVisit.PostMaintenanceAssessment?.DeleteFull();
            fieldVisit.DeleteFieldVisit();
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay("Successfully deleted the field visit.");

            return new ModalDialogFormJsonResult(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c => c.Index()));
        }

        private PartialViewResult ViewDeleteFieldVisit(FieldVisit fieldVisit, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete the field visit from '{fieldVisit.VisitDate}'?{AssociatedFieldVisitEntitiesString(fieldVisit)}";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        private static string AssociatedFieldVisitEntitiesString(FieldVisit fieldVisit)
        {
            var entitiesSubstrings = new List<string>
            {
                (fieldVisit.InitialAssessment != null) ? "initial assessment" : null,
                fieldVisit.PostMaintenanceAssessment != null ? "post-maintenance assessment" : null,
                fieldVisit.MaintenanceRecord != null ? "maintenance record" : null
            };
            var entitiesConcatenated = string.Join(", ", entitiesSubstrings.Where(x => x != null));
            var lastComma = entitiesConcatenated.LastIndexOf(",",StringComparison.InvariantCulture);
            var associatedFieldVisitEntitiesString = lastComma > -1 ? entitiesConcatenated.Insert(lastComma + 1, " and") : entitiesConcatenated;

            return !associatedFieldVisitEntitiesString.IsNullOrWhiteSpace() ? $" This will delete the associated {associatedFieldVisitEntitiesString}." : "";
        }
    }

    public enum FieldVisitAssessmentType
    {
        Initial,
        PostMaintenance
    }
}