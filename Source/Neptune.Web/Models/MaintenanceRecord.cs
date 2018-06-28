using System;
using System.Linq;

namespace Neptune.Web.Models
{
    public partial class MaintenanceRecord
    {
        public DateTime? GetMaintenanceRecordDate => FieldVisit?.VisitDate;
        public Person GetMaintenanceRecordPerson => FieldVisit?.PerformedByPerson;
        public Organization GetMaintenanceRecordOrganization => FieldVisit?.PerformedByPerson.Organization;

        public bool IsMissingRequiredAttributes =>
            MaintenanceRecordObservations.Any(x =>
                x.CustomAttributeType.IsRequired && !IsObservationComplete(x));

        private static bool IsObservationComplete(MaintenanceRecordObservation maintenanceRecordObservation)
        {
            return maintenanceRecordObservation.MaintenanceRecordObservationValues != null && !maintenanceRecordObservation.MaintenanceRecordObservationValues.All(y =>
                       string.IsNullOrWhiteSpace(y.ObservationValue));
        }

        public string MaintenanceRecordStatus()
        {
            var completedObservationCount =
                MaintenanceRecordObservations.Count(IsObservationComplete);
            var totalObservationCount =
                MaintenanceRecordObservations.Count;

            return $"{completedObservationCount} of {totalObservationCount} observations provided";
        }

        public string GetObservationValueWithUnitsForAttributeType(CustomAttributeType customAttributeType)
        {
            if (!TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Select(x=>x.CustomAttributeType).Contains(customAttributeType))
                return "N/A";
            return MaintenanceRecordObservations?
                       .SingleOrDefault(y =>
                           y.CustomAttributeTypeID == customAttributeType.CustomAttributeTypeID &&
                           y.MaintenanceRecordObservationValues.Any(z =>
                               !string.IsNullOrWhiteSpace(z.ObservationValue)))?
                       .GetObservationValueWithUnits() ?? "Not Provided";
        }
    }
}