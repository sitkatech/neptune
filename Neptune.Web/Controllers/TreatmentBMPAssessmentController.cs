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

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Security;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPAssessment;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPAssessmentController : NeptuneBaseController<TreatmentBMPAssessmentController>
    {
        public TreatmentBMPAssessmentController(NeptuneDbContext dbContext, ILogger<TreatmentBMPAssessmentController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet("{treatmentBMPAssessmentPrimaryKey}")]
        [TreatmentBMPAssessmentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPAssessmentPrimaryKey")]
        public ViewResult Detail([FromRoute] TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = TreatmentBMPAssessments.GetByID(_dbContext, treatmentBMPAssessmentPrimaryKey);
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMPAssessment.TreatmentBMPTypeID);
            var viewData = new DetailViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMPAssessment, treatmentBMPType);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet("{treatmentBMPAssessmentPrimaryKey}")]
        [TreatmentBMPAssessmentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPAssessmentPrimaryKey")]
        public PartialViewResult Delete([FromRoute] TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMPAssessment.TreatmentBMPAssessmentID);
            return ViewDeleteTreatmentBMPAssessment(treatmentBMPAssessment, viewModel);
        }

        [HttpPost("{treatmentBMPAssessmentPrimaryKey}")]
        [TreatmentBMPAssessmentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPAssessmentPrimaryKey")]
        public async Task<IActionResult> Delete([FromRoute] TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentBMPAssessment(treatmentBMPAssessment, viewModel);
            }
            treatmentBMPAssessment.DeleteFull(_dbContext);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("BMP Assessment successfully deleted.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteTreatmentBMPAssessment(TreatmentBMPAssessment treatmentBMPAssessment, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = treatmentBMPAssessment.CanDelete(CurrentPerson);
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete the assessment dated {treatmentBMPAssessment.GetAssessmentDate().ToStringDate()}?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Treatment BMP", UrlTemplate.MakeHrefString(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression( _linkGenerator, x => x.Detail(treatmentBMPAssessment)), "here").ToString());

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPAssessmentPrimaryKey}")]
        [TreatmentBMPAssessmentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPAssessmentPrimaryKey")]
        public ViewResult Score([FromRoute] TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new ScoreViewModel(treatmentBMPAssessment);
            return ViewScore(treatmentBMPAssessment, viewModel);
        }

        [HttpPost("{treatmentBMPAssessmentPrimaryKey}")]
        [TreatmentBMPAssessmentManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPAssessmentPrimaryKey")]
        public async Task<IActionResult> Score([FromRoute] TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, ScoreViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewScore(treatmentBMPAssessment, viewModel);
            }
            
            viewModel.UpdateModel(treatmentBMPAssessment, CurrentPerson);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay("Score successfully saved.");

            return viewModel.AutoAdvance
                ? RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(_linkGenerator, x => x.Detail(treatmentBMPAssessment.TreatmentBMPAssessmentID)))
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(_linkGenerator, x => x.Score(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewScore(TreatmentBMPAssessment treatmentBMPAssessment, ScoreViewModel viewModel)
        {
            var treatmentBMPType = TreatmentBMPTypes.GetByID(_dbContext, treatmentBMPAssessment.TreatmentBMPTypeID);
            var viewData = new ScoreViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMPAssessment, treatmentBMPType);
            return RazorView<Score, ScoreViewData, ScoreViewModel>(viewData, viewModel);
        }

        [HttpGet("{treatmentBMPAssessmentPhotoPrimaryKey}")]
        [TreatmentBMPAssessmentPhotoManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPAssessmentPhotoPrimaryKey")]
        public PartialViewResult DeletePhoto([FromRoute] TreatmentBMPAssessmentPhotoPrimaryKey treatmentBMPAssessmentPhotoPrimaryKey)
        {
            var treatmentBMPAssessmentPhoto = treatmentBMPAssessmentPhotoPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMPAssessmentPhoto.TreatmentBMPAssessmentPhotoID);
            return ViewDeletePhoto(viewModel);
        }

        [HttpPost("{treatmentBMPAssessmentPhotoPrimaryKey}")]
        [TreatmentBMPAssessmentPhotoManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPAssessmentPhotoPrimaryKey")]
        public async Task<IActionResult> DeletePhoto([FromRoute] TreatmentBMPAssessmentPhotoPrimaryKey treatmentBMPAssessmentPhotoPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewDeletePhoto(viewModel);
            }

            var treatmentBMPAssessmentPhoto = treatmentBMPAssessmentPhotoPrimaryKey.EntityObject;
            _dbContext.FileResources.Remove(treatmentBMPAssessmentPhoto.FileResource);
            _dbContext.TreatmentBMPAssessmentPhotos.Remove(treatmentBMPAssessmentPhoto);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay("Successfully deleted Treatment BMP Assessment Photo.");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeletePhoto(ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData( "Are you sure you want to delete this photo?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
    }
}
