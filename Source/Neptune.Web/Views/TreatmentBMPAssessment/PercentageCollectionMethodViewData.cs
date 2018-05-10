/*-----------------------------------------------------------------------
<copyright file="MaterialAccumulationViewData.cs" company="Tahoe Regional Planning Agency">
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
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class PercentageCollectionMethodViewData : BaseCollectionMethodFormViewData
    {
        public PercentageCollectionMethodViewDataForAngular ViewDataForAngular { get; }
        public string MeasurementUnitLabelAndUnit { get; }
        public string AssessmentDescription { get; }
        public string SubmitUrl { get; }        

        public PercentageCollectionMethodViewData(Models.TreatmentBMPAssessment treatmentBmpAssessment, Models.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            ViewDataForAngular = new PercentageCollectionMethodViewDataForAngular(TreatmentBMPAssessmentObservationType.PercentageSchema);
            MeasurementUnitLabelAndUnit = $"{TreatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitLabel()} ({TreatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType().LegendDisplayName})";
            AssessmentDescription = TreatmentBMPAssessmentObservationType.PercentageSchema.AssessmentDescription;

            SubmitUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(x => x.PercentageCollectionMethod(treatmentBmpAssessment, TreatmentBMPAssessmentObservationType));
        }

        public class PercentageCollectionMethodViewDataForAngular
        {
            public List<SelectItemSimple> PropertiesToObserve { get; }

            public PercentageCollectionMethodViewDataForAngular(PercentageObservationTypeSchema percentageObservationTypeSchema)
            {
                PropertiesToObserve = new List<SelectItemSimple>();
                var count = 1;
                percentageObservationTypeSchema.PropertiesToObserve.ForEach(x =>
                {
                    PropertiesToObserve.Add(new SelectItemSimple(count, x));
                    count += 1;
                });
            }
        }
    }
}
