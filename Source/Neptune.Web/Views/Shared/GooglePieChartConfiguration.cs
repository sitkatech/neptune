using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;
using LtInfo.Common;
using Newtonsoft.Json;

namespace Neptune.Web.Views.Shared
{
    public class GooglePieChartConfiguration : GoogleChartConfiguration
    {
        [JsonProperty(PropertyName = "slices")]
        public List<GooglePieChartSlice> Slices { get; set; }
        [JsonProperty(PropertyName = "pieSliceTextStyle")]
        public GoogleChartTextStyle PieSliceTextStyle { get; set; }
        [JsonProperty(PropertyName = "pieSliceText")]
        public string PieSliceText { get; set; }

        public GooglePieChartConfiguration(string chartTitle,
            MeasurementUnitTypeEnum measurementUnitTypeEnum,
            List<GooglePieChartSlice> googlePieChartSlices,
            GoogleChartType googleChartType,
            GoogleChartDataTable googleChartDataTable) : base(chartTitle,
            true,
            googleChartType,
            googleChartDataTable,
            new GoogleChartAxis("Year", null, null),
            new List<GoogleChartAxis> {new GoogleChartAxis(null, measurementUnitTypeEnum, null)})
        {
            PieSliceTextStyle = new GoogleChartTextStyle("black");
            ChartArea = new GoogleChartConfigurationArea(10, 10);
            Slices = googlePieChartSlices;
        }
    }
}