using System.Linq;

namespace Neptune.Web.Models
{
    public partial class MaintenanceRecordObservation
    {
        public string GetObservationValueWithoutUnits()
        {
            return string.Join(", ",
                MaintenanceRecordObservationValues.OrderBy(x => x.ObservationValue).Select(x => x.ObservationValue));
        }
    }
}