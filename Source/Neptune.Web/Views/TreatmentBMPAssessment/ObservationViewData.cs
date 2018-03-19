/*-----------------------------------------------------------------------
<copyright file="AssessmentViewData.cs" company="Tahoe Regional Planning Agency">
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

using System;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class ObservationViewData : NeptuneViewData
    {
        // ReSharper disable InconsistentNaming
        public readonly ObservationTypeCollectionMethod ObservationTypeCollectionMethod;
        public readonly BaseObservationViewData ObservationPartialViewData;

        public ObservationViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment, ObservationTypeCollectionMethod observationTypeCollectionMethod, Models.ObservationType observationType)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP)
        {
            ObservationTypeCollectionMethod = observationTypeCollectionMethod;
            switch (observationTypeCollectionMethod.ToEnum)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    ObservationPartialViewData = new DiscreteCollectionMethodViewData(currentPerson, treatmentBMPAssessment, observationType, false);
                    break;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    ObservationPartialViewData = new PassFailCollectionMethodViewData(currentPerson, treatmentBMPAssessment, observationType, false);
                    break;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    ObservationPartialViewData = new PercentageCollectionMethodViewData(currentPerson, treatmentBMPAssessment, observationType, false);
                    break;
                case ObservationTypeCollectionMethodEnum.Rate:
                    ObservationPartialViewData = new RateCollectionMethodViewData(currentPerson, treatmentBMPAssessment, observationType, false);
                    break;
                default:
                    throw new ArgumentException($"Observation Type Collection Method {observationTypeCollectionMethod.ObservationTypeCollectionMethodDisplayName} is not supported by the Observation View Data.");
            }
        }
    }
}
