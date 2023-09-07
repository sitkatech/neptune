using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
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
