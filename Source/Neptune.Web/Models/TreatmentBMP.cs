/*-----------------------------------------------------------------------
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

using System;
using System.Linq;
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
            return new JurisdictionManageFeature().HasPermissionByPerson(person);
        }

        public bool CanDelete(Person person)
        {
            return new TreatmentBMPDeleteFeature().HasPermission(person, this).HasPermission;
        }
       
        public string AuditDescriptionString => TreatmentBMPName;
        public FieldVisit InProgressFieldVisit => FieldVisits.SingleOrDefault(x => x.FieldVisitStatusID == FieldVisitStatus.InProgress.FieldVisitStatusID);

        public bool IsBenchmarkAndThresholdsComplete()
        {
            var observationTypesIDs = TreatmentBMPType.GetObservationTypes().Where(x => x.HasBenchmarkAndThreshold).Select(x => x.TreatmentBMPAssessmentObservationTypeID).ToList();
            var benchmarkAndThresholdObservationTypeIDs = TreatmentBMPBenchmarkAndThresholds.Select(x => x.TreatmentBMPAssessmentObservationTypeID).ToList();

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

        public bool HasDependentObjectsBesidesBenchmarksAndThresholds()
        {
            return TreatmentBMPAssessments.Any();
        }

        public bool IsBenchmarkAndThresholdCompleteForObservationType(TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType)
        {
            return TreatmentBMPBenchmarkAndThresholds.SingleOrDefault(x =>
                       x.TreatmentBMPAssessmentObservationTypeID == TreatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID) != null;
        }

        public string GetCustomAttributeValueWithUnits(TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType)
        {
            if (CustomAttributes.Any())
            {
                var customAttribute = CustomAttributes.SingleOrDefault(x =>
                    x.CustomAttributeTypeID == treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID);
                if (customAttribute != null)
                {
                    var measurmentUnit = "";
                    if (customAttribute.CustomAttributeType.MeasurementUnitTypeID.HasValue)
                    {
                        measurmentUnit = $" {customAttribute.CustomAttributeType.MeasurementUnitType.LegendDisplayName}";
                    }

                    var value = string.Join(", ", customAttribute.CustomAttributeValues.OrderBy(x => x.AttributeValue).Select(x => x.AttributeValue));

                    return $"{value}{measurmentUnit}";
                }           
            }
            return string.Empty;
        }

        public DateTime? LastMaintainedDateTime()
        {
            return !MaintenanceRecords.Any()
                ? null
                : (DateTime?) MaintenanceRecords.Select(x => x.MaintenanceRecordDate)
                    .Max();
        }

        public string LastMaintainedDateAsString()
        {
            return !MaintenanceRecords.Any()
                ? "Never"
                : MaintenanceRecords.Select(x => x.MaintenanceRecordDate)
                    .Max().ToString("g");

        }
    }
}
