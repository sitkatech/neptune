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
using Neptune.Web.Models;
using Neptune.Web.Views.ObservationType;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class RateCollectionMethodViewData : BaseObservationViewData
    {
        public RateCollectionMethodViewDataForAngular ViewDataForAngular { get; }
        public string MeasurementUnitLabelAndUnit { get; }
        public string AssessmentDescription { get; }
        public string SubmitUrl { get; }

        public RateCollectionMethodViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBmpAssessment, Models.ObservationType observationType, bool disableInputs)
            : base(currentPerson, treatmentBmpAssessment, observationType.ObservationTypeName, disableInputs)
        {
            ViewDataForAngular = new RateCollectionMethodViewDataForAngular(observationType.RateObservationTypeSchema);
            MeasurementUnitLabelAndUnit =
                $"{observationType.BenchmarkMeasurementUnitLabel()} ({observationType.BenchmarkMeasurementUnitType().LegendDisplayName})";
            AssessmentDescription = observationType.RateObservationTypeSchema.AssessmentDescription;

            SubmitUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(x =>
                x.RateCollectionMethod(treatmentBmpAssessment, observationType));
        }

        public class RateCollectionMethodViewDataForAngular
        {
            public List<SelectItemSimple> PropertiesToObserve { get; }
            public int MinimumNumberOfObservations { get; }
            public int? MaximumNumberOfObservations { get; }
            public double MinimumValueOfObservations { get; }
            public double? MaximumValueOfObservations { get; }

            public RateCollectionMethodViewDataForAngular(RateObservationTypeSchema rateObservationTypeSchema)
            {
                PropertiesToObserve = new List<SelectItemSimple>();
                var count = 1;
                rateObservationTypeSchema.PropertiesToObserve.ForEach(x =>
                {
                    PropertiesToObserve.Add(new SelectItemSimple(count, x));
                    count += 1;
                });

                MinimumNumberOfObservations = rateObservationTypeSchema.DiscreteRateMinimumNumberOfObservations;
                MaximumNumberOfObservations = rateObservationTypeSchema.DiscreteRateMaximumNumberOfObservations;
                MinimumValueOfObservations = rateObservationTypeSchema.DiscreteRateMinimumValueOfObservations;
                MaximumValueOfObservations = rateObservationTypeSchema.DiscreteRateMaximumValueOfObservations;
            }
        }
    }

}
