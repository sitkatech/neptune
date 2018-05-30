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
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.FieldVisit;
using Neptune.Web.Views.Shared.SortOrder;
using Neptune.Web.Views.TreatmentBMPAssessment;
using AssessmentInformation = Neptune.Web.Views.FieldVisit.AssessmentInformation;
using AssessmentInformationViewData = Neptune.Web.Views.FieldVisit.AssessmentInformationViewData;
using AssessmentInformationViewModel = Neptune.Web.Views.FieldVisit.AssessmentInformationViewModel;

namespace Neptune.Web.Controllers
{
    public class FieldVisitController : NeptuneBaseController
    {
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
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, DateTime.Now);
                HttpRequestStorage.DatabaseEntities.AllFieldVisits.Add(fieldVisit);
            }
            else if (viewModel.Continue == false)
            {
                var oldFieldVisit = treatmentBMP.InProgressFieldVisit;
                oldFieldVisit.FieldVisitStatusID = FieldVisitStatus.Unresolved.FieldVisitStatusID;
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, DateTime.Now);
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
            var viewData = new AttributesViewData(CurrentPerson, fieldVisit);
            return RazorView<Attributes, AttributesViewData>(viewData);
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
            var viewData = new MaintainViewData(CurrentPerson, fieldVisit);
            return RazorView<Maintain, MaintainViewData>(viewData);
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
            return RazorView<WrapUpVisit, WrapUpVisitViewData>(viewData);
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult ManageVisit(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewData = new ManageVisitViewData(CurrentPerson, fieldVisit);
            return RazorView<ManageVisit, ManageVisitViewData>(viewData);
        }

        [HttpGet]
        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<FieldVisit> FieldVisitGridJsonData(
            TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var fieldVisits = GetFieldVisitsAndGridSpec(out var gridSpec, CurrentPerson, treatmentBMP);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FieldVisit>(fieldVisits, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<FieldVisit> GetFieldVisitsAndGridSpec(out FieldVisitGridSpec gridSpec, Person currentPerson,
            TreatmentBMP treatmentBMP)
        {
            gridSpec = new FieldVisitGridSpec(currentPerson);
            return HttpRequestStorage.DatabaseEntities.FieldVisits.Where(x =>
                x.TreatmentBMPID == treatmentBMP.TreatmentBMPID).ToList();
        }

        #region Assessment-Related Actions

        #region New and Edit Information
        // TODO: Currently we're just creating assessments automatically when the user clicks the "Begin" button on the starter page. We may offer editing a "Basic information" section later.
        //[HttpGet]
        //[FieldVisitEditFeature]
        //public ViewResult NewAssessment(FieldVisitPrimaryKey fieldVisitPrimaryKey,
        //    int fieldVisitAssessmentTypeID)
        //{
        //    var fieldVisit = fieldVisitPrimaryKey.EntityObject;
        //    var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
        //    var treatmentBMP = fieldVisit.TreatmentBMP;
        //    var treatmentBMPAssessment = CreatePlaceholderTreatmentBMPAssessment(treatmentBMP);

        //    var viewModel = new AssessmentInformationViewModel(treatmentBMPAssessment, CurrentPerson);
        //    return ViewEdit(treatmentBMPAssessment, viewModel, fieldVisit, fieldVisitAssessmentType);
        //}

        //[HttpPost]
        //[FieldVisitEditFeature]
        //[AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        //public ActionResult NewAssessment(FieldVisitPrimaryKey fieldVisitPrimaryKey,
        //    int fieldVisitAssessmentTypeID, AssessmentInformationViewModel viewModel)
        //{
        //    var fieldVisit = fieldVisitPrimaryKey.EntityObject;
        //    var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
        //    var treatmentBMP = fieldVisit.TreatmentBMP;
        //    var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);

        //    return EditPostImpl(treatmentBMPAssessment, viewModel, fieldVisit, fieldVisitAssessmentType);
        //}

        //[HttpGet]
        //[FieldVisitEditFeature]
        //public ViewResult EditAssessment(FieldVisitPrimaryKey fieldVisitPrimaryKey,
        //    int fieldVisitAssessmentTypeID)
        //{
        //    var fieldVisit = fieldVisitPrimaryKey.EntityObject;
        //    var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
        //    var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);

        //    var viewModel = new AssessmentInformationViewModel(treatmentBMPAssessment, CurrentPerson);
        //    return ViewEdit(treatmentBMPAssessment, viewModel, fieldVisit, fieldVisitAssessmentType);
        //}

        //[HttpPost]
        //[FieldVisitEditFeature]
        //[AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        //public ActionResult EditAssessment(FieldVisitPrimaryKey fieldVisitPrimaryKey,
        //    int fieldVisitAssessmentTypeID, AssessmentInformationViewModel viewModel)
        //{
        //    var fieldVisit = fieldVisitPrimaryKey.EntityObject;
        //    var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
        //    var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
        //    return EditPostImpl(treatmentBMPAssessment, viewModel, fieldVisit, fieldVisitAssessmentType);
        //}

        //private ActionResult EditPostImpl(TreatmentBMPAssessment treatmentBMPAssessment,
        //    AssessmentInformationViewModel viewModel, FieldVisit fieldVisit,
        //    FieldVisitAssessmentType fieldVisitAssessmentType)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return ViewEdit(treatmentBMPAssessment, viewModel, fieldVisit, fieldVisitAssessmentType);
        //    }

        //    if (!ModelObjectHelpers.IsRealPrimaryKeyValue(treatmentBMPAssessment.PrimaryKey))
        //    {
        //        SaveNewAssessmentToFieldVisit(treatmentBMPAssessment, fieldVisit, fieldVisitAssessmentType);
        //    }

        //    viewModel.UpdateModel(treatmentBMPAssessment, CurrentPerson);

        //    SetMessageForDisplay("Assessment Information successfully saved.");

        //    return viewModel.AutoAdvance
        //        ? GetNextObservationTypeViewResult(fieldVisit, null, fieldVisitAssessmentType)
        //        : RedirectToAction(new SitkaRoute<FieldVisitController>(c =>
        //            c.EditAssessment(fieldVisit, (int) fieldVisitAssessmentType)));
        //}

        //private ViewResult ViewEdit(TreatmentBMPAssessment treatmentBMPAssessment,
        //    AssessmentInformationViewModel viewModel, FieldVisit fieldVisit,
        //    FieldVisitAssessmentType fieldVisitAssessmentType)
        //{
        //    var stormwaterJurisdictionPeople = treatmentBMPAssessment.TreatmentBMP.StormwaterJurisdiction
        //        .PeopleWhoCanManageStormwaterJurisdictionExceptSitka().ToList();
        //    stormwaterJurisdictionPeople.AddRange(new[] {CurrentPerson});

        //    if (!stormwaterJurisdictionPeople.Contains(treatmentBMPAssessment.GetFieldVisitPerson))
        //    {
        //        stormwaterJurisdictionPeople.Add(treatmentBMPAssessment.GetFieldVisitPerson);
        //    }

        //    stormwaterJurisdictionPeople = stormwaterJurisdictionPeople.Distinct().ToList();

        //    var peopleSelectList = stormwaterJurisdictionPeople.ToSelectList(
        //        p => p.PersonID.ToString(CultureInfo.InvariantCulture), p => p.FullNameFirstLastAndOrgAbbreviation);

        //    var viewData = new AssessmentInformationViewData(CurrentPerson, fieldVisit, peopleSelectList,
        //        fieldVisitAssessmentType);
        //    return RazorView<AssessmentInformation, AssessmentInformationViewData, AssessmentInformationViewModel>(
        //        viewData, viewModel);
        //}

        #endregion

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

        #region Kill this?

        // todo: kill this?
        //[HttpGet]
        //[FieldVisitEditFeature]
        //public ViewResult Score(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        //{
        //    var fieldVisit = fieldVisitPrimaryKey.EntityObject;
        //    var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
        //    var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
        //    var viewModel = new ScoreViewModel(treatmentBMPAssessment);
        //    return ViewScore(treatmentBMPAssessment, viewModel);
        //}

        //[HttpPost]
        //[FieldVisitEditFeature]
        //[AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        //public ActionResult Score(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey,
        //    ScoreViewModel viewModel)
        //{
        //    var fieldVisit = fieldVisitPrimaryKey.EntityObject;
        //    var fieldVisitAssessmentType = (FieldVisitAssessmentType) fieldVisitAssessmentTypeID;
        //    var treatmentBMPAssessment = fieldVisit.GetAssessmentByType(fieldVisitAssessmentType);
        //    if (!ModelState.IsValid)
        //    {
        //        return ViewScore(treatmentBMPAssessment, viewModel);
        //    }

        //    viewModel.UpdateModel(treatmentBMPAssessment, CurrentPerson);

        //    SetMessageForDisplay("Score successfully saved.");

        //    return viewModel.AutoAdvance
        //        ? RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c =>
        //            c.Detail(treatmentBMPAssessment.TreatmentBMPAssessmentID)))
        //        : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c =>
        //            c.Score(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        //}

        //private ViewResult ViewScore(TreatmentBMPAssessment treatmentBMPAssessment, ScoreViewModel viewModel)
        //{
        //    var viewData = new ScoreViewData(CurrentPerson, treatmentBMPAssessment);
        //    return RazorView<Score, ScoreViewData, ScoreViewModel>(viewData, viewModel);
        //}

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
            var isNextPageScore = nextObservationType == null;

            var nextObservationTypeViewResult = isNextPageScore
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
    }

    public enum FieldVisitAssessmentType
    {
        Initial,
        PostMaintenance
    }
}