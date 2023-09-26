/*-----------------------------------------------------------------------
<copyright file="InstructionsViewData.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.TreatmentBMPBenchmarkAndThreshold
{
    public class InstructionsViewData : BenchmarkAndThresholdViewData
    {
        public const string InstructionsSectionName = "Instructions";
        public readonly string NextSectionUrl;

        public InstructionsViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP, EFModels.Entities.TreatmentBMPType treatmentBMPType, List<EFModels.Entities.TreatmentBMPBenchmarkAndThreshold> treatmentBMPBenchmarkAndThresholds)
            : base(httpContext, linkGenerator, currentPerson, treatmentBMP, treatmentBMPType, treatmentBMPBenchmarkAndThresholds)
        {
            NextSectionUrl = treatmentBMPType.HasSettableBenchmarkAndThresholdValues()
                ? SitkaRoute<TreatmentBMPBenchmarkAndThresholdController>.BuildUrlFromExpression(LinkGenerator, x => x.EditBenchmarkAndThreshold(treatmentBMP, treatmentBMPType.GetObservationTypes().First(y => y.GetHasBenchmarkAndThreshold()).TreatmentBMPAssessmentObservationTypeID))
                : SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(treatmentBMPType.TreatmentBMPTypeID));
        }
    }
}
