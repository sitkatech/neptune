﻿/*-----------------------------------------------------------------------
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
    public class DiscreteCollectionMethodViewData : BaseCollectionMethodFormViewData
    {
        public DiscreteCollectionMethodViewDataForAngular ViewDataForAngular { get; }
        public string MeasurementUnitLabelAndUnit { get; }
        public string AssessmentDescription { get; }
        public string SubmitUrl { get; }        

        public DiscreteCollectionMethodViewData(Models.TreatmentBMPAssessment treatmentBmpAssessment, Models.TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            ViewDataForAngular = new DiscreteCollectionMethodViewDataForAngular(TreatmentBMPAssessmentObservationType.DiscreteObservationTypeSchema);
            MeasurementUnitLabelAndUnit =
                $"{TreatmentBMPAssessmentObservationType.DiscreteObservationTypeSchema.MeasurementUnitLabel} ({TreatmentBMPAssessmentObservationType.BenchmarkMeasurementUnitType().LegendDisplayName})";
            AssessmentDescription = TreatmentBMPAssessmentObservationType.DiscreteObservationTypeSchema.AssessmentDescription;

            SubmitUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(x =>
                x.DiscreteCollectionMethod(treatmentBmpAssessment, TreatmentBMPAssessmentObservationType));
        }

        public class DiscreteCollectionMethodViewDataForAngular
        {
            public List<SelectItemSimple> PropertiesToObserve { get; }
            public int MinimumNumberOfObservations { get; }
            public int MaximumNumberOfObservations { get; }
            public double MinimumValueOfObservations { get; }
            public double MaximumValueOfObservations { get; }

            public DiscreteCollectionMethodViewDataForAngular(DiscreteObservationTypeSchema discreteObservationTypeSchema)
            {
                PropertiesToObserve = new List<SelectItemSimple>();
                var count = 1;
                discreteObservationTypeSchema.PropertiesToObserve.ForEach(x =>
                {
                    PropertiesToObserve.Add(new SelectItemSimple(count, x));
                    count += 1;
                });

                MinimumNumberOfObservations = discreteObservationTypeSchema.MinimumNumberOfObservations;
                MaximumNumberOfObservations = discreteObservationTypeSchema.MaximumNumberOfObservations ?? int.MaxValue;
                MinimumValueOfObservations = discreteObservationTypeSchema.MinimumValueOfObservations;
                MaximumValueOfObservations = discreteObservationTypeSchema.MaximumValueOfObservations ?? double.MaxValue;
            }
        }
    }
}
