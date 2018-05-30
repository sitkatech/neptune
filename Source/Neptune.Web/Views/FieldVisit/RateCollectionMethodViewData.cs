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
using Neptune.Web.Views.TreatmentBMPAssessmentObservationType;

namespace Neptune.Web.Views.FieldVisit
{
    public class RateCollectionMethodViewData : BaseCollectionMethodFormViewData
    {
        public RateCollectionMethodViewDataForAngular ViewDataForAngular { get; }
        public string MeasurementUnitLabelAndUnit { get; }
        public string AssessmentDescription { get; }
        public string SubmitUrl { get; }

        public RateCollectionMethodViewData(Models.FieldVisit fieldVisit, Models.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, FieldVisitAssessmentType fieldVisitAssessmentType, Person currentPerson)
            : base(currentPerson, fieldVisit, fieldVisitAssessmentType == FieldVisitAssessmentType.Initial ? (Models.FieldVisitSection)Models.FieldVisitSection.Assessment : Models.FieldVisitSection.PostMaintenanceAssessment)
        {
            ViewDataForAngular = new RateCollectionMethodViewDataForAngular(treatmentBMPAssessmentObservationType.RateObservationTypeSchema);
            MeasurementUnitLabelAndUnit =
                $"{treatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitLabel()} ({treatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType().LegendDisplayName})";
            AssessmentDescription = treatmentBMPAssessmentObservationType.RateObservationTypeSchema.AssessmentDescription;

            SubmitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x =>
                x.RateCollectionMethod(fieldVisit, treatmentBMPAssessmentObservationType, (int)fieldVisitAssessmentType));
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
