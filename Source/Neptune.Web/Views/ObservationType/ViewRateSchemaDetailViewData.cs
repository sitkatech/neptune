using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ObservationType
{
    public class ViewRateSchemaDetailViewData
    {
        public RateSchema RateSchema { get; }
        public string PropertiesToObserveFormatted { get; }
        public MeasurementUnitType DiscreteRateMeasurementUnit { get; }
        public MeasurementUnitType ReadingMeasurementUnit { get; }
        public MeasurementUnitType TimeMeasurementUnit { get; }
        public ViewRateSchemaDetailViewData(RateSchema discreteValueSchema)
        {
            RateSchema = discreteValueSchema;
            PropertiesToObserveFormatted = string.Join(", ", discreteValueSchema.PropertiesToObserve.OrderBy(x => x));
            DiscreteRateMeasurementUnit = MeasurementUnitType.All.Find(x => x.MeasurementUnitTypeID == discreteValueSchema.DiscreteRateMeasurementUnitTypeID);
            ReadingMeasurementUnit = MeasurementUnitType.All.Find(x => x.MeasurementUnitTypeID == discreteValueSchema.ReadingMeasurementUnitTypeID);
            TimeMeasurementUnit = MeasurementUnitType.All.Find(x => x.MeasurementUnitTypeID == discreteValueSchema.TimeMeasurementUnitTypeID);
        }        
    }
}
