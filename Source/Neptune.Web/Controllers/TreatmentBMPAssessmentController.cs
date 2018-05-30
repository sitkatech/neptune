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

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPAssessment;

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
            var canDelete = treatmentBMPAssessment.CanDelete(CurrentPerson);
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete the assessment dated {treatmentBMPAssessment.GetAssessmentDate.ToShortDateString()}?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Treatment BMP", SitkaRoute<TreatmentBMPAssessmentController>.BuildLinkFromExpression(x => x.Detail(treatmentBMPAssessment), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
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
        
        [HttpGet]
        [NeptuneAdminFeature]
        public ContentResult Preview()
        {
            return Content("");
        }

        // todo: neutered for now, may bring back later.
        //[HttpPost]
        //[NeptuneAdminFeature]
        //public ActionResult Preview(Views.TreatmentBMPAssessmentObservationType.EditViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var modelStateSerialized = JObject
        //            .FromObject(ModelState.ToDictionary(x => x.Key,
        //                x => x.Value.Errors.Select(y => y.ErrorMessage).ToList())).ToString(Formatting.None);
        //        Response.StatusCode = 400;
        //        Response.ContentType = "application/json";
        //        return Content(modelStateSerialized);
        //    }
        //
        //    PartialViewResult result = null;
        //    var treatmentBmpAssessment = new TreatmentBMPAssessment(ModelObjectHelpers.NotYetAssignedID,
        //        ModelObjectHelpers.NotYetAssignedID,
        //        ModelObjectHelpers.NotYetAssignedID, null, string.Empty, 
        //        string.Empty);
        //    var observationTypeCollectionMethod = ObservationTypeCollectionMethod.All.Single(x => x.ObservationTypeCollectionMethodID == viewModel.ObservationTypeCollectionMethodID);
        //    var observationTypeSpecification = ObservationTypeSpecification.All.Single(x =>
        //        x.ObservationTargetTypeID == viewModel.ObservationTargetTypeID &&
        //        x.ObservationThresholdTypeID == viewModel.ObservationThresholdTypeID &&
        //        x.ObservationTypeCollectionMethodID == viewModel.ObservationTypeCollectionMethodID);
        //    var TreatmentBMPAssessmentObservationType = new TreatmentBMPAssessmentObservationType(viewModel.TreatmentBMPAssessmentObservationTypeName, observationTypeSpecification, viewModel.TreatmentBMPAssessmentObservationTypeSchema);
        //    switch (observationTypeCollectionMethod.ToEnum)
        //    {
        //        case ObservationTypeCollectionMethodEnum.DiscreteValue:
        //            var discreteCollectionMethodViewModel = new DiscreteCollectionMethodViewModel();
        //            var discreteCollectionMethodViewData = new DiscreteCollectionMethodViewData(treatmentBmpAssessment, TreatmentBMPAssessmentObservationType);
        //            result = RazorPartialView<DiscreteCollectionMethod, DiscreteCollectionMethodViewData, DiscreteCollectionMethodViewModel>(discreteCollectionMethodViewData, discreteCollectionMethodViewModel);
        //            break;
        //        case ObservationTypeCollectionMethodEnum.PassFail:
        //            var passFailCollectionMethodViewModel = new PassFailCollectionMethodViewModel();
        //            var passFailCollectionMethodViewData = new PassFailCollectionMethodViewData(treatmentBmpAssessment, TreatmentBMPAssessmentObservationType);
        //            result = RazorPartialView<PassFailCollectionMethod, PassFailCollectionMethodViewData, PassFailCollectionMethodViewModel>(passFailCollectionMethodViewData, passFailCollectionMethodViewModel);
        //            break;
        //        case ObservationTypeCollectionMethodEnum.Percentage:
        //            var percentageCollectionMethodViewModel = new PercentageCollectionMethodViewModel();
        //            var percentageCollectionMethodViewData = new PercentageCollectionMethodViewData(treatmentBmpAssessment, TreatmentBMPAssessmentObservationType);
        //            result = RazorPartialView<PercentageCollectionMethod, PercentageCollectionMethodViewData, PercentageCollectionMethodViewModel>(percentageCollectionMethodViewData, percentageCollectionMethodViewModel);
        //            break;
        //        case ObservationTypeCollectionMethodEnum.Rate:
        //            var rateCollectionMethodViewModel = new RateCollectionMethodViewModel();
        //            var rateCollectionMethodViewData = new RateCollectionMethodViewData(treatmentBmpAssessment, TreatmentBMPAssessmentObservationType);
        //            result = RazorPartialView<RateCollectionMethod, RateCollectionMethodViewData, RateCollectionMethodViewModel>(rateCollectionMethodViewData, rateCollectionMethodViewModel);
        //            break;
        //        default:
        //            throw new ArgumentException($"Observation Collection Method {observationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName} not supported by Observation Type Preview.");
        //    }
        //
        //    return result;
        //}
    }
}
