﻿/*-----------------------------------------------------------------------
<copyright file="TreatmentBMP.cs" company="Tahoe Regional Planning Agency">
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
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Views;
using Neptune.Web.Security;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMP : IAuditableEntity
    {
        public bool CanView(Person person)
        {
            return new NeptuneViewFeature().HasPermissionByPerson(person);
        }

        public bool CanEdit(Person person)
        {
            return new TreatmentBMPManageFeature().HasPermission(person, this).HasPermission;
        }

        public bool CanDelete()
        {
            return TreatmentBMPAssessments.Count.Equals(0);
        }
       
        public string AuditDescriptionString => TreatmentBMPName; public string FormattedNameAndType => $"{TreatmentBMPName} ({TreatmentBMPType.TreatmentBMPTypeName})";

        public bool IsBenchmarkAndThresholdsComplete()
        {
            var observationTypesIDs = TreatmentBMPType.GetObservationTypes().Where(x => x.HasBenchmarkAndThreshold).Select(x => x.ObservationTypeID).ToList();
            var benchmarkAndThresholdObservationTypeIDs = TreatmentBMPBenchmarkAndThresholds.Select(x => x.ObservationTypeID).ToList();

            return !observationTypesIDs.Except(benchmarkAndThresholdObservationTypeIDs).Any();
        }

        public bool HasSettableBenchmarkAndThresholdValues()
        {
            return TreatmentBMPType.GetObservationTypes().Any(x => x.HasBenchmarkAndThreshold);
        }

        public string GetMostRecentScoreAsString()
        {
            var latestAssessment = GetMostRecentAssessment();
            if (latestAssessment == null)
            {
                return string.Empty;
            }
            return latestAssessment.AlternateAssessmentScore.HasValue ? latestAssessment.AlternateAssessmentScore.ToString() : latestAssessment.FormattedScore();
        }

        public TreatmentBMPAssessment GetMostRecentAssessment()
        {
            var latestAssessment = TreatmentBMPAssessments.OrderByDescending(x => x.AssessmentDate).FirstOrDefault(x => x.HasCalculatedOrAlternateScore());
            return latestAssessment;
        }     

        public double? GetWetBasinThresholdValueInObservedUnits(TreatmentBMPObservation treatmentBMPObservation)
        {
            var observationValue = treatmentBMPObservation.GetObservationValue(treatmentBMPObservation.ObservationType);
            var benchmarkValue = treatmentBMPObservation.ObservationType.GetBenchmarkValue(this).Value;
            var thresholdValue = treatmentBMPObservation.ObservationType.GetThresholdValue(this).Value;
            return observationValue < benchmarkValue ? benchmarkValue - thresholdValue : benchmarkValue + thresholdValue;
        }

        public bool HasDependentObjectsBesidesBenchmarksAndThresholds()
        {
            return TreatmentBMPAssessments.Any();
        }

        public bool IsBenchmarkAndThresholdCompleteForObservationType(ObservationType observationType)
        {
            return TreatmentBMPBenchmarkAndThresholds.SingleOrDefault(x =>
                       x.ObservationTypeID == observationType.ObservationTypeID) != null;
        }
    }
}
