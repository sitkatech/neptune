using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class MaintenanceRecordObservation
    {
        public string GetObservationValueWithoutUnits()
        {
            return string.Join(", ",
                MaintenanceRecordObservationValues.OrderBy(x => x.ObservationValue).Select(x => x.ObservationValue));
        }

        public bool IsObservationComplete()
        {
            return !MaintenanceRecordObservationValues.All(y =>
                string.IsNullOrWhiteSpace(y.ObservationValue));
        }
    }
}