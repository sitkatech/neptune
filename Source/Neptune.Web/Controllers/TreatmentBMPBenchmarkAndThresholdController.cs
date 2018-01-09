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
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TreatmentBMPBenchmarkAndThreshold;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPBenchmarkAndThresholdController : NeptuneBaseController
    {
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        public ViewResult Instructions(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewData = new InstructionsViewData(CurrentPerson, treatmentBMP);
            return RazorView<Instructions, InstructionsViewData>(viewData);
        }


        [HttpGet]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        public ViewResult DiscreteThreshold(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, ObservationTypePrimaryKey observationTypePrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var observationType = observationTypePrimaryKey.EntityObject;

            var viewModel = new DiscreteThresholdViewModel(treatmentBMP, observationType);
            return ViewDiscreteThreshold(treatmentBMP, observationType, viewModel);
        }

        [HttpPost]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DiscreteThreshold(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, ObservationTypePrimaryKey observationTypePrimaryKey, DiscreteThresholdViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var observationType = observationTypePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                SetErrorForDisplay("Could not save benchmark and threshold values: Please fix validation errors to proceed.");
                return ViewDiscreteThreshold(treatmentBMP, observationType, viewModel);
            }

            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.ObservationTypeID == observationType.ObservationTypeID) ??
                                        new TreatmentBMPBenchmarkAndThreshold(treatmentBMP, observationType, viewModel.BenchmarkValue.Value, viewModel.ThresholdValue.Value);

            viewModel.UpdateModel(benchmarkAndThreshold, CurrentPerson);
            SetMessageForDisplay("Benchmark and threshold values successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMP, observationType)
                : RedirectToAction(new SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>(c => c.DiscreteThreshold(treatmentBMPPrimaryKey, observationTypePrimaryKey)));
        }

        private ViewResult ViewDiscreteThreshold(TreatmentBMP treatmentBMP, ObservationType observationType, DiscreteThresholdViewModel viewModel)
        {
            var viewData = new DiscreteThresholdViewData(CurrentPerson, treatmentBMP, observationType);
            return RazorView<DiscreteThreshold, DiscreteThresholdViewData, DiscreteThresholdViewModel>(viewData, viewModel);
        }

        private RedirectResult GetNextObservationTypeViewResult(TreatmentBMP treatmentBMP, ObservationType observationType)
        {
            var nextObservationType = treatmentBMP.TreatmentBMPType.GetObservationTypes().OrderBy(x => x.ObservationTypeName).Where(x => x.HasBenchmarkAndThreshold).FirstOrDefault(x => string.Compare(x.ObservationTypeName, observationType.ObservationTypeName) > 0);
            var nextObservationTypeViewResult = nextObservationType == null
                ? RedirectToAction(new SitkaRoute<TreatmentBMPController>(x => x.Detail(treatmentBMP.TreatmentBMPID)))
                : Redirect(nextObservationType.BenchmarkAndThresholdUrl(treatmentBMP));
            return nextObservationTypeViewResult;
        }

    }
}
