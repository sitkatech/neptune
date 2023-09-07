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

using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Views.Shared.SortOrder;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class ScoreDetailViewData
    {
        public readonly ScoreViewDataForAngular ViewDataForAngular;
        
        public ScoreDetailViewData(EFModels.Entities.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            var treatmentBMPType = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType;
            var treatmentBMPTypeAssessmentObservationTypes = treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes;
            ViewDataForAngular = new ScoreViewDataForAngular(treatmentBMPTypeAssessmentObservationTypes, treatmentBMPAssessment);
            OverrideScore =
                ViewDataForAngular.ObservationTypeSimples.Any(x => x.TreatmentBMPObservationSimple?.OverrideScore ?? false);
        }

        public class ScoreViewDataForAngular
        {
            public List<TreatmentBMPAssessmentObservationTypeForScoringDto> ObservationTypeSimples { get; }            
            public bool AssessmentIsComplete { get; }
            public string AssessmentScore { get; }

            public ScoreViewDataForAngular(IEnumerable<TreatmentBMPTypeAssessmentObservationType> treatmentBMPTypeAssessmentObservationTypes, EFModels.Entities.TreatmentBMPAssessment treatmentBMPAssessment)
            {
                ObservationTypeSimples = treatmentBMPTypeAssessmentObservationTypes.ToList()
                    .SortByOrderThenName().Select(x => x.TreatmentBMPAssessmentObservationType.AsForScoringDto(treatmentBMPAssessment,
                            x.OverrideAssessmentScoreIfFailing)).ToList();
                AssessmentIsComplete = treatmentBMPAssessment.IsAssessmentComplete;
                AssessmentScore = treatmentBMPAssessment.IsAssessmentComplete ? treatmentBMPAssessment.FormattedScore() : null;
            }
        }

        public bool OverrideScore { get; }
    }
}
