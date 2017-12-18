using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClosedXML.Excel;
using LtInfo.Common.ExcelWorkbookUtilities;
using LtInfo.Common.Models;
using Newtonsoft.Json;
using Neptune.Web.Views.Shared;
using System;
using System.Globalization;


namespace Neptune.Web.Views.GoogleChart
{
    public class DownloadChartDataViewModel : FormViewModel
    {
        [DisplayName("Google Chart Jsons"), Required]
        public List<string> GoogleChartJsons { get; set; }

        [DisplayName("Chart Name"), Required]
        public string ExcelFilename { get; set; }

        [DisplayName("Main Column Label"), Required]
        public string MainColumnLabel { get; set; }

        public delegate IExcelWorkbookSheetDescriptor GoogleChartJsonToExcelWorkbookSheetDescriptor(GoogleChartJson googleChartJson);

        public XLWorkbook GetExcelWorkbook(GoogleChartJsonToExcelWorkbookSheetDescriptor googleChartJsonToExcelWorkbookSheetDescriptor)
        {
            return new ExcelWorkbookMaker(GoogleChartJsons.Select(JsonConvert.DeserializeObject<GoogleChartJson>)
                .ToList()
                .Select(x => googleChartJsonToExcelWorkbookSheetDescriptor(x))
                .OrderBy(x => x.WorksheetName)
                .ToList()).ToXLWorkbook();
        }
    }

    public class GoogleChartExcelSpec : ExcelWorksheetSpec<GoogleChartDataSimple>
    {
        public GoogleChartExcelSpec(List<GoogleChartColumn> googleChartColumns)
        {
            var validGoogleChartColumns = googleChartColumns.Where(x => x.ColumnLabel != null).ToList();
            foreach (var googleChartColumn in validGoogleChartColumns)
            {
                if (googleChartColumn.ColumnDataType == GoogleChartColumnDataType.String.ColumnDataType)
                {
                    AddColumn(googleChartColumn.ColumnLabel,
                        x =>
                        {
                            var googleChartRowV = x.ColumnValues[googleChartColumn.ColumnLabel];
                            return googleChartRowV?.Format ?? googleChartRowV?.Value?.ToString() ?? string.Empty;
                        });
                }
                else if (googleChartColumn.ColumnDataType == GoogleChartColumnDataType.Number.ColumnDataType)
                {
                    AddColumn(googleChartColumn.ColumnLabel,
                        x =>
                        {
                            var googleChartRowV = x.ColumnValues[googleChartColumn.ColumnLabel];
                            return googleChartRowV?.Value != null ? Double.Parse(googleChartRowV.Value.ToString(), CultureInfo.InvariantCulture) : (Double?)null;
                        });
                }
                else if (googleChartColumn.ColumnDataType == GoogleChartColumnDataType.Date.ColumnDataType || googleChartColumn.ColumnDataType == GoogleChartColumnDataType.DateTime.ColumnDataType ||
                         googleChartColumn.ColumnDataType == GoogleChartColumnDataType.TimeOfDay.ColumnDataType)
                {
                    AddColumn(googleChartColumn.ColumnLabel,
                        x =>
                        {
                            // First try to parse the raw date value. If that doesn't work, which it probably won't, default to trying to parse the formatted date string
                            var googleChartRowV = x.ColumnValues[googleChartColumn.ColumnLabel];
                            DateTime? dateTime = null;
                            try
                            {
                                dateTime = googleChartRowV?.Value != null ? DateTime.Parse(googleChartRowV.Value.ToString(), CultureInfo.InvariantCulture) : (DateTime?)null;
                            }
                            catch (FormatException)
                            {
                                try
                                {
                                    dateTime = googleChartRowV?.Format != null ? DateTime.Parse(googleChartRowV.Format.ToString(), CultureInfo.InvariantCulture) : (DateTime?)null;
                                }
                                catch (FormatException)
                                {
                                }
                            }

                            return dateTime;
                        });
                }
            }
        }
    }
}
