using System;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Views;

namespace Neptune.Web.Models
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
                MaintenanceRecordObservations.Count(MaintenanceRecordObservationModelExtensions.IsObservationComplete);
            var totalObservationCount =
                MaintenanceRecordObservations.Count;

            return $"{completedObservationCount} of {totalObservationCount} observations provided";
        }

        public string GetObservationValueForAttributeType(CustomAttributeType customAttributeType)
        {
            if (customAttributeType.TreatmentBMPTypeCustomAttributeTypes.All(x => x.TreatmentBMPTypeID != TreatmentBMPTypeID))
            {
                return ViewUtilities.NaString;
            }

            var maintenanceRecordObservation = MaintenanceRecordObservations.SingleOrDefault(y =>
                y.CustomAttributeTypeID == customAttributeType.CustomAttributeTypeID &&
                y.MaintenanceRecordObservationValues.Any(z =>
                    !string.IsNullOrWhiteSpace(z.ObservationValue)));
            return maintenanceRecordObservation?.GetObservationValueWithoutUnits() ?? ViewUtilities.NotProvidedString;
        }

        public string GetAuditDescriptionString()
        {
            return $"Maintenance Record dated {GetMaintenanceRecordDate().ToStringDate()}";
        }
    }
}