using System;
using System.Linq;

namespace Neptune.Web.Models
{
    public partial class MaintenanceRecord
    {
        public FieldVisit GetFieldVisit()
        {
            return FieldVisits.SingleOrDefault();
        }

        public DateTime GetMaintenanceRecordDate => GetFieldVisit().VisitDate;
        public virtual Person GetMaintenanceRecordPerson => GetFieldVisit().PerformedByPerson;
        public virtual Organization GetMaintenanceRecordOrganization => GetFieldVisit().PerformedByPerson.Organization;

    }
}