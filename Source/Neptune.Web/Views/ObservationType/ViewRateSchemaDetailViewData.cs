using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ObservationType
{
    public class ViewRateSchemaDetailViewData
    {
        public RateObservationTypeSchema RateObservationTypeSchema { get; }
        public string PropertiesToObserveFormatted { get; }
        public MeasurementUnitType DiscreteRateMeasurementUnit { get; }
        public MeasurementUnitType ReadingMeasurementUnit { get; }
        public MeasurementUnitType TimeMeasurementUnit { get; }
        public ViewRateSchemaDetailViewData(RateObservationTypeSchema schema)
        {
            RateObservationTypeSchema = schema;
            PropertiesToObserveFormatted = string.Join(", ", schema.PropertiesToObserve.OrderBy(x => x));
            DiscreteRateMeasurementUnit = MeasurementUnitType.All.Find(x => x.MeasurementUnitTypeID == schema.DiscreteRateMeasurementUnitTypeID);
            ReadingMeasurementUnit = MeasurementUnitType.All.Find(x => x.MeasurementUnitTypeID == schema.ReadingMeasurementUnitTypeID);
            TimeMeasurementUnit = MeasurementUnitType.All.Find(x => x.MeasurementUnitTypeID == schema.TimeMeasurementUnitTypeID);
        }        
    }
}
