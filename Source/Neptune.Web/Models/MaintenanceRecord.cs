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
                x.CustomAttributeType.IsRequired && (x.MaintenanceRecordObservationValues == null ||
                                                     x.MaintenanceRecordObservationValues.All(y =>
                                                         string.IsNullOrWhiteSpace(y.ObservationValue))));
    }
}