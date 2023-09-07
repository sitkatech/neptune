using Microsoft.AspNetCore.Html;

namespace Neptune.Web.Models;

public class FieldVisitSubsectionData
{
    public string SubsectionName { get; set; }
    public string SubsectionUrl { get; set; }
    public HtmlString SectionCompletionStatusIndicator { get; set; }
}