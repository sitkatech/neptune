﻿/*-----------------------------------------------------------------------
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

using System.Web.Mvc;
using Hangfire;
using LtInfo.Common;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.ScheduledJobs;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPAssessment;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPAssessmentController : NeptuneBaseController
    {
        [TreatmentBMPAssessmentManageFeature]
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
            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentBMPAssessment(treatmentBMPAssessment, viewModel);
            }
            treatmentBMPAssessment.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay("BMP Assessment successfully deleted.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteTreatmentBMPAssessment(TreatmentBMPAssessment treatmentBMPAssessment, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = treatmentBMPAssessment.CanDelete(CurrentPerson);
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete the assessment dated {treatmentBMPAssessment.GetAssessmentDate().ToStringDate()}?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Treatment BMP", SitkaRoute<TreatmentBMPAssessmentController>.BuildLinkFromExpression(x => x.Detail(treatmentBMPAssessment), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
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
        [TreatmentBMPAssessmentPhotoManageFeature]
        public PartialViewResult DeletePhoto(TreatmentBMPAssessmentPhotoPrimaryKey treatmentBMPAssessmentPhotoPrimaryKey)
        {
            var treatmentBMPAssessmentPhoto = treatmentBMPAssessmentPhotoPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMPAssessmentPhoto.TreatmentBMPAssessmentPhotoID);
            return ViewDeletePhoto(viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentPhotoManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeletePhoto(TreatmentBMPAssessmentPhotoPrimaryKey treatmentBMPAssessmentPhotoPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewDeletePhoto(viewModel);
            }

            var treatmentBMPAssessmentPhoto = treatmentBMPAssessmentPhotoPrimaryKey.EntityObject;
            HttpRequestStorage.DatabaseEntities.FileResources.Remove(treatmentBMPAssessmentPhoto.FileResource);
            HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessmentPhotos.Remove(treatmentBMPAssessmentPhoto);

            SetMessageForDisplay("Successfully deleted Treatment BMP Assessment Photo.");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeletePhoto(ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData("Are you sure you want to delete this photo?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        //todo: remove when runs once in PROD
        [SitkaAdminFeature]
        public ContentResult RefreshAssessmentScores()
        {
            BackgroundJob.Enqueue(() => ScheduledBackgroundJobLaunchHelper.RunRefreshAssessmentScoreJob(CurrentPerson.PersonID));
            return Content("Refresh will run in the background.");
        }
    }
}
