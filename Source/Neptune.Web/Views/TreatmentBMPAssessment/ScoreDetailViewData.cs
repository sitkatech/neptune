/*-----------------------------------------------------------------------
<copyright file="ScoreDetailViewData.cs" company="Tahoe Regional Planning Agency">
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
using System.Linq;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared.SortOrder;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class ScoreDetailViewData
    {
        public readonly ScoreViewDataForAngular ViewDataForAngular;
        
        public ScoreDetailViewData(Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            var treatmentBMPType = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType;
            var TreatmentBMPTypeAssessmentObservationTypes = treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes;
            ViewDataForAngular = new ScoreViewDataForAngular(TreatmentBMPTypeAssessmentObservationTypes, treatmentBMPAssessment);
            OverrideScore =
                ViewDataForAngular.ObservationTypeSimples.Any(x => x.TreatmentBMPObservationSimple?.OverrideScore ?? false);
        }

        public class ScoreViewDataForAngular
        {
            public List<TreatmentBMPAssessmentObservationTypeSimple> ObservationTypeSimples { get; }            
            public bool AssessmentIsComplete { get; }
            public string AssessmentScore { get; }

            public ScoreViewDataForAngular(IEnumerable<TreatmentBMPTypeAssessmentObservationType> TreatmentBMPTypeAssessmentObservationTypes, Models.TreatmentBMPAssessment treatmentBMPAssessment)
            {
                ObservationTypeSimples = TreatmentBMPTypeAssessmentObservationTypes.ToList()
                    .SortByOrderThenName().Select(x =>
                        new TreatmentBMPAssessmentObservationTypeSimple(x.TreatmentBMPAssessmentObservationType, treatmentBMPAssessment,
                            x.OverrideAssessmentScoreIfFailing)).ToList();
                AssessmentIsComplete = treatmentBMPAssessment.CalculateIsAssessmentComplete();
                AssessmentScore = treatmentBMPAssessment.CalculateIsAssessmentComplete() ? treatmentBMPAssessment.FormattedScore() : null;

            }
        }

        public bool OverrideScore { get; }
    }
}
