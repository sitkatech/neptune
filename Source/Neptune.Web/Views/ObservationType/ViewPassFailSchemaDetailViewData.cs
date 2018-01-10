using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.ObservationType
{
    public class ViewPassFailSchemaDetailViewData
    {
        public PassFailObservationTypeSchema PassFailSchema { get; }
        public string PropertiesToObserveFormatted { get; }
        public MeasurementUnitType MeasurementUnitType { get; }
        public ViewPassFailSchemaDetailViewData(PassFailObservationTypeSchema schema)
        {
            PassFailSchema = schema;
            PropertiesToObserveFormatted = string.Join(", ", schema.PropertiesToObserve.OrderBy(x => x));            
        }        
    }
}
