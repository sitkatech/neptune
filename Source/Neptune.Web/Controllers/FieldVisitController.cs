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

using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Microsoft.Ajax.Utilities;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views;
using Neptune.Web.Views.FieldVisit;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.Shared.EditAttributes;
using Neptune.Web.Views.Shared.Location;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
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
                HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.Where(x => x.CustomAttributeTypePurposeID == CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID).ToList();
            var viewData = new IndexViewData(CurrentPerson, neptunePage, maintenanceAttributeTypes, HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [FieldVisitViewFeature]
        public GridJsonNetJObjectResult<vFieldVisitDetailed> FieldVisitGridJsonData(
            TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey?.EntityObject;
            var fieldVisits = GetFieldVisitsAndGridSpec(out var gridSpec, CurrentPerson, treatmentBMP, true);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vFieldVisitDetailed>(fieldVisits, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [FieldVisitViewFeature]
        public GridJsonNetJObjectResult<vFieldVisitDetailed> AllFieldVisitsGridJsonData()
        {
            var fieldVisits = GetFieldVisitsAndGridSpec(out var gridSpec, CurrentPerson, null, false);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vFieldVisitDetailed>(fieldVisits, gridSpec);
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
        private static List<vFieldVisitDetailed> GetFieldVisitsAndGridSpec(out FieldVisitGridSpec gridSpec, Person currentPerson,
            TreatmentBMP treatmentBMP, bool detailPage)
        {
            gridSpec = new FieldVisitGridSpec(currentPerson, detailPage);
            var stormwaterJurisdictionIDsPersonCanView = currentPerson.GetStormwaterJurisdictionIDsPersonCanView();
            var fieldVisits = HttpRequestStorage.DatabaseEntities.vFieldVisitDetaileds
                .Where(x => stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID));
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
            var viewData = new DetailViewData(CurrentPerson, fieldVisit, initialAssessmentViewData, postMaintenanceAssessmentViewData);
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
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, viewModel.FieldVisitDate, false, fieldVisitType, false);
                HttpRequestStorage.DatabaseEntities.FieldVisits.Add(fieldVisit);
            }
            else if (viewModel.Continue == false)
            {
                var oldFieldVisit = treatmentBMP.GetInProgressFieldVisit();
                oldFieldVisit.FieldVisitStatusID = FieldVisitStatus.Unresolved.FieldVisitStatusID;
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, viewModel.FieldVisitDate, false, fieldVisitType, false);
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
        public PartialViewResult EditDateAndType(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewModel = new EditDateAndTypeViewModel(fieldVisit);
            return ViewEditDateAndType(fieldVisit, viewModel);
        }

        private PartialViewResult ViewEditDateAndType(FieldVisit fieldVisit, EditDateAndTypeViewModel viewModel)
        {
            var viewData = new EditDateAndTypeViewData(fieldVisit);
            return RazorPartialView<EditDateAndType, EditDateAndTypeViewData, EditDateAndTypeViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditDateAndType(FieldVisitPrimaryKey fieldVisitPrimaryKey, EditDateAndTypeViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            fieldVisit.FieldVisitTypeID = viewModel.FieldVisitTypeID;
            fieldVisit.VisitDate = viewModel.FieldVisitDate;

            SetMessageForDisplay("Successfully updated Field Visit Date and Field Visit Type");
            //Because this could come from multiple places, look for where the modal was triggered from
            return new ModalDialogFormJsonResult(Request.UrlReferrer.ToString());
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
            if (FinalizeVisitIfNecessary(viewModel, fieldVisit)) { return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Detail(fieldVisit))); }
            return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.VisitSummary(fieldVisit)));
        }

        private static bool FinalizeVisitIfNecessary(FieldVisitViewModel viewModel, FieldVisit fieldVisit)
        {
            if (viewModel.FinalizeVisit ?? false)
            {
                fieldVisit.FieldVisitStatusID = FieldVisitStatus.Complete.FieldVisitStatusID;
                return true;
            }

            return false;
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
            if (FinalizeVisitIfNecessary(viewModel, fieldVisit)) { return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Detail(fieldVisit))); }

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
            if (FinalizeVisitIfNecessary(viewModel, fieldVisit)) { return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Detail(fieldVisit))); }
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
            var missingRequiredAttributes = fieldVisit.TreatmentBMP.RequiredAttributeDoesNotHaveValue();
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

            if (!ModelState.IsValid)
            {
                return ViewAttributes(fieldVisit, viewModel);
            }

            viewModel.UpdateModel(fieldVisit, CurrentPerson);
            fieldVisit.TreatmentBMP.MarkInventoryAsProvisionalIfNonManager(CurrentPerson);
            fieldVisit.InventoryUpdated = true;
            if (FinalizeVisitIfNecessary(viewModel, fieldVisit)) { return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Detail(fieldVisit))); }
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
            // check if we are wrapping up the visit
            if (FinalizeVisitIfNecessary(viewModel, fieldVisit))
            {
                return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Detail(fieldVisit)));
            }

            // we are not finalizing the visit, so we are beginning the assessment
            // if we don't already have one created now is the time
            if (fieldVisit.GetAssessmentByType(treatmentBMPAssessmentTypeEnum) == null)
            {
                CreatePlaceholderTreatmentBMPAssessment(fieldVisit, treatmentBMPAssessmentTypeEnum);
            }

            return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Observations(fieldVisit, treatmentBMPAssessmentTypeEnum)));
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
            // check if we are wrapping up the visit
            if (FinalizeVisitIfNecessary(viewModel, fieldVisit))
            {
                return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Detail(fieldVisit)));
            }

            // we are not finalizing the visit, so we are beginning the assessment
            // if we don't already have one created now is the time
            if (fieldVisit.GetAssessmentByType(treatmentBMPAssessmentTypeEnum) == null)
            {
                CreatePlaceholderTreatmentBMPAssessment(fieldVisit, treatmentBMPAssessmentTypeEnum);
            }

            return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Observations(fieldVisit, treatmentBMPAssessmentTypeEnum)));
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
            // check if we are wrapping up the visit
            if (FinalizeVisitIfNecessary(viewModel, fieldVisit))
            {
                return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Detail(fieldVisit)));
            }
            // we are not finalizing the visit, so we are beginning the maintenance
            // if we don't already have one created now is the time
            if (fieldVisit.MaintenanceRecord == null)
            {
                // a little awkward, but newing up the object is sufficient to add it to the EF changeset since we're using the entity-consuming constructor
                // ReSharper disable once ObjectCreationAsStatement
                new MaintenanceRecord(fieldVisit.TreatmentBMP, fieldVisit.TreatmentBMP.TreatmentBMPType, fieldVisit)
                {
                    MaintenanceRecordTypeID = MaintenanceRecordType.Routine.MaintenanceRecordTypeID
                };
            }

            return RedirectToAction(new SitkaRoute<FieldVisitController>(x => x.EditMaintenanceRecord(fieldVisitPrimaryKey)));
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ActionResult EditMaintenanceRecord(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var maintenanceRecord = fieldVisit.MaintenanceRecord;
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

            fieldVisit.MarkFieldVisitAsProvisionalIfNonManager(CurrentPerson);
            var allCustomAttributeTypes = HttpRequestStorage.DatabaseEntities.CustomAttributeTypes.ToList();
            viewModel.UpdateModel(fieldVisit, allCustomAttributeTypes);
            if (FinalizeVisitIfNecessary(viewModel, fieldVisit)) { return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Detail(fieldVisit))); }

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
            return new ModalDialogFormJsonResult(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Detail(fieldVisitPrimaryKey)));
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
        [FieldVisitReturnToEditFeature]
        public PartialViewResult ReturnFieldVisitToEdit(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(fieldVisit.FieldVisitID);
            return ViewReturnFieldVisitToEdit(fieldVisit, viewModel);
        }

        [HttpPost]
        [FieldVisitReturnToEditFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ReturnFieldVisitToEdit(FieldVisitPrimaryKey fieldVisitPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewReturnFieldVisitToEdit(fieldVisit, viewModel);
            }

            fieldVisit.ReturnFieldVisitToEdit();
            SetMessageForDisplay("The Field Visit was successfully returned to edit.");
            var redirectUrl =
                (fieldVisit.IsFieldVisitVerified || fieldVisit.FieldVisitStatus == FieldVisitStatus.Complete)
                    ? SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Detail(fieldVisitPrimaryKey))
                    : SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Inventory(fieldVisitPrimaryKey));
            return new ModalDialogFormJsonResult(redirectUrl);
        }

        private PartialViewResult ViewReturnFieldVisitToEdit(FieldVisit fieldVisit, ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData($"Are you sure you want to re-enable editing the Field Visit to the treatment BMP '{fieldVisit.TreatmentBMP.TreatmentBMPName}' dated '{fieldVisit.VisitDate}'? ");
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
                return Redirect(treatmentBMPAssessmentTypeEnum == TreatmentBMPAssessmentTypeEnum.Initial
                    ? SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Assessment(fieldVisitPrimaryKey))
                    : SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x =>
                        x.PostMaintenanceAssessment(fieldVisitPrimaryKey)));
            }

            var existingObservations = treatmentBMPAssessment.TreatmentBMPObservations.ToList();
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
                // TODO: there should probably be a null-check here
                var treatmentBMPAssessmentObservationType =
                    HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentObservationTypes
                        .GetTreatmentBMPAssessmentObservationType(collectionMethodSectionViewModel
                            .TreatmentBMPAssessmentObservationTypeID.Value);
                var treatmentBMPObservation = GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMPAssessment, treatmentBMPAssessmentObservationType);
                collectionMethodSectionViewModel.UpdateModel(treatmentBMPObservation);
            }

            // cache the score and the completeness status because they are difficult to calculate en masse later.
            treatmentBMPAssessment.CalculateIsAssessmentComplete();
            treatmentBMPAssessment.CalculateAssessmentScore();

            if (FinalizeVisitIfNecessary(viewModel, fieldVisit)) { return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Detail(fieldVisit))); }
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
            return new TreatmentBMPAssessment(fieldVisit.TreatmentBMP, fieldVisit.TreatmentBMP.TreatmentBMPType, fieldVisit, TreatmentBMPAssessmentType.ToType(bmpAssessmentTypeEnum), false);
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
            if (FinalizeVisitIfNecessary(viewModel, fieldVisit)) { return RedirectToAction(new SitkaRoute<FieldVisitController>(c => c.Detail(fieldVisit))); }
            SetMessageForDisplay("Successfully updated treatment BMP assessment photos.");

            return treatmentBMPAssessmentTypeEnum == TreatmentBMPAssessmentTypeEnum.Initial
                    ? RedirectToNextStep(viewModel, new SitkaRoute<FieldVisitController>(c => c.AssessmentPhotos(fieldVisit, treatmentBMPAssessmentTypeEnum)), new SitkaRoute<FieldVisitController>(x => x.Maintain(fieldVisit)), fieldVisit)
                    : RedirectToNextStep(viewModel, new SitkaRoute<FieldVisitController>(c => c.AssessmentPhotos(fieldVisit, treatmentBMPAssessmentTypeEnum)), new SitkaRoute<FieldVisitController>(x => x.VisitSummary(fieldVisit)), fieldVisit);
        }

        private ViewResult ViewAssessmentPhotos(TreatmentBMPAssessment treatmentBMPAssessment, TreatmentBMPAssessmentTypeEnum treatmentBMPAssessmentTypeEnum, AssessmentPhotosViewModel viewModel)
        {
            var fieldVisitSection = treatmentBMPAssessmentTypeEnum == TreatmentBMPAssessmentTypeEnum.Initial ? (FieldVisitSection)FieldVisitSection.Assessment : FieldVisitSection.PostMaintenanceAssessment;

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
            fieldVisit.DeleteFull(HttpRequestStorage.DatabaseEntities);
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
                fieldVisit.MaintenanceRecord != null ? "maintenance record" : null
            };
            var entitiesConcatenated = string.Join(", ", entitiesSubstrings.Where(x => x != null));
            var lastComma = entitiesConcatenated.LastIndexOf(",", StringComparison.InvariantCulture);
            var associatedFieldVisitEntitiesString = lastComma > -1 ? entitiesConcatenated.Insert(lastComma + 1, " and") : entitiesConcatenated;

            return !associatedFieldVisitEntitiesString.IsNullOrWhiteSpace() ? $" This will delete the associated {associatedFieldVisitEntitiesString}." : "";
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult BulkUploadTrashScreenVisit()
        {
            return RazorView<BulkUploadTrashScreenVisit, BulkUploadTrashScreenVisitViewData>(new BulkUploadTrashScreenVisitViewData(CurrentPerson));
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public FileResult GetTrashScreenBulkUploadTemplate()
        {

            var stormwaterJurisdictionIDsPersonCanView = CurrentPerson.GetStormwaterJurisdictionIDsPersonCanView().ToList();

            // todo: constify the 35
            var currentPersonTrashScreens = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Include(x => x.StormwaterJurisdiction)
                .Include(x => x.StormwaterJurisdiction.Organization)
                .Where(x => x.TreatmentBMPTypeID == 35 &&
                            stormwaterJurisdictionIDsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();

            // todo: we will need a commercial license in order to continue using this library outside of dev/QA environment
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo newFile = DisposableTempFile.MakeDisposableTempFileEndingIn(".xlsx").FileInfo;
            FileInfo template =
                new FileInfo(
                    NeptuneWebConfiguration.PathToFieldVisitUploadTemplate);
            var row = 2;
            using (ExcelPackage package = new ExcelPackage(newFile, template))
            {
                var worksheet = package.Workbook.Worksheets["Field Visits"];
                foreach (var treatmentBMP in currentPersonTrashScreens)
                {
                    worksheet.Cells[$"A{row}"].Value = treatmentBMP.TreatmentBMPName;
                    worksheet.Cells[$"B{row}"].Value = treatmentBMP.StormwaterJurisdiction.Organization.OrganizationName;
                    worksheet.Cells[$"C{row}"].Value = treatmentBMP.YearBuilt;
                    worksheet.Cells[$"D{row}"].Value = treatmentBMP.Notes;
                    row++;
                }
                package.Save();

            }

            return File(newFile.FullName, @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}