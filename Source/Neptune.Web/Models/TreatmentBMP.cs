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
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMP : IAuditableEntity, IHaveHRUCharacteristics
    {
        public bool CanView(Person person)
        {
            return person.IsAssignedToStormwaterJurisdiction(StormwaterJurisdictionID);
        }

        public bool CanEdit(Person person)
        {
            return person.IsAssignedToStormwaterJurisdiction(StormwaterJurisdictionID);
        }

        public bool CanDelete(Person person)
        {
            return person.IsManagerOrAdmin() && person.IsAssignedToStormwaterJurisdiction(StormwaterJurisdictionID);
        }

        public string GetAuditDescriptionString()
        {
            return TreatmentBMPName;
        }

        public FieldVisit GetInProgressFieldVisit()
        {
            return FieldVisits.SingleOrDefault(x =>
                x.FieldVisitStatusID == FieldVisitStatus.InProgress.FieldVisitStatusID);
        }

        public bool IsBenchmarkAndThresholdsComplete()
        {
            var observationTypesIDs = TreatmentBMPType.GetObservationTypes().Where(x => x.GetHasBenchmarkAndThreshold()).Select(x => x.TreatmentBMPAssessmentObservationTypeID).ToList();
            var benchmarkAndThresholdObservationTypeIDs = TreatmentBMPBenchmarkAndThresholds.Select(x => x.TreatmentBMPAssessmentObservationTypeID).ToList();

            return !observationTypesIDs.Except(benchmarkAndThresholdObservationTypeIDs).Any();
        }

        public bool HasSettableBenchmarkAndThresholdValues()
        {
            return TreatmentBMPType.GetObservationTypes().Any(x => x.GetHasBenchmarkAndThreshold());
        }

        public string GetMostRecentScoreAsString()
        {
            var latestAssessment = GetMostRecentAssessment();
            if (latestAssessment == null)
            {
                return string.Empty;
            }
            return latestAssessment.FormattedScore();
        }

        public TreatmentBMPAssessment GetMostRecentAssessment()
        {
            var latestAssessment = TreatmentBMPAssessments.OrderByDescending(x => x.GetAssessmentDate()).FirstOrDefault(x => x.IsAssessmentComplete);
            return latestAssessment;
        }

        public bool IsBenchmarkAndThresholdCompleteForObservationType(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return TreatmentBMPBenchmarkAndThresholds.SingleOrDefault(x =>
                       x.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID) != null;
        }

        public string GetCustomAttributeValueWithUnits(TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType)
        {
            if (CustomAttributes.Any())
            {
                var customAttribute = CustomAttributes.SingleOrDefault(x =>
                    x.CustomAttributeTypeID == treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID);
                if (customAttribute != null)
                {
                    var measurementUnit = "";
                    if (customAttribute.CustomAttributeType.MeasurementUnitTypeID.HasValue)
                    {
                        measurementUnit = $" {customAttribute.CustomAttributeType.MeasurementUnitType.LegendDisplayName}";
                    }

                    var value = string.Join(", ", customAttribute.CustomAttributeValues.OrderBy(x => x.AttributeValue).Select(x => x.AttributeValue));

                    return $"{value}{measurementUnit}";
                }           
            }
            return string.Empty;
        }

        public DateTime? LastMaintainedDateTime()
        {
            if (!MaintenanceRecords.Any())
            {
                return null;
            }

            return MaintenanceRecords.Max(x => x.GetMaintenanceRecordDate());
        }

        public string CustomAttributeStatus()
        {
            var nonMaintenanceTreatmentBMPTypeCustomAttributeTypes =
                TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Where(x =>
                    x.CustomAttributeType.CustomAttributeTypePurpose != CustomAttributeTypePurpose.Maintenance).ToList();

            var completedObservationCount = nonMaintenanceTreatmentBMPTypeCustomAttributeTypes.Count(x =>
                x.CustomAttributeType.IsRequired && x.CustomAttributeType.IsCompleteForTreatmentBMP(this));

            var totalObservationCount = nonMaintenanceTreatmentBMPTypeCustomAttributeTypes.Count(x =>
                x.CustomAttributeType.IsRequired);

            return completedObservationCount == totalObservationCount
                ? "All Required Data Provided"
                : $"In Progress ({completedObservationCount} of {totalObservationCount} required attributes entered)";
        }

        public bool RequiredAttributeDoesNotHaveValue()
        {
            return TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Any(x =>
                x.CustomAttributeType.IsRequired && x.CustomAttributeType.CustomAttributeTypePurpose !=
                CustomAttributeTypePurpose.Maintenance &&
                !x.CustomAttributeType.IsCompleteForTreatmentBMP(this)
            );
        }

        public void MarkInventoryAsProvisionalIfNonManager(Person person)
        {
            var isAssignedToStormwaterJurisdiction = person.CanManageStormwaterJurisdiction(StormwaterJurisdictionID);
            if (!isAssignedToStormwaterJurisdiction)
            {
                InventoryIsVerified = false;
            }
            InventoryLastChangedDate = DateTime.Now;
        }

        public void MarkAsVerified(Person currentPerson)
        {
            InventoryIsVerified = true;
            DateOfLastInventoryVerification = DateTime.Now;
            InventoryVerifiedByPersonID = currentPerson.PersonID;
        }

        public HtmlString GetDelineationTypeDisplay()
        {
            var delineationType = Delineation?.DelineationType;
            return delineationType != null
                ? new HtmlString(delineationType?.DelineationTypeDisplayName)
                : new HtmlString("<p class='systemText'>No Delineation Provided</p>");
        }

        public DbGeometry GetCatchmentGeometry()
        {
            return Delineation?.DelineationGeometry;
        }

        public void RemoveUpstreamBMP()
        { 
            this.UpstreamBMPID = null;
        }

        public IEnumerable<HRUCharacteristic> GetHRUCharacteristics()
        {
            if (Delineation == null)
            {
                return new List<HRUCharacteristic>();
            }

            if (Delineation.DelineationType == DelineationType.Centralized)
            {
                var upstreamRegionalSubbasinIDs = this.GetRegionalSubbasin().TraceUpstreamCatchmentsReturnIDList();
                return HttpRequestStorage.DatabaseEntities.HRUCharacteristics.Where(x =>
                    x.LoadGeneratingUnit.RegionalSubbasinID != null &&
                    upstreamRegionalSubbasinIDs.Contains(x.LoadGeneratingUnit.RegionalSubbasinID.Value));
            }

            else
            {
                return HttpRequestStorage.DatabaseEntities.HRUCharacteristics.Where(x =>
                    x.LoadGeneratingUnit.Delineation!= null && x.LoadGeneratingUnit.Delineation.TreatmentBMPID == TreatmentBMPID);
            }
        }
    }
}
