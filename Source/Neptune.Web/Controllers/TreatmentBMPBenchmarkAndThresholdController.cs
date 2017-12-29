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
