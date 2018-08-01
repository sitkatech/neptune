using System;
using System.Linq;

namespace Neptune.Web.Models
{
    public partial class MaintenanceRecordObservation
    {
        public string GetObservationValueWithUnits()
        {
            var customAttributeType = CustomAttributeType;

            var measurmentUnit = "";
            if (customAttributeType.MeasurementUnitTypeID.HasValue)
            {
                measurmentUnit = $" {customAttributeType.MeasurementUnitType.LegendDisplayName}";
            }

            var value = String.Join(", ",
                MaintenanceRecordObservationValues.OrderBy(x => x.ObservationValue).Select(x => x.ObservationValue));

            return $"{value}{measurmentUnit}";
        }
    }
}