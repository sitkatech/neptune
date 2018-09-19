﻿/*-----------------------------------------------------------------------
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
using Neptune.Web.Views.Shared.Location;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;
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
        private static List<FieldVisit> GetFieldVisitsAndGridSpec(out FieldVisitGridSpec gridSpec, Person currentPerson,
            TreatmentBMP treatmentBMP, bool detailPage)
        {
            gridSpec = new FieldVisitGridSpec(currentPerson, detailPage);
            var fieldVisits = HttpRequestStorage.DatabaseEntities.FieldVisits.ToList().Where(x=> x.TreatmentBMP.CanView(currentPerson));
            return (treatmentBMP != null
                ? fieldVisits.Where(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID)
                : fieldVisits).ToList();
        }

        [HttpGet]
        [FieldVisitViewFeature]
        public ViewResult Detail(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var initialAssessmentViewData = new AssessmentDetailViewData(CurrentPerson, fieldVisit.GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.Initial), TreatmentBMPAssessmentTypeEnum.Initial);
            var postMaintenanceAssessmentViewData = new AssessmentDetailViewData(CurrentPerson, fieldVisit.GetAssessmentByType(TreatmentBMPAssessmentTypeEnum.PostMaintenance), TreatmentBMPAssessmentTypeEnum.PostMaintenance);
            var viewData = new DetailViewData(CurrentPerson, StormwaterBreadCrumbEntity.FieldVisits, fieldVisit, initialAssessmentViewData, postMaintenanceAssessmentViewData);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [FieldVisitCreateFeature]
        public PartialViewResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new NewFieldVisitViewModel(treatmentBMP.GetInProgressFieldVisit());
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
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, DateTime.Now, false, fieldVisitType, false);
                HttpRequestStorage.DatabaseEntities.AllFieldVisits.Add(fieldVisit);
            }
            else if (viewModel.Continue == false)
            {
                var oldFieldVisit = treatmentBMP.GetInProgressFieldVisit();
                oldFieldVisit.FieldVisitStatusID = FieldVisitStatus.Unresolved.FieldVisitStatusID;
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, DateTime.Now, false, fieldVisitType, false);
            }
            else // if Continue == true
            {
                fieldVisit = treatmentBMP.GetInProgressFieldVisit();
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

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Inventory(FieldVisitPrimaryKey fieldVisitPrimaryKey, InventoryViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            FinalizeVisitIfNecessary(viewModel, fieldVisit);
            return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.VisitSummary(fieldVisit)));
        }

        private static void FinalizeVisitIfNecessary(FieldVisitViewModel viewModel, FieldVisit fieldVisit)
        {
            if (viewModel.FinalizeVisit ?? false)
            {
                fieldVisit.FieldVisitStatusID = FieldVisitStatus.Complete.FieldVisitStatusID;
            }
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
            var layerGeoJsons = MapInitJsonHelpers.GetJurisdictionMapLayers().ToList();
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
            fieldVisit.TreatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            viewModel.UpdateModel(fieldVisit.TreatmentBMP, CurrentPerson);
            fieldVisit.InventoryUpdated = true;
            FinalizeVisitIfNecessary(viewModel, fieldVisit);

            SetMessageForDisplay("Successfully updated Treatment BMP Location.");

            return RedirectToNextStep(viewModel, new SitkaRoute<FieldVisitController>(c =>
                c.Location(fieldVisit)), new SitkaRoute<FieldVisitController>(c =>
                c.Photos(fieldVisit)), fieldVisit);
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
            fieldVisit.TreatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            fieldVisit.InventoryUpdated = true;
            FinalizeVisitIfNecessary(viewModel, fieldVisit);
            SetMessageForDisplay("Successfully updated treatment BMP assessment photos.");
            return RedirectToNextStep(viewModel, new SitkaRoute<FieldVisitController>(c => c.Photos(fieldVisit)), new SitkaRoute<FieldVisitController>(c => c.Attributes(fieldVisit)), fieldVisit);
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
            var editAttributesViewData = new EditAttributesViewData(CurrentPerson, fieldVisit, true, missingRequiredAttributes);
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
            fieldVisit.TreatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            fieldVisit.InventoryUpdated = true;
            FinalizeVisitIfNecessary(viewModel, fieldVisit);
            SetMessageForDisplay("Successfully updated Treatment BMP Attributes.");
            return RedirectToNextStep(viewModel, new SitkaRoute<FieldVisitController>(c =>
                c.Attributes(fieldVisit)), new SitkaRoute<FieldVisitController>(c =>
                c.Assessment(fieldVisit)), fieldVisit);
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
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Assessment(FieldVisitPrimaryKey fieldVisitPrimaryKey, FieldVisitViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            const TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum = TreatmentBMPAssessmentTypeEnum.Initial;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(treatmentBMPAssessmentTypeEnum) ?? CreatePlaceholderTreatmentBMPAssessment(fieldVisit, treatmentBMPAssessmentTypeEnum);
            FinalizeVisitIfNecessary(viewModel, fieldVisit);
            return viewModel.FinalizeVisit ?? false ? RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.VisitSummary(fieldVisit))) : RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Observations(fieldVisit, treatmentBMPAssessmentTypeEnum)));
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
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult PostMaintenanceAssessment(FieldVisitPrimaryKey fieldVisitPrimaryKey, FieldVisitViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            const TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum = TreatmentBMPAssessmentTypeEnum.PostMaintenance;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(treatmentBMPAssessmentTypeEnum) ?? CreatePlaceholderTreatmentBMPAssessment(fieldVisit, treatmentBMPAssessmentTypeEnum);
            FinalizeVisitIfNecessary(viewModel, fieldVisit);
            return viewModel.FinalizeVisit ?? false ? RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.VisitSummary(fieldVisit))) : RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Observations(fieldVisit, treatmentBMPAssessmentTypeEnum)));
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
            if (!ModelState.IsValid)
            {
                return ViewMaintain(fieldVisit, viewModel);
            }
            var maintenanceRecord = fieldVisit.GetMaintenanceRecord() ?? new MaintenanceRecord(fieldVisit.TreatmentBMP, fieldVisit.TreatmentBMP.TreatmentBMPTypeID, fieldVisit) { MaintenanceRecordTypeID = MaintenanceRecordType.Routine.MaintenanceRecordTypeID };
            FinalizeVisitIfNecessary(viewModel, fieldVisit);
            return viewModel.FinalizeVisit ?? false ? RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.VisitSummary(fieldVisit))) : RedirectToAction(new SitkaRoute<FieldVisitController>(x => x.EditMaintenanceRecord(fieldVisitPrimaryKey)));
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ActionResult EditMaintenanceRecord(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var maintenanceRecord = fieldVisit.GetMaintenanceRecord();
            // need this check to support deleting maintenance records from the edit page
            if (maintenanceRecord == null)
            {
                return Redirect(
                    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Maintain(fieldVisitPrimaryKey)));
            }
            var viewModel = new EditMaintenanceRecordViewModel(maintenanceRecord);
            return ViewEditMaintenanceRecord(viewModel, maintenanceRecord.TreatmentBMP, false, fieldVisit, maintenanceRecord);
        }

        private ViewResult ViewEditMaintenanceRecord(EditMaintenanceRecordViewModel viewModel, TreatmentBMP treatmentBMP, bool isNew, FieldVisit fieldVisit, MaintenanceRecord maintenanceRecord)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.OrderBy(x => x.OrganizationShortName)
                .ToList();
            var missingRequiredAttributes = maintenanceRecord.IsMissingRequiredAttributes();
            var editMaintenanceRecordObservationsViewData = new EditMaintenanceRecordObservationsViewData(CurrentPerson,
                fieldVisit.TreatmentBMP, CustomAttributeTypePurpose.Maintenance, fieldVisit.GetMaintenanceRecord(), true,
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
                return ViewEditMaintenanceRecord(viewModel, fieldVisit.TreatmentBMP, false, fieldVisit, fieldVisit.GetMaintenanceRecord());
            }

            fieldVisit.MarkFieldVisitAsProvisionalIfNonManager(CurrentPerson);
            var allCustomAttributeTypes = HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.ToList();
            viewModel.UpdateModel(fieldVisit, allCustomAttributeTypes);
            FinalizeVisitIfNecessary(viewModel, fieldVisit);

            SetMessageForDisplay($"{FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()} successfully updated.");

            return RedirectToNextStep(viewModel, new SitkaRoute<FieldVisitController>(c =>
                c.EditMaintenanceRecord(fieldVisit)), new SitkaRoute<FieldVisitController>(c =>
                c.PostMaintenanceAssessment(fieldVisit)), fieldVisit);
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult VisitSummary(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewData = new VisitSummaryViewData(CurrentPerson, fieldVisit);
            return RazorView<VisitSummary, VisitSummaryViewData, VisitSummaryViewModel>(viewData, new VisitSummaryViewModel());
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult VisitSummary(FieldVisitPrimaryKey fieldVisitPrimaryKey, VisitSummaryViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewData = new VisitSummaryViewData(CurrentPerson, fieldVisit);
            if (!ModelState.IsValid)
            {
                return RazorView<VisitSummary, VisitSummaryViewData, VisitSummaryViewModel>(viewData, viewModel);
            }

            fieldVisit.FieldVisitStatusID = FieldVisitStatus.Complete.FieldVisitStatusID;

            SetMessageForDisplay($"Successfully completed the Field Visit for {fieldVisit.TreatmentBMP.GetDisplayNameAsUrl()}.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(x => x.FindABMP()));
        }

        [HttpGet]
        [FieldVisitVerifyFeature]
        public PartialViewResult VerifyFieldVisit(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(fieldVisit.FieldVisitID);
            return ViewVerifyFieldVisit(fieldVisit, viewModel);
        }

        [HttpPost]
        [FieldVisitVerifyFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult VerifyFieldVisit(FieldVisitPrimaryKey fieldVisitPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewVerifyFieldVisit(fieldVisit, viewModel);
            }

            fieldVisit.VerifyFieldVisit(CurrentPerson);
            SetMessageForDisplay("The Field Visit was successfully verified.");
            return new ModalDialogFormJsonResult(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x=>x.Detail(fieldVisitPrimaryKey)));
        }

        private PartialViewResult ViewVerifyFieldVisit(FieldVisit fieldVisit, ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData($"Are you sure you want to verify the Assessment and Maintenance Records for the Field Visit to the treatment BMP '{fieldVisit.TreatmentBMP.TreatmentBMPName}' dated '{fieldVisit.VisitDate}'? ");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FieldVisitVerifyFeature]
        public PartialViewResult MarkProvisionalFieldVisit(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(fieldVisit.FieldVisitID);
            return ViewMarkProvisionalFieldVisit(fieldVisit, viewModel);
        }

        [HttpPost]
        [FieldVisitVerifyFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult MarkProvisionalFieldVisit(FieldVisitPrimaryKey fieldVisitPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewMarkProvisionalFieldVisit(fieldVisit, viewModel);
            }

            fieldVisit.MarkFieldVisitAsProvisional();
            SetMessageForDisplay("The Field Visit was successfully marked as provisional.");
            var redirectUrl =
                (fieldVisit.IsFieldVisitVerified || fieldVisit.FieldVisitStatus == FieldVisitStatus.Complete)
                    ? SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Detail(fieldVisitPrimaryKey))
                    : SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Inventory(fieldVisitPrimaryKey));
            return new ModalDialogFormJsonResult(redirectUrl);
        }

        private PartialViewResult ViewMarkProvisionalFieldVisit(FieldVisit fieldVisit, ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData($"Are you sure you want to mark the Assessment and Maintenance Records as provisional for the Field Visit to the treatment BMP '{fieldVisit.TreatmentBMP.TreatmentBMPName}' dated '{fieldVisit.VisitDate}'? ");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [FieldVisitEditFeature]
        public ActionResult Observations(FieldVisitPrimaryKey fieldVisitPrimaryKey, TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(treatmentBMPAssessmentTypeEnum);

            // need this check to support deleting assessments from the edit page
            if (treatmentBMPAssessment == null)
            {
                if (treatmentBMPAssessmentTypeEnum == TreatmentBMPAssessmentTypeEnum.Initial)
                {
                    return Redirect(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Assessment(fieldVisitPrimaryKey)));
                }
                else
                {
                    return Redirect(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.PostMaintenanceAssessment(fieldVisitPrimaryKey)));

                }
            }

            var existingObservations = treatmentBMPAssessment != null ? treatmentBMPAssessment.TreatmentBMPObservations.ToList() : new List<TreatmentBMPObservation>();
            var viewModel = new ObservationsViewModel(existingObservations);
            var viewData = new ObservationsViewData(fieldVisit, treatmentBMPAssessmentTypeEnum, CurrentPerson);
            return RazorView<Observations, ObservationsViewData, ObservationsViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Observations(FieldVisitPrimaryKey fieldVisitPrimaryKey, TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum, ObservationsViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(treatmentBMPAssessmentTypeEnum);

            if (!ModelState.IsValid)
            {
                var viewData = new ObservationsViewData(fieldVisit, treatmentBMPAssessmentTypeEnum, CurrentPerson);
                return RazorView<Observations, ObservationsViewData, ObservationsViewModel>(viewData, viewModel);
            }

            fieldVisit.MarkFieldVisitAsProvisionalIfNonManager(CurrentPerson);

            // we may not have an assessment yet if we went directly to the url instead of using the wizard
            if (treatmentBMPAssessment == null)
            {
                treatmentBMPAssessment = CreatePlaceholderTreatmentBMPAssessment(fieldVisit, treatmentBMPAssessmentTypeEnum);
            }

            foreach (var collectionMethodSectionViewModel in viewModel.Observations)
            {
                var treatmentBMPAssessmentObservationType =
                    HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes
                        .GetTreatmentBMPAssessmentObservationType(collectionMethodSectionViewModel
                            .TreatmentBMPAssessmentObservationTypeID.Value);
                var treatmentBMPObservation = GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMPAssessment, treatmentBMPAssessmentObservationType);
                collectionMethodSectionViewModel.UpdateModel(treatmentBMPObservation);
            }

            FinalizeVisitIfNecessary(viewModel, fieldVisit);
            SetMessageForDisplay("Assessment Information successfully saved.");

            return RedirectToNextStep(viewModel, new SitkaRoute<FieldVisitController>(c =>
                c.Observations(fieldVisit, treatmentBMPAssessmentTypeEnum)), new SitkaRoute<FieldVisitController>(c =>
                c.AssessmentPhotos(fieldVisit, treatmentBMPAssessmentTypeEnum)), fieldVisit);
        }

        private ActionResult RedirectToNextStep(FieldVisitViewModel viewModel, SitkaRoute<FieldVisitController> stayOnPageRoute,
            SitkaRoute<FieldVisitController> nextPageRoute, FieldVisit fieldVisit)
        {
            if (viewModel.StepToAdvanceTo.HasValue)
            {
                switch (viewModel.StepToAdvanceTo)
                {
                    case StepToAdvanceToEnum.StayOnPage:
                        return RedirectToAction(stayOnPageRoute);
                    case StepToAdvanceToEnum.NextPage:
                        return RedirectToAction(nextPageRoute);
                    case StepToAdvanceToEnum.WrapUpPage:
                        return RedirectToAction(new SitkaRoute<FieldVisitController>(c =>
                            c.VisitSummary(fieldVisit)));
                    default:
                        throw new ArgumentOutOfRangeException($"Invalid StepToAdvanceTo {viewModel.StepToAdvanceTo}");
                }
            }

            return RedirectToAction(stayOnPageRoute);
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

        #region Helper methods for Assessment

        private static TreatmentBMPAssessment CreatePlaceholderTreatmentBMPAssessment(FieldVisit fieldVisit, TreatmentBMPAssessmentTypeEnum bmpAssessmentTypeEnum)
        {
            return new TreatmentBMPAssessment(fieldVisit.TreatmentBMP, fieldVisit.TreatmentBMP.TreatmentBMPType, fieldVisit, TreatmentBMPAssessmentType.ToType(bmpAssessmentTypeEnum));
        }
        #endregion

        #region Assessment Photos

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult AssessmentPhotos(FieldVisitPrimaryKey fieldVisitPrimaryKey, TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(treatmentBMPAssessmentTypeEnum);
            var viewModel = new AssessmentPhotosViewModel(treatmentBMPAssessment);
            return ViewAssessmentPhotos(treatmentBMPAssessment, treatmentBMPAssessmentTypeEnum, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult AssessmentPhotos(FieldVisitPrimaryKey fieldVisitPrimaryKey, TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum, AssessmentPhotosViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(treatmentBMPAssessmentTypeEnum);
            if (!ModelState.IsValid)
            {
                return ViewAssessmentPhotos(treatmentBMPAssessment, treatmentBMPAssessmentTypeEnum, viewModel);
            }

            fieldVisit.MarkFieldVisitAsProvisionalIfNonManager(CurrentPerson);

            if (treatmentBMPAssessment == null)
            {
                treatmentBMPAssessment = CreatePlaceholderTreatmentBMPAssessment(fieldVisit, treatmentBMPAssessmentTypeEnum);
            }

            viewModel.UpdateModels(CurrentPerson, treatmentBMPAssessment);
            FinalizeVisitIfNecessary(viewModel, fieldVisit);
            SetMessageForDisplay("Successfully updated treatment BMP assessment photos.");
            
            return treatmentBMPAssessmentTypeEnum == TreatmentBMPAssessmentTypeEnum.Initial
                    ?  RedirectToNextStep(viewModel, new SitkaRoute<FieldVisitController>(c => c.AssessmentPhotos(fieldVisit, treatmentBMPAssessmentTypeEnum)), new SitkaRoute<FieldVisitController>(x => x.Maintain(fieldVisit)), fieldVisit)
                    : RedirectToNextStep(viewModel, new SitkaRoute<FieldVisitController>(c => c.AssessmentPhotos(fieldVisit, treatmentBMPAssessmentTypeEnum)), new SitkaRoute<FieldVisitController>(x => x.VisitSummary(fieldVisit)), fieldVisit);
        }

        private ViewResult ViewAssessmentPhotos(TreatmentBMPAssessment treatmentBMPAssessment, TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum, AssessmentPhotosViewModel viewModel)
        {
            var fieldVisitSection = treatmentBMPAssessmentTypeEnum == TreatmentBMPAssessmentTypeEnum.Initial ? (FieldVisitSection) FieldVisitSection.Assessment : FieldVisitSection.PostMaintenanceAssessment;

            var managePhotosWithPreviewViewData = new ManagePhotosWithPreviewViewData(CurrentPerson, treatmentBMPAssessment);

            var viewData = new AssessmentPhotosViewData(CurrentPerson, treatmentBMPAssessment, fieldVisitSection, managePhotosWithPreviewViewData);
            return RazorView<AssessmentPhotos, AssessmentPhotosViewData, AssessmentPhotosViewModel>(viewData, viewModel);
        }

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
            fieldVisit.DeleteFull();
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
                (fieldVisit.GetInitialAssessment() != null) ? "initial assessment" : null,
                fieldVisit.GetPostMaintenanceAssessment() != null ? "post-maintenance assessment" : null,
                fieldVisit.GetMaintenanceRecord() != null ? "maintenance record" : null
            };
            var entitiesConcatenated = string.Join(", ", entitiesSubstrings.Where(x => x != null));
            var lastComma = entitiesConcatenated.LastIndexOf(",",StringComparison.InvariantCulture);
            var associatedFieldVisitEntitiesString = lastComma > -1 ? entitiesConcatenated.Insert(lastComma + 1, " and") : entitiesConcatenated;

            return !associatedFieldVisitEntitiesString.IsNullOrWhiteSpace() ? $" This will delete the associated {associatedFieldVisitEntitiesString}." : "";
        }
    }
}