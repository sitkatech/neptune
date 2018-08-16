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
    public class PassFailCollectionMethodViewData : CollectionMethodSectionViewData
    {
        

        public PassFailCollectionMethodViewDataForAngular ViewDataForAngular { get; }
        public string PassingScoreLabel { get; }
        public string FailingScoreLabel { get; }
        public string AssessmentDescription { get; }
        public string SubmitUrl { get; }        

        public PassFailCollectionMethodViewData(Models.FieldVisit fieldVisit, Models.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, FieldVisitAssessmentType fieldVisitAssessmentType, Person currentPerson)
            : base(currentPerson, fieldVisit, fieldVisitAssessmentType == FieldVisitAssessmentType.Initial ? (Models.FieldVisitSection)Models.FieldVisitSection.Assessment : Models.FieldVisitSection.PostMaintenanceAssessment, treatmentBMPAssessmentObservationType)
        {
            ViewDataForAngular = new PassFailCollectionMethodViewDataForAngular(treatmentBMPAssessmentObservationType.GetPassFailSchema());
            PassingScoreLabel = treatmentBMPAssessmentObservationType.GetPassFailSchema().PassingScoreLabel;
            FailingScoreLabel = treatmentBMPAssessmentObservationType.GetPassFailSchema().FailingScoreLabel;
            AssessmentDescription = treatmentBMPAssessmentObservationType.GetPassFailSchema().AssessmentDescription;

            SubmitUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x =>
                x.PassFailCollectionMethod(fieldVisit, treatmentBMPAssessmentObservationType, (int)fieldVisitAssessmentType));
        }

        public PassFailCollectionMethodViewData(Models.TreatmentBMPAssessment treatmentBmpAssessment, Models.TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType, Person currentPerson): base(currentPerson)
        {
            ViewDataForAngular = new PassFailCollectionMethodViewDataForAngular(treatmentBMPAssessmentObservationType.GetPassFailSchema());
            PassingScoreLabel = treatmentBMPAssessmentObservationType.GetPassFailSchema().PassingScoreLabel;
            FailingScoreLabel = treatmentBMPAssessmentObservationType.GetPassFailSchema().FailingScoreLabel;
            AssessmentDescription = treatmentBMPAssessmentObservationType.GetPassFailSchema().AssessmentDescription;
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
