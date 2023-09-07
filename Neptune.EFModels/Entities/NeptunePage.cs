using Neptune.Common.DesignByContract;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Html;

namespace Neptune.EFModels.Entities;

public partial class NeptunePage
{
    [NotMapped]
    public HtmlString? NeptunePageContentHtmlString
    {
        get => NeptunePageContent == null ? null : new HtmlString(NeptunePageContent);
        set => NeptunePageContent = value?.ToString();
    }

    public bool HasNeptunePageContent()
    {
        return !string.IsNullOrWhiteSpace(NeptunePageContent);
    }
}