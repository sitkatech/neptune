using System;
using System.Linq;

namespace Neptune.Web.Models
{
    public partial class MaintenanceRecord : IAuditableEntity
    {
        public DateTime? GetMaintenanceRecordDate()
        {
            return FieldVisit?.VisitDate;
        }

        public Person GetMaintenanceRecordPerson()
        {
            return FieldVisit?.PerformedByPerson;
        }

        public Organization GetMaintenanceRecordOrganization()
        {
            return FieldVisit?.PerformedByPerson.Organization;
        }

        public bool IsMissingRequiredAttributes()
        {
            return MaintenanceRecordObservations.Any(x =>
                x.CustomAttributeType.IsRequired && !x.IsObservationComplete());
        }

        public string MaintenanceRecordStatus()
        {
            var completedObservationCount =
                MaintenanceRecordObservations.Count(MaintenanceRecordObservationModelExtensions.IsObservationComplete);
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

        public string GetAuditDescriptionString()
        {
            return $"Maintenance Record dated {GetMaintenanceRecordDate().GetValueOrDefault().ToShortDateString()}";
        }
    }
}