using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class MaintenanceRecord : IAuditableEntity
    {
        public DateTime GetMaintenanceRecordDate()
        {
            return FieldVisit.VisitDate;
        }

        public Person GetMaintenanceRecordPerson()
        {
            return FieldVisit.PerformedByPerson;
        }

        public Organization GetMaintenanceRecordOrganization()
        {
            return FieldVisit.PerformedByPerson.Organization;
        }

        public bool IsMissingRequiredAttributes()
        {
            return MaintenanceRecordObservations.Any(x => x.CustomAttributeType.IsRequired && !x.IsObservationComplete());
        }

        public string MaintenanceRecordStatus()
        {
            var completedObservationCount =
                MaintenanceRecordObservations.Count(x => x.IsObservationComplete());
            var totalObservationCount =
                MaintenanceRecordObservations.Count;

            return $"{completedObservationCount} of {totalObservationCount} observations provided";
        }

        public string GetObservationValueForAttributeType(CustomAttributeType customAttributeType)
        {
            if (customAttributeType.TreatmentBMPTypeCustomAttributeTypes.All(x => x.TreatmentBMPTypeID != TreatmentBMPTypeID))
            {
                return "n/a";
            }

            var maintenanceRecordObservation = MaintenanceRecordObservations.SingleOrDefault(y =>
                y.CustomAttributeTypeID == customAttributeType.CustomAttributeTypeID &&
                y.MaintenanceRecordObservationValues.Any(z =>
                    !string.IsNullOrWhiteSpace(z.ObservationValue)));
            return maintenanceRecordObservation?.GetObservationValueWithoutUnits() ?? "not provided";
        }

        public string GetAuditDescriptionString()
        {
            return $"Maintenance Record dated {GetMaintenanceRecordDate():MM/dd/yyyy}";
        }
    }
}