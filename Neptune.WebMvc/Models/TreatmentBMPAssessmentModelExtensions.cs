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

using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Models
{
    public static class TreatmentBMPAssessmentModelExtensions
    {
        //public static readonly UrlTemplate<int> EditInitialAssessmentUrlTemplate = new UrlTemplate<int>(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.Assessment(UrlTemplate.Parameter1Int)));
        //public static readonly UrlTemplate<int> EditPostMaintenanceAssessmentUrlTemplate = new UrlTemplate<int>(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.PostMaintenanceAssessment(UrlTemplate.Parameter1Int)));
        //public static string GetEditUrl(this TreatmentBMPAssessment treatmentBMPAssessment)
        //{
        //    if (treatmentBMPAssessment.TreatmentBMPAssessmentType == TreatmentBMPAssessmentType.Initial){
        //        return EditInitialAssessmentUrlTemplate.ParameterReplace(treatmentBMPAssessment.FieldVisit.FieldVisitID);
        //    }

        //    return EditPostMaintenanceAssessmentUrlTemplate.ParameterReplace(treatmentBMPAssessment.FieldVisit.FieldVisitID);
        //}

        public static void CalculateAssessmentScore(this TreatmentBMPAssessment treatmentBMPAssessment, TreatmentBMPType treatmentBMPType, TreatmentBMP treatmentBMP)
        {
            if (!treatmentBMP.IsBenchmarkAndThresholdsComplete(treatmentBMPType))
            {
                return;
            }

            if (!treatmentBMPAssessment.IsAssessmentComplete)
            {
                return;
            }

            //if any observations that override the score have a failing score, return 0
            var observationTypesThatPotentiallyOverrideScore = treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .Where(x => x.OverrideAssessmentScoreIfFailing)
                .ToList().Select(x => x.TreatmentBMPAssessmentObservationType);

            double? score;
            var treatmentBMPObservations = treatmentBMPAssessment.TreatmentBMPObservations;
            if (observationTypesThatPotentiallyOverrideScore.Any(x =>
                {
                    var treatmentBMPObservation = treatmentBMPObservations.SingleOrDefault(y => y.TreatmentBMPAssessmentObservationType == x);
                    return treatmentBMPObservation?.OverrideScoreForFailingObservation() ?? false;
                }))
            {
                score = 0;
            }
            //if all observations override the score and all are passing, return 5
            else if (treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .All(x => x.OverrideAssessmentScoreIfFailing))
            {
                score = 5;
            }
            else
            {
                //otherwise calculate the score
                score = treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                    .Where(x => !x.OverrideAssessmentScoreIfFailing)
                    .Select(x => x.TreatmentBMPAssessmentObservationType).ToList().Sum(x =>
                    {
                        var tempQualifier = treatmentBMPObservations
                            .SingleOrDefault(y =>
                                y.TreatmentBMPAssessmentObservationTypeID ==
                                x.TreatmentBMPAssessmentObservationTypeID);
                        var observationScore = treatmentBMPObservations
                            .SingleOrDefault(y =>
                                y.TreatmentBMPAssessmentObservationTypeID ==
                                x.TreatmentBMPAssessmentObservationTypeID).CalculateObservationScore(treatmentBMP);

                        var treatmentBMPAssessmentObservationType = treatmentBMPObservations
                            .SingleOrDefault(y =>
                                y.TreatmentBMPAssessmentObservationTypeID ==
                                x.TreatmentBMPAssessmentObservationTypeID).TreatmentBMPAssessmentObservationType;
                        var observationWeight = Convert.ToDouble(treatmentBMPType
                            .GetTreatmentBMPTypeObservationType(treatmentBMPAssessmentObservationType)
                            .AssessmentScoreWeight
                            .Value);
                        return observationScore * observationWeight;
                    });
            }

            treatmentBMPAssessment.AssessmentScore = score;
        }

        public static void CalculateIsAssessmentComplete(this TreatmentBMPAssessment treatmentBMPAssessment)
        {
            treatmentBMPAssessment.IsAssessmentComplete = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.GetObservationTypes().All(treatmentBMPAssessment.IsObservationComplete);
        }
    }
}
