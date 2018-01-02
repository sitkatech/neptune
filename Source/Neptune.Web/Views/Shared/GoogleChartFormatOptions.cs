using System;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Views.Shared
{
    public class GoogleChartFormatOptions
    {
        [JsonProperty(PropertyName = "source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }
        [JsonProperty(PropertyName = "prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string Prefix { get; set; }
        [JsonProperty(PropertyName = "suffix", NullValueHandling = NullValueHandling.Ignore)]
        public string Suffix { get; set; }

        public GoogleChartFormatOptions(MeasurementUnitTypeEnum? measurementUnitTypeEnum)
        {
            switch (measurementUnitTypeEnum)
            {
                case MeasurementUnitTypeEnum.Percent:
                    Suffix = "%";
                    break;
                case MeasurementUnitTypeEnum.Acres:
                case MeasurementUnitTypeEnum.SquareFeet:
                case MeasurementUnitTypeEnum.Kilogram:
                case MeasurementUnitTypeEnum.Number:
                case MeasurementUnitTypeEnum.MilligamsPerLiter:
                case MeasurementUnitTypeEnum.Meters:
                case MeasurementUnitTypeEnum.Feet:
                case MeasurementUnitTypeEnum.InchesPerHour:
                case MeasurementUnitTypeEnum.Seconds:
                case null:
                    Source = "inline";
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(measurementUnitTypeEnum), measurementUnitTypeEnum, null);
            }
        }
    }
}