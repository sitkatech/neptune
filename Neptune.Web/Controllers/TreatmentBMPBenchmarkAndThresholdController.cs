/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPBenchmarkAndThresholdController.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Common.DesignByContract;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Security;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.TreatmentBMPBenchmarkAndThreshold;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPBenchmarkAndThresholdController : NeptuneBaseController<TreatmentBMPBenchmarkAndThresholdController>
    {
        public TreatmentBMPBenchmarkAndThresholdController(NeptuneDbContext dbContext, ILogger<TreatmentBMPBenchmarkAndThresholdController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet("{treatmentBMPPrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        public ViewResult Instructions([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewData = new InstructionsViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMP);
            return RazorView<Instructions, InstructionsViewData>(viewData);
        }

        [HttpGet("{treatmentBMPPrimaryKey}/{treatmentBMPAssessmentObservationTypePrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPAssessmentObservationTypePrimaryKey")]
        public ViewResult EditBenchmarkAndThreshold([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, [FromRoute] TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;

            var viewModel = new EditBenchmarkAndThresholdViewModel(treatmentBMP, treatmentBMPAssessmentObservationType);
            return ViewEditBenchmarkAndThreshold(treatmentBMP, treatmentBMPAssessmentObservationType, viewModel);
        }

        [HttpPost("{treatmentBMPPrimaryKey}/{treatmentBMPAssessmentObservationTypePrimaryKey}")]
        [TreatmentBMPManageFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPPrimaryKey")]
        [ValidateEntityExistsAndPopulateParameterFilter("treatmentBMPAssessmentObservationTypePrimaryKey")]
        public async Task<IActionResult> EditBenchmarkAndThreshold([FromRoute] TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, [FromRoute] TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey, EditBenchmarkAndThresholdViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                SetErrorForDisplay("Could not save benchmark and threshold values: Please fix validation errors to proceed.");
                return ViewEditBenchmarkAndThreshold(treatmentBMP, treatmentBMPAssessmentObservationType, viewModel);
            }

            var benchmarkAndThreshold = GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMP, treatmentBMPAssessmentObservationType);

            viewModel.UpdateModel(benchmarkAndThreshold, CurrentPerson);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay("Benchmark and threshold values successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMP, treatmentBMPAssessmentObservationType)
                : RedirectToAction(new SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>(_linkGenerator, x => x.EditBenchmarkAndThreshold(treatmentBMPPrimaryKey, treatmentBMPAssessmentObservationTypePrimaryKey)));
        }

        private static TreatmentBMPBenchmarkAndThreshold GetExistingTreatmentBMPObservationOrCreateNew(
            TreatmentBMP treatmentBMP, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var treatmentBMPObservation = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.SingleOrDefault(x => x.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            if (treatmentBMPObservation == null)
            {
                var treatmentBMPTypeAssessmentObservationType =
                    treatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.SingleOrDefault(x =>
                        x.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
                Check.RequireNotNull(treatmentBMPTypeAssessmentObservationType,
                    $"Not a valid Observation Type ID {treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID} for Treatment BMP Type ID {treatmentBMP.TreatmentBMPTypeID}");
                treatmentBMPObservation = new TreatmentBMPBenchmarkAndThreshold
                {
                    TreatmentBMP = treatmentBMP,
                    TreatmentBMPTypeAssessmentObservationType = treatmentBMPTypeAssessmentObservationType,
                    TreatmentBMPType = treatmentBMP.TreatmentBMPType,
                    TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationType, BenchmarkValue = 0,
                    ThresholdValue = 0
                };
            }

            return treatmentBMPObservation;
        }


        private ViewResult ViewEditBenchmarkAndThreshold(TreatmentBMP treatmentBMP, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, EditBenchmarkAndThresholdViewModel viewModel)
        {
            var viewData = new EditBenchmarkAndThresholdViewData(HttpContext, _linkGenerator, CurrentPerson, treatmentBMP, treatmentBMPAssessmentObservationType);
            return RazorView<EditBenchmarkAndThreshold, EditBenchmarkAndThresholdViewData, EditBenchmarkAndThresholdViewModel>(viewData, viewModel);
        }

        private RedirectResult GetNextObservationTypeViewResult(TreatmentBMP treatmentBMP, TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            var orderedObservationTypes = treatmentBMP.TreatmentBMPType.GetObservationTypes()
                .Where(x => x.GetHasBenchmarkAndThreshold()).ToList();

            var nextObservationType = treatmentBMPAssessmentObservationType == null
                ? orderedObservationTypes.First()
                : orderedObservationTypes.ElementAtOrDefault(
                    orderedObservationTypes.IndexOf(treatmentBMPAssessmentObservationType) + 1);

            var nextObservationTypeViewResult = nextObservationType == null
                ? RedirectToAction(new SitkaRoute<TreatmentBMPController>(_linkGenerator, x => x.Detail(treatmentBMP.TreatmentBMPID)))
                : Redirect(SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(_linkGenerator, x => x.EditBenchmarkAndThreshold(treatmentBMP, nextObservationType)));
            return nextObservationTypeViewResult;
        }

    }
}
