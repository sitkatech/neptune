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
        public ViewResult InfiltrationRate(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new InfiltrationRateViewModel(treatmentBMP);
            return ViewInfiltrationRate(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult InfiltrationRate(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, InfiltrationRateViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                SetErrorForDisplay("Could not save Infiltration Rate benchmark and threshold values: Please fix validation errors to proceed.");
                return ViewInfiltrationRate(treatmentBMP, viewModel);
            }

            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.ObservationTypeID == ObservationType.InfiltrationRate.ObservationTypeID);
            if (benchmarkAndThreshold == null)
            {
                benchmarkAndThreshold = new TreatmentBMPBenchmarkAndThreshold(treatmentBMP, ObservationType.InfiltrationRate, viewModel.BenchmarkValue.Value, viewModel.ThresholdValue.Value);
            }
            viewModel.UpdateModel(benchmarkAndThreshold, CurrentPerson);
            SetMessageForDisplay("Infiltration Rate benchmark and threshold values successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMP, ObservationType.InfiltrationRate)
                : RedirectToAction(new SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>(c => c.InfiltrationRate(treatmentBMPPrimaryKey)));
        }

        private ViewResult ViewInfiltrationRate(TreatmentBMP treatmentBMP, InfiltrationRateViewModel viewModel)
        {
            var viewData = new InfiltrationRateViewData(CurrentPerson, treatmentBMP);
            return RazorView<InfiltrationRate, InfiltrationRateViewData, InfiltrationRateViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        public ViewResult MaterialAccumulation(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new MaterialAccumulationViewModel(treatmentBMP);
            return ViewMaterialAccumulation(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult MaterialAccumulation(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, MaterialAccumulationViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                SetErrorForDisplay("Could not save Material Accumulation benchmark and threshold values: Please fix validation errors to proceed.");
                return ViewMaterialAccumulation(treatmentBMP, viewModel);
            }

            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.ObservationTypeID == ObservationType.MaterialAccumulation.ObservationTypeID);
            if (benchmarkAndThreshold == null)
            {
                benchmarkAndThreshold = new TreatmentBMPBenchmarkAndThreshold(treatmentBMP, ObservationType.MaterialAccumulation, viewModel.BenchmarkValue.Value, viewModel.ThresholdValue.Value);
            }
            viewModel.UpdateModel(benchmarkAndThreshold, CurrentPerson);
            SetMessageForDisplay("Material Accumulation benchmark and threshold values successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMP, ObservationType.MaterialAccumulation)
                : RedirectToAction(new SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>(c => c.MaterialAccumulation(treatmentBMPPrimaryKey)));
        }

        private ViewResult ViewMaterialAccumulation(TreatmentBMP treatmentBMP, MaterialAccumulationViewModel viewModel)
        {
            var viewData = new MaterialAccumulationViewData(CurrentPerson, treatmentBMP);
            return RazorView<MaterialAccumulation, MaterialAccumulationViewData, MaterialAccumulationViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        public ViewResult Runoff(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new RunoffViewModel(treatmentBMP);
            return ViewRunoff(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Runoff(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, RunoffViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                SetErrorForDisplay("Could not save Runoff benchmark and threshold values: Please fix validation errors to proceed.");
                return ViewRunoff(treatmentBMP, viewModel);
            }

            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.ObservationTypeID == ObservationType.Runoff.ObservationTypeID);
            if (benchmarkAndThreshold == null)
            {
                benchmarkAndThreshold = new TreatmentBMPBenchmarkAndThreshold(treatmentBMP, ObservationType.Runoff, viewModel.BenchmarkValue.Value, viewModel.ThresholdValue.Value);
            }
            viewModel.UpdateModel(benchmarkAndThreshold, CurrentPerson);
            SetMessageForDisplay("Runoff benchmark and threshold values successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMP, ObservationType.Runoff)
                : RedirectToAction(new SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>(c => c.Runoff(treatmentBMPPrimaryKey)));
        }

        private ViewResult ViewRunoff(TreatmentBMP treatmentBMP, RunoffViewModel viewModel)
        {
            var viewData = new RunoffViewData(CurrentPerson, treatmentBMP);
            return RazorView<Runoff, RunoffViewData, RunoffViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        public ViewResult SedimentTrapCapacity(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new SedimentTrapCapacityViewModel(treatmentBMP);
            return ViewSedimentTrapCapacity(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult SedimentTrapCapacity(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, SedimentTrapCapacityViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                SetErrorForDisplay("Could not save Sediment Trap Capacity benchmark and threshold values: Please fix validation errors to proceed.");
                return ViewSedimentTrapCapacity(treatmentBMP, viewModel);
            }

            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.ObservationTypeID == ObservationType.SedimentTrapCapacity.ObservationTypeID);
            if (benchmarkAndThreshold == null)
            {
                benchmarkAndThreshold = new TreatmentBMPBenchmarkAndThreshold(treatmentBMP, ObservationType.SedimentTrapCapacity, viewModel.BenchmarkValue.Value, viewModel.ThresholdValue.Value);
            }
            viewModel.UpdateModel(benchmarkAndThreshold, CurrentPerson);
            SetMessageForDisplay("Sediment Trap Capacity benchmark and threshold values successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMP, ObservationType.SedimentTrapCapacity)
                : RedirectToAction(new SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>(c => c.SedimentTrapCapacity(treatmentBMPPrimaryKey)));
        }

        private ViewResult ViewSedimentTrapCapacity(TreatmentBMP treatmentBMP, SedimentTrapCapacityViewModel viewModel)
        {
            var viewData = new SedimentTrapCapacityViewData(CurrentPerson, treatmentBMP);
            return RazorView<SedimentTrapCapacity, SedimentTrapCapacityViewData, SedimentTrapCapacityViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        public ViewResult VaultCapacity(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new VaultCapacityViewModel(treatmentBMP);
            return ViewVaultCapacity(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult VaultCapacity(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, VaultCapacityViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                SetErrorForDisplay("Could not save Vault Capacity benchmark and threshold values: Please fix validation errors to proceed.");
                return ViewVaultCapacity(treatmentBMP, viewModel);
            }

            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.ObservationTypeID == ObservationType.VaultCapacity.ObservationTypeID);
            if (benchmarkAndThreshold == null)
            {
                benchmarkAndThreshold = new TreatmentBMPBenchmarkAndThreshold(treatmentBMP, ObservationType.VaultCapacity, viewModel.BenchmarkValue.Value, viewModel.ThresholdValue.Value);
            }
            viewModel.UpdateModel(benchmarkAndThreshold, CurrentPerson);
            SetMessageForDisplay("Vault Capacity benchmark and threshold values successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMP, ObservationType.VaultCapacity)
                : RedirectToAction(new SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>(c => c.VaultCapacity(treatmentBMPPrimaryKey)));
        }

        private ViewResult ViewVaultCapacity(TreatmentBMP treatmentBMP, VaultCapacityViewModel viewModel)
        {
            var viewData = new VaultCapacityViewData(CurrentPerson, treatmentBMP);
            return RazorView<VaultCapacity, VaultCapacityViewData, VaultCapacityViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        public ViewResult VegetativeCover(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new VegetativeCoverViewModel(treatmentBMP);
            return ViewVegetativeCover(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult VegetativeCover(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, VegetativeCoverViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                SetErrorForDisplay("Could not save Vegetative Cover benchmark and threshold values: Please fix validation errors to proceed.");
                return ViewVegetativeCover(treatmentBMP, viewModel);
            }

            var benchmarkAndThreshold = treatmentBMP.TreatmentBMPBenchmarkAndThresholds.FirstOrDefault(x => x.ObservationTypeID == ObservationType.VegetativeCover.ObservationTypeID);
            if (benchmarkAndThreshold == null)
            {
                benchmarkAndThreshold = new TreatmentBMPBenchmarkAndThreshold(treatmentBMP, ObservationType.VegetativeCover, viewModel.BenchmarkValue.Value, viewModel.ThresholdValue.Value);
            }
            viewModel.UpdateModel(benchmarkAndThreshold, CurrentPerson);
            SetMessageForDisplay("Vegetative Cover benchmark and threshold values successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMP, ObservationType.VegetativeCover)
                : RedirectToAction(new SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>(c => c.VegetativeCover(treatmentBMPPrimaryKey)));
        }

        private ViewResult ViewVegetativeCover(TreatmentBMP treatmentBMP, VegetativeCoverViewModel viewModel)
        {
            var viewData = new VegetativeCoverViewData(CurrentPerson, treatmentBMP);
            return RazorView<VegetativeCover, VegetativeCoverViewData, VegetativeCoverViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        public ViewResult WetBasinVegetativeCover(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new WetBasinVegetativeCoverViewModel(treatmentBMP);
            return ViewWetBasinVegetativeCover(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPBenchmarkAndThresholdsManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult WetBasinVegetativeCover(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, WetBasinVegetativeCoverViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                SetErrorForDisplay("Could not save Wet Basin Vegetative Cover benchmark and threshold values: Please fix validation errors to proceed.");
                return ViewWetBasinVegetativeCover(treatmentBMP, viewModel);
            }

            viewModel.UpdateModel(treatmentBMP, CurrentPerson);
            SetMessageForDisplay("Wet Basin Vegetative Cover benchmark and threshold values successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMP, ObservationType.WetBasinVegetativeCover)
                : RedirectToAction(new SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>(c => c.WetBasinVegetativeCover(treatmentBMPPrimaryKey)));
        }

        private ViewResult ViewWetBasinVegetativeCover(TreatmentBMP treatmentBMP, WetBasinVegetativeCoverViewModel viewModel)
        {
            var viewData = new WetBasinVegetativeCoverViewData(CurrentPerson, treatmentBMP);
            return RazorView<WetBasinVegetativeCover, WetBasinVegetativeCoverViewData, WetBasinVegetativeCoverViewModel>(viewData, viewModel);
        }

        private RedirectResult GetNextObservationTypeViewResult(TreatmentBMP treatmentBMP, ObservationType observationType)
        {
            var nextObservationType = treatmentBMP.TreatmentBMPType.GetObservationTypes().OrderBy(x => x.SortOrder).Where(x => x.HasBenchmarkAndThreshold).FirstOrDefault(x => x.SortOrder > observationType.SortOrder);
            var nextObservationTypeViewResult = nextObservationType == null
                ? RedirectToAction(new SitkaRoute<TreatmentBMPController>(x => x.Detail(treatmentBMP.TreatmentBMPID)))
                : Redirect(nextObservationType.BenchmarkAndThresholdUrl(treatmentBMP.TreatmentBMPID));
            return nextObservationTypeViewResult;
        }

    }
}
