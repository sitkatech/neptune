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

using Neptune.Web.Models;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMP : IAuditableEntity//, IHaveHRUCharacteristics
    {
        public bool CanView(Person person)
        {
            return ProjectID == null && person.IsAssignedToStormwaterJurisdiction(StormwaterJurisdictionID);
        }

        public bool CanEdit(Person person)
        {
            return ProjectID == null && person.IsAssignedToStormwaterJurisdiction(StormwaterJurisdictionID);
        }

        public bool CanDelete(Person person)
        {
            return ProjectID == null && person.IsManagerOrAdmin() && person.IsAssignedToStormwaterJurisdiction(StormwaterJurisdictionID);
        }

        public string GetAuditDescriptionString()
        {
            return TreatmentBMPName;
        }

        public FieldVisit GetInProgressFieldVisit()
        {
            return new FieldVisit();
            // todo:
            //return FieldVisits.SingleOrDefault(x =>
            //    x.FieldVisitStatusID == FieldVisitStatus.InProgress.FieldVisitStatusID);
        }

        public bool IsBenchmarkAndThresholdsComplete()
        {
            return true;
            // TODO:
            //var observationTypesIDs = TreatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
            //    .Where(x => x.TreatmentBMPAssessmentObservationType.GetHasBenchmarkAndThreshold())
            //    .Select(x => x.TreatmentBMPAssessmentObservationTypeID).ToList();
            //var benchmarkAndThresholdObservationTypeIDs = TreatmentBMPBenchmarkAndThresholdTreatmentBMPs.Select(x => x.TreatmentBMPAssessmentObservationTypeID).ToList();

            //return !observationTypesIDs.Except(benchmarkAndThresholdObservationTypeIDs).Any();
        }

        public bool HasSettableBenchmarkAndThresholdValues()
        {
            return true;
            // TODO:
            //return TreatmentBMPType.GetObservationTypes().Any(x => x.GetHasBenchmarkAndThreshold());
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
            var latestAssessment = TreatmentBMPAssessmentTreatmentBMPs.OrderByDescending(x => x.GetAssessmentDate()).FirstOrDefault(x => x.IsAssessmentComplete);
            return latestAssessment;
        }

        public bool IsBenchmarkAndThresholdCompleteForObservationType(TreatmentBMPAssessmentObservationType treatmentBMPAssessmentObservationType)
        {
            return TreatmentBMPBenchmarkAndThresholdTreatmentBMPs.SingleOrDefault(x =>
                       x.TreatmentBMPAssessmentObservationTypeID == treatmentBMPAssessmentObservationType.TreatmentBMPAssessmentObservationTypeID) != null;
        }

        public string GetCustomAttributeValueWithUnits(TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType)
        {
            if (this.CustomAttributeTreatmentBMPs.Any())
            {
                var customAttribute = CustomAttributeTreatmentBMPs.SingleOrDefault(x =>
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

        public string GetCustomAttributeValue(TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType)
        {
            if (CustomAttributeTreatmentBMPs.Any())
            {
                var customAttribute = CustomAttributeTreatmentBMPs.SingleOrDefault(x =>
                    x.CustomAttributeTypeID == treatmentBMPTypeCustomAttributeType.CustomAttributeTypeID);
                if (customAttribute != null)
                {
                    return string.Join(", ", customAttribute.CustomAttributeValues.OrderBy(x => x.AttributeValue).Select(x => x.AttributeValue));
                }
            }
            return string.Empty;
        }

        public DateTime? LastMaintainedDateTime()
        {
            if (!MaintenanceRecordTreatmentBMPs.Any())
            {
                return null;
            }

            return MaintenanceRecordTreatmentBMPs.Max(x => x.GetMaintenanceRecordDate());
        }

        public string CustomAttributeStatus()
        {
            return string.Empty;
            //todo:
            //var nonMaintenanceTreatmentBMPTypeCustomAttributeTypes =
            //    TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Where(x =>
            //        x.CustomAttributeType.CustomAttributeTypePurpose != CustomAttributeTypePurpose.Maintenance).ToList();

            //var completedObservationCount = nonMaintenanceTreatmentBMPTypeCustomAttributeTypes.Count(x =>
            //    x.CustomAttributeType.IsRequired && x.CustomAttributeType.IsCompleteForTreatmentBMP(this));

            //var totalObservationCount = nonMaintenanceTreatmentBMPTypeCustomAttributeTypes.Count(x =>
            //    x.CustomAttributeType.IsRequired);

            //return completedObservationCount == totalObservationCount
            //    ? "All Required Data Provided"
            //    : $"In Progress ({completedObservationCount} of {totalObservationCount} required attributes entered)";
        }

        public bool RequiredAttributeDoesNotHaveValue()
        {
            return false;
            //return TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Any(x =>
            //x.CustomAttributeType.IsRequired && x.CustomAttributeType.CustomAttributeTypePurpose !=
            //    CustomAttributeTypePurpose.Maintenance &&
            //    !x.CustomAttributeType.IsCompleteForTreatmentBMP(this)
            //);
        }

        public void MarkInventoryAsProvisionalIfNonManager(Person person)
        {
            // todo:
            //var isAssignedToStormwaterJurisdiction = person.CanManageStormwaterJurisdiction(StormwaterJurisdictionID);
            //if (!isAssignedToStormwaterJurisdiction)
            //{
            //    InventoryIsVerified = false;
            //}
            //InventoryLastChangedDate = DateTime.Now;
        }

        public void MarkAsVerified(Person currentPerson)
        {
            InventoryIsVerified = true;
            DateOfLastInventoryVerification = DateTime.Now;
            InventoryVerifiedByPersonID = currentPerson.PersonID;
        }

        public Geometry GetCatchmentGeometry()
        {
            return Delineation?.DelineationGeometry;
        }

        public void RemoveUpstreamBMP()
        { 
            this.UpstreamBMPID = null;
        }

        public IEnumerable<HRUCharacteristic> GetHRUCharacteristics()
        {
            return new List<HRUCharacteristic>();
            //if (Delineation == null)
            //{
            //    return new List<HRUCharacteristic>();
            //}

            //if (Delineation.DelineationType == DelineationType.Centralized && TreatmentBMPType.TreatmentBMPModelingType != null)
            //{
            //    var catchmentRegionalSubbasins = this.GetRegionalSubbasin().TraceUpstreamCatchmentsReturnIDList(HttpRequestStorage.DatabaseEntities);

            //    catchmentRegionalSubbasins.Add(RegionalSubbasinID.GetValueOrDefault());

            //    return HttpRequestStorage.DatabaseEntities.HRUCharacteristics.Where(x =>
            //        x.LoadGeneratingUnit.RegionalSubbasinID != null &&
            //        catchmentRegionalSubbasins.Contains(x.LoadGeneratingUnit.RegionalSubbasinID.Value));
            //}

            //else
            //{
            //    return HttpRequestStorage.DatabaseEntities.HRUCharacteristics.Where(x =>
            //        x.LoadGeneratingUnit.Delineation!= null && x.LoadGeneratingUnit.Delineation.TreatmentBMPID == TreatmentBMPID);
            //}
        }
    }
}
