﻿using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAssessmentObservationType
{
    public class ViewPercentageSchemaDetailViewData
    {
        public PercentageObservationTypeSchema PercentageSchema { get; }
        public string PropertiesToObserveFormatted { get; }
        public MeasurementUnitType MeasurementUnitType { get; }
        public ViewPercentageSchemaDetailViewData(PercentageObservationTypeSchema schema)
        {
            PercentageSchema = schema;
            PropertiesToObserveFormatted = string.Join(", ", schema.PropertiesToObserve.OrderBy(x => x));
            MeasurementUnitType = MeasurementUnitType.Percent;
        }        
    }
}
