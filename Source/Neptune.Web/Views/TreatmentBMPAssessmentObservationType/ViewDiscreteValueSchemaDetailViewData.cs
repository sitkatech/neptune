using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
    public class ViewDiscreteValueSchemaDetailViewData
    {
        public DiscreteObservationTypeSchema DiscreteObservationTypeSchema { get; }
        public string PropertiesToObserveFormatted { get; }
        public MeasurementUnitType MeasurementUnitType { get; }
        public ViewDiscreteValueSchemaDetailViewData(DiscreteObservationTypeSchema schema)
        {
            DiscreteObservationTypeSchema = schema;
            PropertiesToObserveFormatted = string.Join(", ", schema.PropertiesToObserve.OrderBy(x => x));
            MeasurementUnitType = MeasurementUnitType.All.Find(x =>
                x.MeasurementUnitTypeID == schema.MeasurementUnitTypeID);
        }        
    }
}
