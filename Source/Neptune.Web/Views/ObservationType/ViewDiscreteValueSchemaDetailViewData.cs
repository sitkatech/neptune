using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ObservationType
{
    public class ViewDiscreteValueSchemaDetailViewData
    {
        public DiscreteValueSchema DiscreteValueSchema { get; }
        public string PropertiesToObserveFormatted { get; }
        public MeasurementUnitType MeasurementUnitType { get; }
        public ViewDiscreteValueSchemaDetailViewData(DiscreteValueSchema discreteValueSchema)
        {
            DiscreteValueSchema = discreteValueSchema;
            PropertiesToObserveFormatted = string.Join(", ", discreteValueSchema.PropertiesToObserve.OrderBy(x => x));
            MeasurementUnitType = MeasurementUnitType.All.Find(x =>
                x.MeasurementUnitTypeID == discreteValueSchema.MeasurementUnitTypeID);
        }        
    }
}
