/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPAssessmentModelExtensions.cs" company="Tahoe Regional Planning Agency">
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
using System.Linq;
using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class TreatmentBMPAssessmentModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return DetailUrlTemplate.ParameterReplace(treatmentBMPAssessment.TreatmentBMPAssessmentID);
        }

        public static readonly UrlTemplate<int> EditInitialAssessmentUrlTemplate = new UrlTemplate<int>(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.Assessment(UrlTemplate.Parameter1Int)));
        public static readonly UrlTemplate<int> EditPostMaintenanceAssessmentUrlTemplate = new UrlTemplate<int>(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.PostMaintenanceAssessment(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            if (treatmentBMPAssessment.TreatmentBMPAssessmentType == TreatmentBMPAssessmentType.Initial){
                return EditInitialAssessmentUrlTemplate.ParameterReplace(treatmentBMPAssessment.FieldVisit.FieldVisitID);
            }

            return EditPostMaintenanceAssessmentUrlTemplate.ParameterReplace(treatmentBMPAssessment.FieldVisit.FieldVisitID);
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPAssessmentController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            return DeleteUrlTemplate.ParameterReplace(treatmentBMPAssessment.TreatmentBMPAssessmentID);
        }

        // attempt to read the cached assessment score; otherwise calculate it, cache it, and return it
        public static double? CalculateAssessmentScore(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            if (!treatmentBMPAssessment.TreatmentBMP.IsBenchmarkAndThresholdsComplete())
            {
                return null;
            }

            if (!treatmentBMPAssessment.IsAssessmentComplete())
            {
                return null;
            }

            if (treatmentBMPAssessment.AssessmentScore.HasValue)
            {
                return Math.Round(treatmentBMPAssessment.AssessmentScore.Value, 1);
            }

            
            //if any observations that override the score have a failing score, return 0
            var observationTypesThatPotentiallyOverrideScore = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .Where(x => x.OverrideAssessmentScoreIfFailing)
                .ToList().Select(x => x.TreatmentBMPAssessmentObservationType);

            if (observationTypesThatPotentiallyOverrideScore.Any(x =>
            {
                var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.SingleOrDefault(y => y.TreatmentBMPAssessmentObservationType == x);
                return treatmentBMPObservation?.OverrideScoreForFailingObservation(x) ?? false;
            }))
            {
                return 0;
            }

            //if all observations override the score and all are passing, return 5
            if (treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .All(x => x.OverrideAssessmentScoreIfFailing))
            {
                return 5;
            }

            //otherwise calculate the score
            var score = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .Where(x => !x.OverrideAssessmentScoreIfFailing)
                .Select(x => x.TreatmentBMPAssessmentObservationType).ToList().Sum(x =>
                {
                    var observationScore = treatmentBMPAssessment.TreatmentBMPObservations.SingleOrDefault(y => y.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID == x.TreatmentBMPAssessmentObservationTypeID).CalculateObservationScore();

                    var treatmentBMPAssessmentObservationType = treatmentBMPAssessment.TreatmentBMPObservations.SingleOrDefault(y => y.TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID == x.TreatmentBMPAssessmentObservationTypeID).TreatmentBMPAssessmentObservationType;
                    var observationWeight = Convert.ToDouble(treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType
                        .GetTreatmentBMPTypeObservationType(treatmentBMPAssessmentObservationType).AssessmentScoreWeight
                        .Value);
                    return observationScore * observationWeight;
                });

            if (score.HasValue)
            {
                treatmentBMPAssessment.AssessmentScore = score;
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }

            return Math.Round(score.Value, 1);
        }
    }
}
