using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.WebMvc.Views.TreatmentBMPAssessmentObservationType
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
