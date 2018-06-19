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
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Microsoft.Ajax.Utilities;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.FieldVisit;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.EditAttributes;
using Neptune.Web.Views.Shared.Location;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;
using Neptune.Web.Views.Shared.SortOrder;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FieldVisitSection = Neptune.Web.Models.FieldVisitSection;

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
            var fieldVisits = GetFieldVisitsAndGridSpec(out var gridSpec, CurrentPerson, treatmentBMP, true);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FieldVisit>(fieldVisits, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [FieldVisitViewFeature]
        public GridJsonNetJObjectResult<FieldVisit> AllFieldVisitsGridJsonData()
        {
            var fieldVisits = GetFieldVisitsAndGridSpec(out var gridSpec, CurrentPerson, null, false);
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
        /// <param name="detailPage"></param>
        /// <returns></returns>
        private List<FieldVisit> GetFieldVisitsAndGridSpec(out FieldVisitGridSpec gridSpec, Person currentPerson,
            TreatmentBMP treatmentBMP, bool detailPage)
        {
            gridSpec = new FieldVisitGridSpec(currentPerson, detailPage);
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
            var viewModel = new NewFieldVisitViewModel(treatmentBMP.InProgressFieldVisit);
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
            var fieldVisitType = FieldVisitType.AllLookupDictionary[viewModel.FieldVisitTypeID.GetValueOrDefault()];
            if (viewModel.Continue == null)
            {
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, DateTime.Now, false, fieldVisitType);
                HttpRequestStorage.DatabaseEntities.AllFieldVisits.Add(fieldVisit);
            }
            else if (viewModel.Continue == false)
            {
                var oldFieldVisit = treatmentBMP.InProgressFieldVisit;
                oldFieldVisit.FieldVisitStatusID = FieldVisitStatus.Unresolved.FieldVisitStatusID;
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, DateTime.Now, false, fieldVisitType);
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
            var viewModel = new LocationViewModel(fieldVisit);

            return ViewLocation(fieldVisit, viewModel);
        }

        private ViewResult ViewLocation(FieldVisit fieldVisit, LocationViewModel viewModel)
        {
            var treatmentBMP = fieldVisit.TreatmentBMP;
            var layerGeoJsons = MapInitJson.GetJurisdictionMapLayers();
            var boundingBox = treatmentBMP?.LocationPoint != null
                ? new BoundingBox(treatmentBMP.LocationPoint)
                : BoundingBox.MakeNewDefaultBoundingBox();
            var mapInitJson =
                new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", 10, layerGeoJsons, boundingBox, false)
                {
                    AllowFullScreen = false
                };
            var editLocationViewData = new EditLocationViewData(CurrentPerson, treatmentBMP, mapInitJson, "treatmentBMPLocation");
            var viewData = new LocationViewData(CurrentPerson, fieldVisit, editLocationViewData);

            return RazorView<Location, LocationViewData, LocationViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Location(FieldVisitPrimaryKey fieldVisitPrimaryKey, LocationViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;

            if (!ModelState.IsValid)
            {
                return ViewLocation(fieldVisit, viewModel);
            }

            viewModel.UpdateModel(fieldVisit.TreatmentBMP, CurrentPerson);
            fieldVisit.InventoryUpdated = true;

            SetMessageForDisplay("Successfully updated Treatment BMP Location.");

            return viewModel.AutoAdvance
                ? new RedirectResult(
                    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x =>
                        x.Photos(fieldVisitPrimaryKey)))
                : new RedirectResult(
                    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x =>
                        x.Location(fieldVisitPrimaryKey)));
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Photos(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewModel = new PhotosViewModel(fieldVisit.TreatmentBMP);
            return ViewPhotos(fieldVisit, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Photos(FieldVisitPrimaryKey fieldVisitPrimaryKey, PhotosViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                ViewPhotos(fieldVisit, viewModel);
            }

            viewModel.UpdateModels(CurrentPerson, fieldVisit.TreatmentBMP);
            SetMessageForDisplay("Successfully updated treatment BMP assessment photos.");

            return viewModel.AutoAdvance
                ? new RedirectResult(
                    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x =>
                        x.Attributes(fieldVisitPrimaryKey)))
                : new RedirectResult(
                    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x =>
                        x.Photos(fieldVisitPrimaryKey)));
        }

        private ViewResult ViewPhotos(FieldVisit fieldVisit, PhotosViewModel viewModel)
        {
            var managePhotosWithPreviewViewData = new ManagePhotosWithPreviewViewData(CurrentPerson, fieldVisit.TreatmentBMP);
            var viewData = new PhotosViewData(CurrentPerson, fieldVisit, managePhotosWithPreviewViewData);
            return RazorView<Photos, PhotosViewData, PhotosViewModel>(viewData, viewModel);
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
            var missingRequiredAttributes = fieldVisit.TreatmentBMP.RequiredAttributeDoesNotHaveValue(fieldVisit);
            EditAttributesViewData editAttributesViewData =
                new EditAttributesViewData(CurrentPerson, fieldVisit, true, missingRequiredAttributes);
            var viewData = new AttributesViewData(CurrentPerson, fieldVisit, editAttributesViewData);
            return RazorView<Attributes, AttributesViewData, AttributesViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Attributes(FieldVisitPrimaryKey fieldVisitPrimaryKey, AttributesViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;

            if (!ModelState.IsValid) {
                return ViewAttributes(fieldVisit, viewModel);
            }

            viewModel.UpdateModel(fieldVisit, CurrentPerson);
            fieldVisit.InventoryUpdated = true;

            SetMessageForDisplay("Successfully updated Treatment BMP Attributes.");

            return viewModel.AutoAdvance
                ? new RedirectResult(
                    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x =>
                        x.Assessment(fieldVisitPrimaryKey)))
                : new RedirectResult(
                    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x =>
                        x.Attributes(fieldVisitPrimaryKey)));
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
            var viewModel = new MaintainViewModel();
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
                maintenanceRecord = new MaintenanceRecord(fieldVisit.TreatmentBMPID);
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
            return ViewEditMaintenanceRecord(viewModel, maintenanceRecord.TreatmentBMP, false, fieldVisit, maintenanceRecord);
        }

        private ViewResult ViewEditMaintenanceRecord(EditMaintenanceRecordViewModel viewModel,
            TreatmentBMP treatmentBMP, bool isNew,
            FieldVisit fieldVisit, MaintenanceRecord maintenanceRecord)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.OrderBy(x => x.OrganizationShortName)
                .ToList();
            var missingRequiredAttributes = maintenanceRecord.IsMissingRequiredAttributes;
            var editMaintenanceRecordObservationsViewData = new EditMaintenanceRecordObservationsViewData(CurrentPerson,
                fieldVisit.TreatmentBMP, CustomAttributeTypePurpose.Maintenance, fieldVisit.MaintenanceRecord, true,
                missingRequiredAttributes);
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
                return ViewEditMaintenanceRecord(viewModel, fieldVisit.TreatmentBMP, false, fieldVisit, fieldVisit.MaintenanceRecord);
            }

            viewModel.UpdateModel(fieldVisit, HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.ToList());

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

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(x => x.FindABMP()));
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
                ? Redirect(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c => c.AssessmentPhotos(fieldVisit, (int) fieldVisitAssessmentType)))
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
        #region Assessment Photos

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult AssessmentPhotos(FieldVisitPrimaryKey fieldVisitPrimaryKey, int fieldVisitAssessmentTypeID)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var fieldVisitAssessmentType = (FieldVisitAssessmentType)fieldVisitAssessmentTypeID;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
            var viewModel = new AssessmentPhotosViewModel(treatmentBMPAssessment);
            return ViewAssessmentPhotos(treatmentBMPAssessment, fieldVisitAssessmentType, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult AssessmentPhotos(FieldVisitPrimaryKey fieldVisitPrimaryKey, int fieldVisitAssessmentTypeID,
            AssessmentPhotosViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
            if (!ModelState.IsValid)
            {
                return ViewAssessmentPhotos(treatmentBMPAssessment, fieldVisitAssessmentType, viewModel);
            }

            viewModel.UpdateModels(CurrentPerson, treatmentBMPAssessment);
            SetMessageForDisplay("Successfully updated treatment BMP assessment photos.");
            
            return viewModel.AutoAdvance
                ? (fieldVisitAssessmentType == FieldVisitAssessmentType.Initial
                    ? RedirectToAction(new SitkaRoute<FieldVisitController>(x => x.Maintain(fieldVisit)))
                    : RedirectToAction(new SitkaRoute<FieldVisitController>(x => x.WrapUpVisit(fieldVisit))))
                : Redirect(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(c => c.AssessmentPhotos(fieldVisit, fieldVisitAssessmentTypeID)));
        }

        private ViewResult ViewAssessmentPhotos(TreatmentBMPAssessment treatmentBMPAssessment, FieldVisitAssessmentType fieldVisitAssessmentType, AssessmentPhotosViewModel viewModel)
        {
            var fieldVisitSection = fieldVisitAssessmentType == FieldVisitAssessmentType.Initial
                ? (FieldVisitSection) FieldVisitSection.Assessment
                : FieldVisitSection.PostMaintenanceAssessment;

            var managePhotosWithPreviewViewData = new ManagePhotosWithPreviewViewData(CurrentPerson, treatmentBMPAssessment);

            var viewData = new AssessmentPhotosViewData(CurrentPerson, treatmentBMPAssessment, fieldVisitSection, managePhotosWithPreviewViewData);
            return RazorView<AssessmentPhotos, AssessmentPhotosViewData, AssessmentPhotosViewModel>(viewData, viewModel);
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
        public ActionResult PreviewObservationType(Views.TreatmentBMPAssessmentObservationType.EditViewModel viewModel)
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

            PartialViewResult result;
            var treatmentBmpAssessment = new TreatmentBMPAssessment(ModelObjectHelpers.NotYetAssignedID,
                ModelObjectHelpers.NotYetAssignedID,
                ModelObjectHelpers.NotYetAssignedID, null);
            var observationTypeCollectionMethod = ObservationTypeCollectionMethod.All.Single(x => x.ObservationTypeCollectionMethodID == viewModel.ObservationTypeCollectionMethodID);
            var observationTypeSpecification = ObservationTypeSpecification.All.Single(x =>
                x.ObservationTargetTypeID == viewModel.ObservationTargetTypeID &&
                x.ObservationThresholdTypeID == viewModel.ObservationThresholdTypeID &&
                x.ObservationTypeCollectionMethodID == viewModel.ObservationTypeCollectionMethodID);
            var treatmentBMPAssessmentObservationType = new TreatmentBMPAssessmentObservationType(viewModel.TreatmentBMPAssessmentObservationTypeName, observationTypeSpecification, viewModel.TreatmentBMPAssessmentObservationTypeSchema);
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    var discreteCollectionMethodViewModel = new DiscreteCollectionMethodViewModel();
                    var discreteCollectionMethodViewData = new DiscreteCollectionMethodViewData(treatmentBmpAssessment, treatmentBMPAssessmentObservationType, CurrentPerson);
                    result =
                        RazorPartialView<DiscreteCollectionMethodPartial, DiscreteCollectionMethodViewData,
                            DiscreteCollectionMethodViewModel>(discreteCollectionMethodViewData,
                            discreteCollectionMethodViewModel);
                    break;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    var passFailCollectionMethodViewModel = new PassFailCollectionMethodViewModel();
                    var passFailCollectionMethodViewData = new PassFailCollectionMethodViewData(treatmentBmpAssessment, treatmentBMPAssessmentObservationType, CurrentPerson);
                    result = RazorPartialView<PassFailCollectionMethodPartial, PassFailCollectionMethodViewData, PassFailCollectionMethodViewModel>(passFailCollectionMethodViewData, passFailCollectionMethodViewModel);
                    break;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    var percentageCollectionMethodViewModel = new PercentageCollectionMethodViewModel();
                    var percentageCollectionMethodViewData = new PercentageCollectionMethodViewData(treatmentBmpAssessment, treatmentBMPAssessmentObservationType, CurrentPerson);
                    result = RazorPartialView<PercentageCollectionMethodPartial, PercentageCollectionMethodViewData, PercentageCollectionMethodViewModel>(percentageCollectionMethodViewData, percentageCollectionMethodViewModel);
                    break;
                case ObservationTypeCollectionMethodEnum.Rate:
                    var rateCollectionMethodViewModel = new RateCollectionMethodViewModel();
                    var rateCollectionMethodViewData = new RateCollectionMethodViewData(treatmentBmpAssessment, treatmentBMPAssessmentObservationType, CurrentPerson);
                    // TODO: Do all like this
                    result = RazorPartialView<RateCollectionMethodPartial, RateCollectionMethodViewData, RateCollectionMethodViewModel>(rateCollectionMethodViewData, rateCollectionMethodViewModel);
                    break;
                default:
                    throw new ArgumentException($"Observation Collection Method {observationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName} not supported by Observation Type Preview.");
            }

            return result;
        }

    }

    public enum FieldVisitAssessmentType
    {
        Initial,
        PostMaintenance
    }
}