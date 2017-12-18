using System;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.ExcelWorkbookUtilities;
using LtInfo.Common.MvcResults;
using Neptune.Web.Models;
using Neptune.Web.Security.Shared;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.GoogleChart;

namespace Neptune.Web.Controllers
{
    public class GoogleChartController : NeptuneBaseController
    {
        [HttpGet]
        [AnonymousUnclassifiedFeature]
        [CrossAreaRoute]
        public ContentResult DownloadChartData()
        {
            return new ContentResult();
        }

        [HttpPost]
        [AnonymousUnclassifiedFeature]
        [CrossAreaRoute]
        public ExcelResult DownloadChartData(DownloadChartDataViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid POST data.");
            }

            var excelWorkbook = viewModel.GetExcelWorkbook(x => ExcelWorkbookSheetDescriptorFactory.MakeWorksheet(x.GoogleChartConfiguration.Title,
                new GoogleChartExcelSpec(x.GoogleChartDataTable.GoogleChartColumns),
                GoogleChartDataSimple.DeriveSimplesFromGoogleChartJson(x)) as IExcelWorkbookSheetDescriptor);
            return new ExcelResult(excelWorkbook, viewModel.ExcelFilename);
        }

        [HttpGet]
        [AnonymousUnclassifiedFeature]
        [CrossAreaRoute]
        public ContentResult GoogleChartPopup()
        {
            return new ContentResult();
        }

        [HttpPost]
        [AnonymousUnclassifiedFeature]
        [CrossAreaRoute]
        public ActionResult GoogleChartPopup(GoogleChartPopupViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid POST data.");
            }

            var googleChartJson = viewModel.DeserializeGoogleChartJson();
            EnlargeGoogleChart(googleChartJson);
            var viewData = new GoogleChartPopupViewData(googleChartJson);
            return RazorPartialView<GoogleChartPopup, GoogleChartPopupViewData, GoogleChartPopupViewModel>(viewData, viewModel);
        }

        private static void EnlargeGoogleChart(GoogleChartJson googleChartJson)
        {
            var googleChartConfiguration = googleChartJson.GoogleChartConfiguration;
            const int fontSizeIncrease = 8;

            googleChartConfiguration.TitlePosition = "top";
            
            // Use default TitleTextStyle for consistency across enlarged charts.
            googleChartConfiguration.TitleTextStyle = null;

            IncreaseGoogleChartTextStyleFontSizeIfPresent(googleChartConfiguration.LegendTextStyle, fontSizeIncrease / 2, false);
            IncreaseGoogleChartTextStyleFontSizeIfPresent(googleChartConfiguration.HorizontalAxis.TextStyle, fontSizeIncrease, true);

            if (googleChartConfiguration.VerticalAxes != null)
            {
                foreach (var vaxis in googleChartConfiguration.VerticalAxes)
                {
                    IncreaseGoogleChartTextStyleFontSizeIfPresent(vaxis.TextStyle, fontSizeIncrease, true);
                }
            }

            //Make lines thicker on Line and Combo charts (but NOT on area charts)
            if (!googleChartJson.ChartType.Equals(GoogleChartType.AreaChart.GoogleChartTypeDisplayName, StringComparison.InvariantCultureIgnoreCase))
            {
                googleChartConfiguration.LineWidth = 6;
            }

            //Since PieCharts are square, we have plenty of side area and this will always be the ideal legend placement
            if (googleChartJson.ChartType.Equals(GoogleChartType.PieChart.GoogleChartTypeDisplayName, StringComparison.InvariantCultureIgnoreCase))
            {
                googleChartConfiguration.Legend.Position = "labeled";
            }
        }

        private static void IncreaseGoogleChartTextStyleFontSizeIfPresent(GoogleChartTextStyle googleChartTextStyle, int fontSize, bool makeBold)
        {
            if (googleChartTextStyle != null)
            {
                googleChartTextStyle.FontSize += fontSize;
                if (makeBold)
                {
                    googleChartTextStyle.MakeBold();
                }
            }
        }
    }
}
