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

using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    public partial class TreatmentBMP
    {
        public double Longitude => LocationPoint4326.Coordinate.X;
        public double Latitude => LocationPoint4326.Coordinate.Y;
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

        public bool IsBenchmarkAndThresholdsComplete(TreatmentBMPType treatmentBMPType)
        {
            var observationTypesIDs = treatmentBMPType.TreatmentBMPTypeAssessmentObservationTypes
                .Where(x => x.TreatmentBMPAssessmentObservationType.GetHasBenchmarkAndThreshold())
                .Select(x => x.TreatmentBMPAssessmentObservationTypeID).ToList();
            var benchmarkAndThresholdObservationTypeIDs = TreatmentBMPBenchmarkAndThresholds.Select(x => x.TreatmentBMPAssessmentObservationTypeID).ToList();

            return !observationTypesIDs.Except(benchmarkAndThresholdObservationTypeIDs).Any();
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

        public string GetCustomAttributeValueWithUnits(TreatmentBMPTypeCustomAttributeType treatmentBMPTypeCustomAttributeType, ICollection<CustomAttribute> customAttributes)
        {
            if (customAttributes.Any())
            {
                var customAttribute = customAttributes.SingleOrDefault(x =>
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

        public void MarkAsVerified(Person currentPerson)
        {
            InventoryIsVerified = true;
            DateOfLastInventoryVerification = DateTime.Now;
            InventoryVerifiedByPersonID = currentPerson.PersonID;
        }

        public async Task RemoveUpstreamBMP(NeptuneDbContext dbContext)
        { 
            UpstreamBMPID = null;
            await dbContext.SaveChangesAsync();

            //todo:
            // need to re-execute the Nereid model here since source of run-off was removed.
            //NereidUtilities.MarkTreatmentBMPDirty(treatmentBMP, _dbContext);
        }

        public void DeleteFull(NeptuneDbContext dbContext)
        {
            //todo:
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectHRUCharacteristic> GetHRUCharacteristics(NeptuneDbContext dbContext)
        {
            if (Delineation == null)
            {
                return new List<ProjectHRUCharacteristic>();
            }

            if (Delineation.DelineationTypeID == (int)DelineationType.DelineationTypeEnum.Centralized && TreatmentBMPType.TreatmentBMPModelingTypeID != null)
            {
                var catchmentRegionalSubbasins = GetRegionalSubbasin(dbContext).TraceUpstreamCatchmentsReturnIDList(dbContext);

                catchmentRegionalSubbasins.Add(RegionalSubbasinID.GetValueOrDefault());

                return dbContext.ProjectHRUCharacteristics
                    .Include(x => x.ProjectLoadGeneratingUnit)
                    .Include(x => x.HRUCharacteristicLandUseCode)
                    .Where(x =>
                        x.ProjectID == ProjectID &&
                        x.ProjectLoadGeneratingUnit.RegionalSubbasinID != null &&
                        catchmentRegionalSubbasins.Contains(x.ProjectLoadGeneratingUnit.RegionalSubbasinID.Value));
            }

            else
            {
                return dbContext.ProjectHRUCharacteristics
                    .Include(x => x.ProjectLoadGeneratingUnit)
                    .ThenInclude(x => x.Delineation)
                    .Include(x => x.HRUCharacteristicLandUseCode)
                    .Where(x =>
                        x.ProjectID == ProjectID &&
                        x.ProjectLoadGeneratingUnit.Delineation != null &&
                        x.ProjectLoadGeneratingUnit.Delineation.TreatmentBMPID == TreatmentBMPID);
            }
        }

        public RegionalSubbasin GetRegionalSubbasin(NeptuneDbContext dbContext)
        {
            return dbContext.RegionalSubbasins.SingleOrDefault(x =>
                x.CatchmentGeometry.Contains(LocationPoint));
        }
    }
}
