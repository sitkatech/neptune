using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Neptune.Web.Common.HtmlHelperExtensions;

public static class IHtmlHelperExtensions
{
    public static void GetUnobtrusiveValidationAttributes<TViewModel>(this IHtmlHelper<TViewModel> htmlHelper, TagBuilder tagBuilder, ModelExplorer modelExplorer, string expressionText)
    {
        var validator = htmlHelper.ViewContext.HttpContext.RequestServices.GetService<ValidationHtmlAttributeProvider>();
        validator?.AddAndTrackValidationAttributes(htmlHelper.ViewContext, modelExplorer, expressionText, tagBuilder.Attributes);
    }
}