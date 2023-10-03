using System.Xml;

namespace Neptune.WebMvc.Common.DhtmlWrappers.DhtmlxExport
{
    public class ExcelCell
    {
        private string value = "";
        private string bgColor = "";
        private string textColor = "";
        private bool bold = false;
        private bool italic = false;
        private string align = "";

        public void Parse(XmlNode parent)
        {
            if (parent.HasChildNodes)
                value = parent.FirstChild.Value;
            XmlElement el = (XmlElement)parent;
            bgColor = el.HasAttribute("bgColor") ? RGBColor.ProcessColorForm(el.Attributes["bgColor"].Value) : "";
            textColor = el.HasAttribute("textColor") ? RGBColor.ProcessColorForm(el.Attributes["textColor"].Value) : "";
            bold = el.HasAttribute("bold") ? el.Attributes["bold"].Value.Equals("bold") : false;
            italic = el.HasAttribute("italic") ? el.Attributes["italic"].Equals("italic") : false;
            align = el.HasAttribute("align") ? el.Attributes["align"].Value : "";
        }

        public string GetValue()
        {
            return value;
        }

        public string GetBgColor()
        {
            return bgColor;
        }

        public string GetTextColor()
        {
            return textColor;
        }

        public bool GetBold()
        {
            return bold;
        }

        public bool GetItalic()
        {
            return italic;
        }

        public string GetAlign()
        {
            return align;
        }

    }
}
