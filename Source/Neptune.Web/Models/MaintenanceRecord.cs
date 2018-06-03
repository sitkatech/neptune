using System;
using System.Linq;

namespace Neptune.Web.Models
{
    public partial class MaintenanceRecord
    {
        public DateTime GetMaintenanceRecordDate {get {return FieldVisit.VisitDate; } } 
        public Person GetMaintenanceRecordPerson => FieldVisit.PerformedByPerson;
        public Organization GetMaintenanceRecordOrganization => FieldVisit.PerformedByPerson.Organization;

    }
}