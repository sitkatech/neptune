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

using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPBenchmarkAndThreshold;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPBenchmarkAndThresholdController : NeptuneBaseController
    {
        [TreatmentBMPManageFeature]
        public ViewResult Instructions(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewData = new InstructionsViewData(CurrentPerson, treatmentBMP);
            return RazorView<Instructions, InstructionsViewData>(viewData);
        }

        [HttpGet]
        [TreatmentBMPManageFeature]
        public ViewResult EditBenchmarkAndThreshold(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;

            var viewModel = new EditBenchmarkAndThresholdViewModel(treatmentBMP, TreatmentBMPAssessmentObservationType);
            return ViewEditBenchmarkAndThreshold(treatmentBMP, TreatmentBMPAssessmentObservationType, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditBenchmarkAndThreshold(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, TreatmentBMPAssessmentObservationTypePrimaryKey treatmentBMPAssessmentObservationTypePrimaryKey, EditBenchmarkAndThresholdViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var TreatmentBMPAssessmentObservationType = treatmentBMPAssessmentObservationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                SetErrorForDisplay("Could not save benchmark and threshold values: Please fix validation errors to proceed.");
                return ViewEditBenchmarkAndThreshold(treatmentBMP, TreatmentBMPAssessmentObservationType, viewModel);
            }

            var benchmarkAndThreshold = GetExistingTreatmentBMPObservationOrCreateNew(treatmentBMP, TreatmentBMPAssessmentObservationType);

            viewModel.UpdateModel(benchmarkAndThreshold, CurrentPerson);
            SetMessageForDisplay("Benchmark and threshold values successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMP, TreatmentBMPAssessmentObservationType)
                : RedirectToAction(new SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>(c => c.EditBenchmarkAndThreshold(treatmentBMPPrimaryKey, treatmentBMPAssessmentObservationTypePrimaryKey)));
        }

        private static TreatmentBMPBenchmarkAndThreshold GetExistingTreatmentBMPObservationOrCreateNew(
            TreatmentBMP treatmentBMP, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            var treatmentBMPObservation = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.SingleOrDefault(x => x.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
            if (treatmentBMPObservation == null)
            {
                var TreatmentBMPTypeAssessmentObservationType =
                    treatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes.SingleOrDefault(x =>
                        x.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID);
                Check.RequireNotNull(TreatmentBMPTypeAssessmentObservationType,
                    $"Not a valid Observation Type ID {TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID} for Treatment BMP Type ID {treatmentBMP.TreatmentBMPTypeID}");
                treatmentBMPObservation = new TreatmentBMPBenchmarkAndThreshold(treatmentBMP, TreatmentBMPTypeAssessmentObservationType,
                    treatmentBMP.TreatmentBMPType, TreatmentBMPAssessmentObservationType, 0, 0);
            }

            return treatmentBMPObservation;
        }


        private ViewResult ViewEditBenchmarkAndThreshold(TreatmentBMP treatmentBMP, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType, EditBenchmarkAndThresholdViewModel viewModel)
        {
            var viewData = new EditBenchmarkAndThresholdViewData(CurrentPerson, treatmentBMP, TreatmentBMPAssessmentObservationType);
            return RazorView<EditBenchmarkAndThreshold, EditBenchmarkAndThresholdViewData, EditBenchmarkAndThresholdViewModel>(viewData, viewModel);
        }

        private RedirectResult GetNextObservationTypeViewResult(TreatmentBMP treatmentBMP, TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            var nextObservationType = treatmentBMP.TreatmentBMPType.GetObservationTypes().OrderBy(x => x.TreatmentBMPAssessmentObservationTypeName).Where(x => x.HasBenchmarkAndThreshold).FirstOrDefault(x => string.Compare(x.TreatmentBMPAssessmentObservationTypeName, TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeName) > 0);
            var nextObservationTypeViewResult = nextObservationType == null
                ? RedirectToAction(new SitkaRoute<TreatmentBMPController>(x => x.Detail(treatmentBMP.TreatmentBMPID)))
                : Redirect(nextObservationType.BenchmarkAndThresholdUrl(treatmentBMP));
            return nextObservationTypeViewResult;
        }

    }
}
