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

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class ScoreDetailViewData
    {
        public readonly ScoreViewDataForAngular ViewDataForAngular;
        
        public ScoreDetailViewData(Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            ViewDataForAngular = new ScoreViewDataForAngular(treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.GetObservationTypes().OrderBy(x => x.ObservationTypeName).ToList(),                
                treatmentBMPAssessment);        
        }

        public class ScoreViewDataForAngular
        {
            public List<TreatmentBMPAssessmentObservationTypeSimple> ObservationTypeSimples { get; }            
            public bool AssessmentIsComplete { get; }
            public string AssessmentScore { get; }

            public ScoreViewDataForAngular(List<Models.ObservationType> observationTypes, Models.TreatmentBMPAssessment treatmentBMPAssessment)
            {
                ObservationTypeSimples = observationTypes.Select(x => new TreatmentBMPAssessmentObservationTypeSimple(x, treatmentBMPAssessment)).ToList();
                AssessmentIsComplete = treatmentBMPAssessment.IsAssessmentComplete();
                AssessmentScore = treatmentBMPAssessment.IsAssessmentComplete() ? treatmentBMPAssessment.FormattedScore() : null;
            }
        }
    }
}
