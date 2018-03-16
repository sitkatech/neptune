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
    public class PassFailCollectionMethodViewData : AssessmentViewData
    {
        public PassFailCollectionMethodViewDataForAngular ViewDataForAngular { get; }
        public string PassingScoreLabel { get; }
        public string FailingScoreLabel { get; }
        public string AssessmentDescription { get; }
        public string SubmitUrl { get; }        

        public PassFailCollectionMethodViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBmpAssessment, Models.ObservationType observationType, bool disableInputs)
            : base(currentPerson, treatmentBmpAssessment, observationType.ObservationTypeName, disableInputs)
        {
            ViewDataForAngular = new PassFailCollectionMethodViewDataForAngular(observationType.PassFailSchema);
            PassingScoreLabel = observationType.PassFailSchema.PassingScoreLabel;
            FailingScoreLabel = observationType.PassFailSchema.FailingScoreLabel;
            AssessmentDescription = observationType.PassFailSchema.AssessmentDescription;

            SubmitUrl = SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(x =>
                x.PassFailCollectionMethod(treatmentBmpAssessment, observationType));
        }

        public class PassFailCollectionMethodViewDataForAngular
        {
            public List<SelectItemSimple> PropertiesToObserve { get; }

            public PassFailCollectionMethodViewDataForAngular(PassFailObservationTypeSchema passFailObservationTypeSchema)
            {
                PropertiesToObserve = new List<SelectItemSimple>();
                var count = 1;
                passFailObservationTypeSchema.PropertiesToObserve.ForEach(x =>
                {
                    PropertiesToObserve.Add(new SelectItemSimple(count, x));
                    count++;
                });

            }
        }
    }
}
