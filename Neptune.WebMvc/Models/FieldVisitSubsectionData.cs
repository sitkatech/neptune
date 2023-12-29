using Microsoft.AspNetCore.Html;

namespace Neptune.WebMvc.Models;

public class FieldVisitSubsectionData
{
    public string SubsectionName { get; set; }
    public string SubsectionUrl { get; set; }
    public HtmlString SectionCompletionStatusIndicator { get; set; }
}