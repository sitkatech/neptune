/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPAssessmentController.cs" company="Tahoe Regional Planning Agency">
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
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPAssessment;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPAssessmentController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public ViewResult Detail(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, treatmentBMPAssessment);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [TreatmentBMPManageFeature]
        public ViewResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPAssessment = CreatePlaceholderTreatmentBMPAssessment(treatmentBMP);
            var viewModel = new AssessmentInformationViewModel(treatmentBMPAssessment, CurrentPerson);
            return ViewEdit(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, AssessmentInformationViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPAssessment = CreatePlaceholderTreatmentBMPAssessment(treatmentBMP);
            return EditPostImpl(treatmentBMPAssessment, viewModel);
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult Edit(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new AssessmentInformationViewModel(treatmentBMPAssessment, CurrentPerson);
            return ViewEdit(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, AssessmentInformationViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            return EditPostImpl(treatmentBMPAssessment, viewModel);
        }

        private ActionResult EditPostImpl(TreatmentBMPAssessment treatmentBMPAssessment, AssessmentInformationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(treatmentBMPAssessment, viewModel);
            }

            if (!ModelObjectHelpers.IsRealPrimaryKeyValue(treatmentBMPAssessment.PrimaryKey))
            {

                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessments.AddOrUpdate(treatmentBMPAssessment); //todo - AddOrUpdate??
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }

            viewModel.UpdateModel(treatmentBMPAssessment, CurrentPerson);
            
            SetMessageForDisplay("Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, null)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.Edit(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewEdit(TreatmentBMPAssessment treatmentBMPAssessment, AssessmentInformationViewModel viewModel)
        {
            var stormwaterJurisdictionPeople = treatmentBMPAssessment.TreatmentBMP.StormwaterJurisdiction.PeopleWhoCanManageStormwaterJurisdictionExceptSitka().ToList();
            stormwaterJurisdictionPeople.AddRange(new[] {CurrentPerson});

            if (!stormwaterJurisdictionPeople.Contains(treatmentBMPAssessment.Person))
            {
                stormwaterJurisdictionPeople.Add(treatmentBMPAssessment.Person);
            }

            stormwaterJurisdictionPeople = stormwaterJurisdictionPeople.Distinct().ToList();

            var peopleSelectList = stormwaterJurisdictionPeople.ToSelectList(p => p.PersonID.ToString(CultureInfo.InvariantCulture), p => p.FullNameFirstLastAndOrgAbbreviation);

            var viewData = new AssessmentInformationViewData(CurrentPerson, treatmentBMPAssessment, peopleSelectList);
            return RazorView<AssessmentInformation, AssessmentInformationViewData, AssessmentInformationViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult DiscreteCollectionMethod(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().FirstOrDefault(x => x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            var viewModel = new DiscreteCollectionMethodViewModel(existingObservation, TreatmentBMPAssessmentObservationType);
            return ViewCollectionMethod(treatmentBMPAssessment, ObservationTypeCollectionMethod.DiscreteValue, TreatmentBMPAssessmentObservationType, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DiscreteCollectionMethod(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey, DiscreteCollectionMethodViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewCollectionMethod(treatmentBMPAssessment, ObservationTypeCollectionMethod.DiscreteValue, TreatmentBMPAssessmentObservationType, viewModel);
            }

            var treatmentBMPObservation = GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType);
            viewModel.UpdateModel(treatmentBMPObservation);
            SetMessageForDisplay("Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.DiscreteCollectionMethod(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType)));
        }

        private static TreatmentBMPObservation GetExistingTreatmentBMPObservationOrCreateNew(
            TreatmentBMPAssessment treatmentBMPAssessment, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList()
                .Find(x => x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            if (treatmentBMPObservation == null)
            {
                var TreatmentBMPTypeAssessmentObservationType =
                    treatmentBMPAssessment.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.SingleOrDefault(x =>
                        x.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
                Check.RequireNotNull(TreatmentBMPTypeAssessmentObservationType,
                    $"Not a valid Observation Type ID {TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID} for Treatment BMP Type ID {treatmentBMPAssessment.TreatmentBMPTypeID}");
                treatmentBMPObservation = new TreatmentBMPObservation(treatmentBMPAssessment, TreatmentBMPTypeAssessmentObservationType,
                    treatmentBMPAssessment.TreatmentBMPType, TreatmentBMPAssessmentObservationType, string.Empty);
            }

            return treatmentBMPObservation;
        }
        
        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult RateCollectionMethod(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().FirstOrDefault(x => x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            var viewModel = new RateCollectionMethodViewModel(existingObservation, TreatmentBMPAssessmentObservationType);
            return ViewCollectionMethod(treatmentBMPAssessment, ObservationTypeCollectionMethod.Rate, TreatmentBMPAssessmentObservationType, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult RateCollectionMethod(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey, RateCollectionMethodViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewCollectionMethod(treatmentBMPAssessment, ObservationTypeCollectionMethod.Rate, TreatmentBMPAssessmentObservationType, viewModel);
            }

            var treatmentBMPObservation = GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType);
            viewModel.UpdateModel(treatmentBMPObservation);

            SetMessageForDisplay("Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.RateCollectionMethod(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType)));
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult PassFailCollectionMethod(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().FirstOrDefault(x => x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            var viewModel = new PassFailCollectionMethodViewModel(existingObservation, TreatmentBMPAssessmentObservationType);
            return ViewCollectionMethod(treatmentBMPAssessment, ObservationTypeCollectionMethod.PassFail, TreatmentBMPAssessmentObservationType, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult PassFailCollectionMethod(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey, PassFailCollectionMethodViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewCollectionMethod(treatmentBMPAssessment, ObservationTypeCollectionMethod.PassFail, TreatmentBMPAssessmentObservationType, viewModel);
            }

            var treatmentBMPObservation = GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType);
            viewModel.UpdateModel(treatmentBMPObservation);

            SetMessageForDisplay("Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.PassFailCollectionMethod(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType)));
        }
        
        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult PercentageCollectionMethod(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().FirstOrDefault(x => x.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            var viewModel = new PercentageCollectionMethodViewModel(existingObservation, TreatmentBMPAssessmentObservationType);
            return ViewCollectionMethod(treatmentBMPAssessment, ObservationTypeCollectionMethod.Percentage, TreatmentBMPAssessmentObservationType, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult PercentageCollectionMethod(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey, PercentageCollectionMethodViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewCollectionMethod(treatmentBMPAssessment, ObservationTypeCollectionMethod.Percentage, TreatmentBMPAssessmentObservationType, viewModel);
            }

            var treatmentBMPObservation = GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType);
            viewModel.UpdateModel(treatmentBMPObservation);

            SetMessageForDisplay("Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.PercentageCollectionMethod(treatmentBMPAssessment, TreatmentBMPAssessmentObservationType)));
        }

        private ViewResult ViewCollectionMethod(TreatmentBMPAssessment treatmentBmpAssessment, ObservationTypeCollectionMethod observationTypeCollectionMethod, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType, CollectionMethodSectionViewModel viewModel)
        {
            var viewData = new CollectionMethodSectionViewData(CurrentPerson, treatmentBmpAssessment, observationTypeCollectionMethod, TreatmentBMPAssessmentObservationType);
            return RazorView<CollectionMethodSection, CollectionMethodSectionViewData, CollectionMethodSectionViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public PartialViewResult Delete(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMPAssessment.TreatmentBMPAssessmentID);
            return ViewDeleteTreatmentBMPAssessment(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var treatmentBMPID = treatmentBMPAssessment.TreatmentBMPID;
            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentBMPAssessment(treatmentBMPAssessment, viewModel);
            }

            treatmentBMPAssessment.TreatmentBMPObservations.DeleteTreatmentBMPObservation();
            treatmentBMPAssessment.DeleteTreatmentBMPAssessment();

            SetMessageForDisplay("BMP Assessment successfully deleted.");

            return new ModalDialogFormJsonResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.Detail(treatmentBMPID)));
        }

        private PartialViewResult ViewDeleteTreatmentBMPAssessment(TreatmentBMPAssessment treatmentBMPAssessment, ConfirmDialogFormViewModel viewModel)
        {
            var waterYear = treatmentBMPAssessment.GetWaterYear();
            var canDelete = treatmentBMPAssessment.CanDelete(CurrentPerson);
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete the assessment dated {treatmentBMPAssessment.AssessmentDate.ToShortDateString()}?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Treatment BMP", SitkaRoute<TreatmentBMPAssessmentController>.BuildLinkFromExpression(x => x.Detail(treatmentBMPAssessment), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        private TreatmentBMPAssessment CreatePlaceholderTreatmentBMPAssessment(TreatmentBMP treatmentBMP)
        {
            return new TreatmentBMPAssessment(treatmentBMP, treatmentBMP.TreatmentBMPType, DateTime.Now, CurrentPerson, false, false);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessment> AssessmentGridJsonData(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPAssessments = GetTreatmentBMPAssessmentsAndGridSpec(out var gridSpec, CurrentPerson, treatmentBMP);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMPAssessment>(treatmentBMPAssessments, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<TreatmentBMPAssessment> GetTreatmentBMPAssessmentsAndGridSpec(out TreatmentBMPAssessmentGridSpec gridSpec, Person currentPerson, TreatmentBMP treatmentBMP)
        {
            gridSpec = new TreatmentBMPAssessmentGridSpec(currentPerson);
            return HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessments.Where(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID).ToList();
        }
      
        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult Score(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new ScoreViewModel(treatmentBMPAssessment);
            return ViewScore(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Score(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, ScoreViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewScore(treatmentBMPAssessment, viewModel);
            }
            
            viewModel.UpdateModel(treatmentBMPAssessment, CurrentPerson);

            SetMessageForDisplay("Score successfully saved.");

            return viewModel.AutoAdvance
                ? RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.Detail(treatmentBMPAssessment.TreatmentBMPAssessmentID)))
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.Score(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewScore(TreatmentBMPAssessment treatmentBMPAssessment, ScoreViewModel viewModel)
        {
            var viewData = new ScoreViewData(CurrentPerson, treatmentBMPAssessment);
            return RazorView<Score, ScoreViewData, ScoreViewModel>(viewData, viewModel);
        }   
 
        private RedirectResult GetNextObservationTypeViewResult(TreatmentBMPAssessment treatmentBMPAssessment, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            //Null TreatmentBMPAssessmentObservationType means we are on the Assessment Information page, in which case dummy in a sort order which is guaranteed to return the actual lowest sort order as the next page.            
            var orderedObservationTypes = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.GetObservationTypes().OrderBy(x => x.TreatmentBMPAssessmentObservationTypeName);

            var nextObservationType = TreatmentBMPAssessmentObservationType == null ? orderedObservationTypes.First() : orderedObservationTypes.FirstOrDefault(x =>  String.CompareOrdinal(x.TreatmentBMPAssessmentObservationTypeName, TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName) > 0); 
            var isNextPageScore = nextObservationType == null;

            var nextObservationTypeViewResult = isNextPageScore
                ? RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(x => x.Score(treatmentBMPAssessment.TreatmentBMPAssessmentID)))
                : Redirect(nextObservationType.AssessmentUrl(treatmentBMPAssessment));
            return nextObservationTypeViewResult;
        }
        
        [HttpGet]
        [NeptuneAdminFeature]
        public ContentResult Preview()
        {
            return Content("");
        }

        [HttpPost]
        [NeptuneAdminFeature]
        public ActionResult Preview(Views.TreatmentBMPAssessmentObservationType.EditViewModel viewModel)
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
                ModelObjectHelpers.NotYetAssignedID, DateTime.Now, CurrentPerson.PersonID, null, string.Empty, false,
                string.Empty, false);
            var observationTypeCollectionMethod = ObservationTypeCollectionMethod.All.Single(x => x.ObservationTypeCollectionMethodID == viewModel.ObservationTypeCollectionMethodID);
            var observationTypeSpecification = ObservationTypeSpecification.All.Single(x =>
                x.ObservationTargetTypeID == viewModel.ObservationTargetTypeID &&
                x.ObservationThresholdTypeID == viewModel.ObservationThresholdTypeID &&
                x.ObservationTypeCollectionMethodID == viewModel.ObservationTypeCollectionMethodID);
            var TreatmentBMPAssessmentObservationType = new TreatmentBMPAssessmentObservationType(viewModel.TreatmentBMPAssessmentObservationTypeName, observationTypeSpecification, viewModel.TreatmentBMPAssessmentObservationTypeSchema);
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    var discreteCollectionMethodViewModel = new DiscreteCollectionMethodViewModel();
                    var discreteCollectionMethodViewData = new DiscreteCollectionMethodViewData(treatmentBmpAssessment, TreatmentBMPAssessmentObservationType);
                    result = RazorPartialView<DiscreteCollectionMethod, DiscreteCollectionMethodViewData, DiscreteCollectionMethodViewModel>(discreteCollectionMethodViewData, discreteCollectionMethodViewModel);
                    break;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    var passFailCollectionMethodViewModel = new PassFailCollectionMethodViewModel();
                    var passFailCollectionMethodViewData = new PassFailCollectionMethodViewData(treatmentBmpAssessment, TreatmentBMPAssessmentObservationType);
                    result = RazorPartialView<PassFailCollectionMethod, PassFailCollectionMethodViewData, PassFailCollectionMethodViewModel>(passFailCollectionMethodViewData, passFailCollectionMethodViewModel);
                    break;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    var percentageCollectionMethodViewModel = new PercentageCollectionMethodViewModel();
                    var percentageCollectionMethodViewData = new PercentageCollectionMethodViewData(treatmentBmpAssessment, TreatmentBMPAssessmentObservationType);
                    result = RazorPartialView<PercentageCollectionMethod, PercentageCollectionMethodViewData, PercentageCollectionMethodViewModel>(percentageCollectionMethodViewData, percentageCollectionMethodViewModel);
                    break;
                case ObservationTypeCollectionMethodEnum.Rate:
                    var rateCollectionMethodViewModel = new RateCollectionMethodViewModel();
                    var rateCollectionMethodViewData = new RateCollectionMethodViewData(treatmentBmpAssessment, TreatmentBMPAssessmentObservationType);
                    result = RazorPartialView<RateCollectionMethod, RateCollectionMethodViewData, RateCollectionMethodViewModel>(rateCollectionMethodViewData, rateCollectionMethodViewModel);
                    break;
                default:
                    throw new ArgumentException($"Observation Collection Method {observationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName} not supported by Observation Type Preview.");
            }

            return result;
        }
    }
}
