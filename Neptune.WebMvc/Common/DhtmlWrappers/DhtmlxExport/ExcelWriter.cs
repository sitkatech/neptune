using System.Collections.Specialized;
using System.Web;
using ClosedXML.Excel;

namespace Neptune.WebMvc.Common.DhtmlWrappers.DhtmlxExport
{
    public class ExcelWriter
    {
        private XLWorkbook wb;
        private IXLWorksheet sheet;
        private ExcelColumn[][] cols;
        private int colsNumber = 0;
        private ExcelXmlParser parser;

        public int headerOffset = 0;
        public int scale = 6;
        public string pathToImgs = "";//optional, physical path

        public double RowHeight { get; set; }
        public double HeaderFontSize { get; set; }
        public double FooterFontSize { get; set; }
        public string FontFamily { get; set; }
        public double GridFontSize { get; set; }
        public double WatermarkFontSize { get; set; }
        public string OutputFileName { get; set; }
        public Dictionary<int, int> Widths { get; set; }

        public XLColor? BGColor { get; set; }
        public XLColor? LineColor { get; set; }

        public XLColor? ScaleOneColor { get; set; }
        public XLColor? ScaleTwoColor { get; set; }

        protected XLColor? GridTextColor { get; set; }
        protected XLColor? WatermarkTextColor { get; set; }
        protected XLColor? HeaderTextColor { get; set; }

        private int cols_stat;
        private int rows_stat;
        public bool PrintFooter { get; set; }
        private string watermark = null;


        public ExcelWriter()
        {
            PrintFooter = false;
            RowHeight = 22.5;
            FontFamily = "Arial";
            HeaderFontSize = FooterFontSize = 9;
            GridFontSize = WatermarkFontSize = 10;
            OutputFileName = "grid.xlsx";
        }

        public void Generate(string xml, Stream output)
        {
            parser = new ExcelXmlParser();
            parser.setXML(xml);
            using (wb = new XLWorkbook())
            {
                sheet = wb.Worksheets.Add("First Sheet");
                setColorProfile();
                headerPrint(parser);
                rowsPrint(parser, output);
                if (PrintFooter)
                    footerPrint(parser);
                watermarkPrint(parser);
                wb.SaveAs(output);
            }
        }

        public void Generate(HttpContext context)
        {
            Generate(HttpUtility.UrlDecode(context.Request.Form["grid_xml"]), context.Response);
        }


        public MemoryStream Generate(string xml)
        {
            using var data = new MemoryStream();
            Generate(xml, data);
            return data;
        }
        public MemoryStream Generate(NameValueCollection form)
        {
            var xml = HttpUtility.UrlDecode(form["grid_xml"]);
            var data = new MemoryStream();
            Generate(xml, data);
            return data;
        }
        public string ContentType
        {
            get
            {
                return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            }
        }

        public void Generate(string xml, HttpResponse resp)
        {
            var data = new MemoryStream();

            resp.ContentType = ContentType;
            resp.Headers.Append("Content-Disposition", $"attachment;filename={OutputFileName}");
            resp.Headers.Append("Cache-Control", "max-age=0");
            Generate(xml, data);

            data.WriteTo(resp.BodyWriter.AsStream());


        }

        private void headerPrint(ExcelXmlParser parser)
        {
            cols = parser.getColumnsInfo("head");
            //Widths
            int[] widths = parser.getWidths();
            if (Widths != null)
            {
                foreach (var index in Widths.Keys)
                {
                    if (index >= 0 && index < widths.Length)
                    {
                        widths[index] = Widths[index];
                    }
                }
            }
            cols_stat = widths.Length;



            var sumWidth = widths.Sum();

            if (parser.getWithoutHeader() == false)
            {
                for (var row = 1; row <= cols.Length; row++)
                {
                    sheet.Row(row).Height = RowHeight;
                    for (var col = 1; col <= cols[row - 1].Length; col++)
                    {
                        sheet.Cell(row, col).Style
                            .Font.SetFontName(FontFamily)
                            .Font.SetFontSize(HeaderFontSize)
                            .Font.SetBold(true)
                            .Font.SetFontColor(HeaderTextColor);
                        sheet.Cell(row, col).Style
                            .Border.SetBottomBorder(XLBorderStyleValues.Thin)
                            .Border.SetBottomBorderColor(LineColor)
                            .Border.SetLeftBorder(XLBorderStyleValues.Thin)
                            .Border.SetLeftBorderColor(LineColor)
                            .Border.SetRightBorder(XLBorderStyleValues.Thin)
                            .Border.SetRightBorderColor(LineColor)
                            .Border.SetTopBorder(XLBorderStyleValues.Thin)
                            .Border.SetTopBorderColor(LineColor);

                        sheet.ColumnWidth = widths[col - 1] / scale;
                        var name = cols[row - 1][col - 1].GetName();

                        sheet.Cell(row, col).Style.Fill.SetBackgroundColor(BGColor);

                        sheet.Cell(row, col).Value = name;
                        colsNumber = col;
                    }
                }
                headerOffset = cols.Length;
            }
        }

        private void footerPrint(ExcelXmlParser parser)
        {
            cols = parser.getColumnsInfo("foot");
            if (parser.getWithoutHeader() == false)
            {
                for (var row = 1; row <= cols.Length; row++)
                {
                    var rowInd = row + headerOffset;
                    sheet.Row(rowInd).Height = RowHeight;

                    for (var col = 1; col <= cols[row - 1].Length; col++)
                    {
                        sheet.Cell(rowInd, col).Style.Fill.SetBackgroundColor(BGColor);
                        sheet.Cell(rowInd, col).Style
                            .Font.SetFontName(FontFamily)
                            .Font.SetFontSize(FooterFontSize)
                            .Font.SetFontColor(HeaderTextColor)
                            .Font.SetBold(true);

                        sheet.Cell(rowInd, col).Style
                            .Border.SetBottomBorder(XLBorderStyleValues.Thin)
                            .Border.SetBottomBorderColor(LineColor)
                            .Border.SetLeftBorder(XLBorderStyleValues.Thin)
                            .Border.SetLeftBorderColor(LineColor)
                            .Border.SetRightBorder(XLBorderStyleValues.Thin)
                            .Border.SetRightBorderColor(LineColor)
                            .Border.SetTopBorder(XLBorderStyleValues.Thin)
                            .Border.SetTopBorderColor(LineColor);

                        sheet.Cell(rowInd, col).Value = cols[row - 1][col - 1].GetName();
                    }
                }
            }
            headerOffset += cols.Length;
        }

        private void watermarkPrint(ExcelXmlParser parser)
        {
            if (watermark == null) return;
            // f.setAlignment(Alignment.CENTRE);
            sheet.Cell(headerOffset + 1, 0).Value = watermark;
        }

        private void rowsPrint(ExcelXmlParser parser, Stream resp)
        {
            var rows = parser.getGridContent();
            rows_stat = rows.Length;

            for (var row = 1; row <= rows.Length; row++)
            {
                var cells = rows[row - 1].getCells();
                var rowInd = row + headerOffset;
                sheet.Row(rowInd).Height = 20;

                for (var col = 1; col <= cells.Length; col++)
                {
                    sheet.Cell(rowInd, col).Style
                        .Font.SetFontName(FontFamily)
                        .Font.SetFontSize(GridFontSize);

                    if (cells[col - 1].GetBold())
                    {
                        sheet.Cell(rowInd, col).Style.Font.SetBold(true);
                    }

                    if (cells[col - 1].GetItalic())
                    {
                        sheet.Cell(rowInd, col).Style.Font.SetItalic(true);
                    }

                    sheet.Cell(rowInd, col).Style
                        .Border.SetBottomBorder(XLBorderStyleValues.Thin)
                        .Border.SetBottomBorderColor(LineColor)
                        .Border.SetLeftBorder(XLBorderStyleValues.Thin)
                        .Border.SetLeftBorderColor(LineColor)
                        .Border.SetRightBorder(XLBorderStyleValues.Thin)
                        .Border.SetRightBorderColor(LineColor)
                        .Border.SetTopBorder(XLBorderStyleValues.Thin)
                        .Border.SetTopBorderColor(LineColor);


                    if (!cells[col - 1].GetBgColor().Equals("") && parser.getProfile().Equals("full_color"))
                    {
                        sheet.Cell(rowInd, col).Style.Fill.SetBackgroundColor(XLColor.FromHtml("FF" + cells[col - 1].GetBgColor()));
                    }
                    else
                    {
                        //Color bg;
                        if (row % 2 == 0)
                        {
                            sheet.Cell(rowInd, col).Style.Fill.SetBackgroundColor(ScaleTwoColor);
                        }
                        else
                        {
                            sheet.Cell(rowInd, col).Style.Fill.SetBackgroundColor(ScaleOneColor);
                        }
                    }


                    if (int.TryParse(cells[col - 1].GetValue(), out var intVal))
                    {
                        sheet.Cell(rowInd, col).Value = intVal;
                    }
                    else
                    {
                        sheet.Cell(rowInd, col).Value = cells[col - 1].GetValue();
                    }
                }
            }
            headerOffset += rows.Length;
        }

        private void setColorProfile()
        {
            var alpha = "FF";
            string profile = parser.getProfile();
            if (profile.ToLower().Equals("color") || profile.ToLower().Equals("full_color"))
            {
                BGColor = BGColor != null ? BGColor : XLColor.FromHtml($"{alpha}D1E5FE");
                LineColor = LineColor != null ? LineColor : XLColor.FromHtml(alpha + "A4BED4");
                HeaderTextColor = HeaderTextColor != null ? HeaderTextColor : XLColor.FromHtml(alpha + "000000");
                ScaleOneColor = ScaleOneColor != null ? ScaleOneColor : XLColor.FromHtml(alpha + "FFFFFF");
                ScaleTwoColor = ScaleTwoColor != null ? ScaleTwoColor : XLColor.FromHtml(alpha + "E3EFFF");
                GridTextColor = GridTextColor != null ? GridTextColor : XLColor.FromHtml(alpha + "00FF00");
                WatermarkTextColor = WatermarkTextColor != null ? WatermarkTextColor : XLColor.FromHtml(alpha + "8b8b8b");
            }
            else
            {
                if (profile.ToLower().Equals("gray"))
                {
                    BGColor = BGColor != null ? BGColor : XLColor.FromHtml(alpha + "E3E3E3");
                    LineColor = LineColor != null ? LineColor : XLColor.FromHtml(alpha + "B8B8B8");
                    HeaderTextColor = HeaderTextColor != null ? HeaderTextColor : XLColor.FromHtml(alpha + "000000");
                    ScaleOneColor = ScaleOneColor != null ? ScaleOneColor : XLColor.FromHtml(alpha + "FFFFFF");
                    ScaleTwoColor = ScaleTwoColor != null ? ScaleTwoColor : XLColor.FromHtml(alpha + "EDEDED");
                    GridTextColor = GridTextColor != null ? GridTextColor : XLColor.FromHtml(alpha + "000000");
                    WatermarkTextColor = WatermarkTextColor != null ? WatermarkTextColor : XLColor.FromHtml(alpha + "8b8b8b");
                }
                else
                {
                    BGColor = BGColor != null ? BGColor : XLColor.FromHtml(alpha + "FFFFFF");
                    LineColor = LineColor != null ? LineColor : XLColor.FromHtml(alpha + "000000");
                    HeaderTextColor = HeaderTextColor != null ? HeaderTextColor : XLColor.FromHtml(alpha + "000000");
                    ScaleOneColor = ScaleOneColor != null ? ScaleOneColor : XLColor.FromHtml(alpha + "FFFFFF");
                    ScaleTwoColor = ScaleTwoColor != null ? ScaleTwoColor : XLColor.FromHtml(alpha + "FFFFFF");
                    GridTextColor = GridTextColor != null ? GridTextColor : XLColor.FromHtml(alpha + "000000");
                    WatermarkTextColor = WatermarkTextColor != null ? WatermarkTextColor : XLColor.FromHtml(alpha + "000000");
                }
            }
        }
    }
}
