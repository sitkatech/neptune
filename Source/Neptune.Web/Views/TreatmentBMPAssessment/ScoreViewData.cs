/*-----------------------------------------------------------------------
<copyright file="ScoreViewData.cs" company="Tahoe Regional Planning Agency">
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
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessment
{
    public class ScoreViewData : AssessmentViewData
    {
        public const string ThisSectionName = "Score";
        public readonly string CalculatedAssessmentScoreFormatted;
        public readonly ScoreDetailViewData ScoreDetailViewData;

        public ScoreViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment)
            : base(currentPerson, treatmentBMPAssessment, ThisSectionName)
        {
            CalculatedAssessmentScoreFormatted = treatmentBMPAssessment.FormattedScore();
            ScoreDetailViewData = new ScoreDetailViewData(treatmentBMPAssessment);
        }

        public class ScoreViewDataForAngular
        {
            public readonly List<ObservationTypeSimple> ObservationTypeSimples;
            public readonly TreatmentBMPAssessmentSimple TreatmentBMPAssessmentSimple;

            public ScoreViewDataForAngular(List<ObservationType> observationTypes, List<TreatmentBMPObservation> treatmentBMPObservations, Models.TreatmentBMPAssessment treatmentBMPAssessment)
            {
                ObservationTypeSimples = observationTypes.Select(x => new ObservationTypeSimple(x, treatmentBMPAssessment)).ToList();
                TreatmentBMPAssessmentSimple = new TreatmentBMPAssessmentSimple(treatmentBMPAssessment);
            }
        }

    }

    public class TreatmentBMPAssessmentSimple
    {
        public bool IsComplete { get; set; }
        public string AssessmentScore { get; set; }

        public TreatmentBMPAssessmentSimple(Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            IsComplete = treatmentBMPAssessment.IsAssessmentComplete();
            AssessmentScore = IsComplete ? treatmentBMPAssessment.FormattedScore() : null;
        }
    }

    public class TreatmentBMPObservationSimple
    {
        public bool IsComplete { get; set; }
        public bool OverrideScore { get; set; }
        public string OverrideScoreText { get; set; }
        public double? ObservedValue { get; set; }
        public string ObservationScore { get; set; }

        public TreatmentBMPObservationSimple(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationType = treatmentBMPObservation.ObservationType;
            IsComplete = observationType.IsComplete(treatmentBMPObservation);
            OverrideScore = !observationType.HasBenchmarkAndThreshold && IsComplete ? observationType.CalculateScore(treatmentBMPObservation) == 2 : false;

            switch (observationType.ToEnum)
            {
                case ObservationTypeEnum.InfiltrationRate:
                case ObservationTypeEnum.VegetativeCover:
                case ObservationTypeEnum.MaterialAccumulation:
                case ObservationTypeEnum.VaultCapacity:
                case ObservationTypeEnum.Runoff:
                case ObservationTypeEnum.SedimentTrapCapacity:    
                case ObservationTypeEnum.WetBasinVegetativeCover:
                    OverrideScoreText = string.Empty;
                    break;
                case ObservationTypeEnum.StandingWater:
                    OverrideScoreText = OverrideScore ? "Presence of standing water indicates BMP is not functioning" : "Observations indicate that BMP is functioning";
                    break;
                case ObservationTypeEnum.Installation:
                    OverrideScoreText = OverrideScore ? "Improper installation indicates BMP is not functioning" : "Proper installation indicates that BMP is functioning";
                    break;
                case ObservationTypeEnum.ConveyanceFunction:
                    OverrideScoreText = OverrideScore ? "One or more inlets or outlets are not functioning" : "All inlets and outlets are functioning";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ObservedValue = IsComplete ? (double?)observationType.GetObservationValue(treatmentBMPObservation) : null;
            ObservationScore = IsComplete ? observationType.FormattedScore(treatmentBMPObservation) : "-";
        }
    }

    public class ObservationTypeSimple
    {
        public int ObservationTypeID { get; set; }
        public bool HasBenchmarkAndThresholds { get; set; }
        public string DisplayName { get; set; }
        public double? ThresholdValueInObservedUnits { get; set; }
        public double? BenchmarkValue { get; set; }
        public double Weight { get; set; }
        public TreatmentBMPObservationSimple TreatmentBMPObservationSimple { get; set; }

        public ObservationTypeSimple(ObservationType observationType, Models.TreatmentBMPAssessment treatmentBMPAssessment)
        {
            ObservationTypeID = observationType.ObservationTypeID;
            HasBenchmarkAndThresholds = observationType.HasBenchmarkAndThreshold;
            DisplayName = $"{observationType.ObservationTypeDisplayName} {observationType.MeasurementUnitType.LegendDisplayName.EncloseInParaentheses()}";
            if (observationType == ObservationType.WetBasinVegetativeCover)
            {
                var wetBasinObservation = treatmentBMPAssessment.TreatmentBMPObservations.SingleOrDefault(x => x.ObservationType == ObservationType.WetBasinVegetativeCover);
                ThresholdValueInObservedUnits = wetBasinObservation != null ? treatmentBMPAssessment.TreatmentBMP.GetWetBasinThresholdValueInObservedUnits(wetBasinObservation) : treatmentBMPAssessment.TreatmentBMP.GetThresholdValueInObservedUnits(observationType);
            }
            else
            {
                ThresholdValueInObservedUnits = treatmentBMPAssessment.TreatmentBMP.GetThresholdValueInObservedUnits(observationType);
            }
            
            BenchmarkValue = treatmentBMPAssessment.TreatmentBMP.GetBenchmarkValue(observationType);
            Weight = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.GetTreatmentBMPTypeObservationType(observationType).AssessmentScoreWeight;

            var treatmentBMPObservation = treatmentBMPAssessment.TreatmentBMPObservations.SingleOrDefault(x => x.ObservationType == observationType);
            TreatmentBMPObservationSimple = treatmentBMPObservation != null ? new TreatmentBMPObservationSimple(treatmentBMPObservation) : null;
        }
    }
}
