using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ObservationType
{
    public class ViewPassFailSchemaDetailViewData
    {
        public PassFailSchema PassFailSchema { get; }
        public string PropertiesToObserveFormatted { get; }
        public MeasurementUnitType MeasurementUnitType { get; }
        public ViewPassFailSchemaDetailViewData(PassFailSchema discreteValueSchema)
        {
            PassFailSchema = discreteValueSchema;
            PropertiesToObserveFormatted = string.Join(", ", discreteValueSchema.PropertiesToObserve.OrderBy(x => x));            
        }        
    }
}
