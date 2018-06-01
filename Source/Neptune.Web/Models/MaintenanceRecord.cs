using System.Linq;

namespace Neptune.Web.Models
{
    public partial class MaintenanceRecord
    {
        public FieldVisit GetFieldVisit()
        {
            return FieldVisits.SingleOrDefault();
        }
    }
}