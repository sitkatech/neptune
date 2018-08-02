using System;
using System.Linq;

namespace Neptune.Web.Models
{
    public static class MaintenanceRecordObservationModelExtensions
    {
        public static bool IsObservationComplete(this MaintenanceRecordObservation maintenanceRecordObservation)
        {
            return maintenanceRecordObservation.MaintenanceRecordObservationValues != null && !maintenanceRecordObservation.MaintenanceRecordObservationValues.All(y =>
                       String.IsNullOrWhiteSpace(y.ObservationValue));
        }
    }
}