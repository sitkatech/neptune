using System;
using System.Linq;

namespace Neptune.Web.Models
{
    public partial class MaintenanceRecord
    {
        public DateTime GetMaintenanceRecordDate {get {return FieldVisit.VisitDate; } } 
        public Person GetMaintenanceRecordPerson => FieldVisit.PerformedByPerson;
        public Organization GetMaintenanceRecordOrganization => FieldVisit.PerformedByPerson.Organization;

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
                MaintenanceRecordObservations.Count(x => x.CustomAttributeType.IsRequired && IsObservationComplete(x));
            var totalObservationCount =
                MaintenanceRecordObservations.Count(x => x.CustomAttributeType.IsRequired);

            return !IsMissingRequiredAttributes
                ? "All Required Data Provided"
                : $"In Progress ({completedObservationCount} of {totalObservationCount} required observations complete)";
        }
    }
}