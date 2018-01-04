using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ObservationType
{
    public class ViewPercentageSchemaDetailViewData
    {
        public PercentageSchema PercentageSchema { get; }
        public string PropertiesToObserveFormatted { get; }
        public MeasurementUnitType MeasurementUnitType { get; }
        public ViewPercentageSchemaDetailViewData(PercentageSchema discreteValueSchema)
        {
            PercentageSchema = discreteValueSchema;
            PropertiesToObserveFormatted = string.Join(", ", discreteValueSchema.PropertiesToObserve.OrderBy(x => x));
            MeasurementUnitType = MeasurementUnitType.Percent;
        }        
    }
}
