/*-----------------------------------------------------------------------
<copyright file="BootstrapHtmlHelpers.cs" company="Sitka Technology Group">
Copyright (c) Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using Microsoft.AspNetCore.Html;

namespace Neptune.Web.Common.BootstrapWrappers
{
    public static class BootstrapHtmlHelpers
    {
        public static IHtmlContent MakeGlyphIcon(string glyphIconName)
        {
            return new HtmlString($"<span class=\"glyphicon {glyphIconName}\"></span>");
        }

        public static IHtmlContent MakeGlyphIconWithHiddenText(string glyphIconName, string text)
        {
            return new HtmlString($"<span class=\"glyphicon {glyphIconName}\"></span><span style='display:none'>{text}</span>");
        }

        public static IHtmlContent MakeGlyphIcon(string glyphIconName, string title)
        {
            return new HtmlString($"<span title=\"{title}\" class=\"glyphicon {glyphIconName}\"></span>");
        }

        public static IHtmlContent MakeModalDialogAlertLink(string alertText, string alertTitle, string closeButtonText, string linkText, List<string> cssClasses)
        {
            return
                new HtmlString(string.Format("<a href=\"javascript:void(0)\" class=\"{0}\" onclick=\"createBootstrapAlert({1}, {2}, {3})\">{4}</a>",
                    string.Join(" ", cssClasses),
                    alertText.ToHTMLFormattedString().ToString().ToJS(),
                    alertTitle.ToHTMLFormattedString().ToString().ToJS(),
                    closeButtonText.ToHTMLFormattedString().ToString().ToJS(),
                    linkText.ToHTMLFormattedString()));
        }

        public static IHtmlContent MakeModalDialogAlertLinkFromUrl(string url, string alertTitle, string closeButtonText, string linkText, List<string> cssClasses, string onJavascriptReadyFunction)
        {
            var javascripReadyFunctionAsParameter = !string.IsNullOrWhiteSpace(onJavascriptReadyFunction) ? $"function() {{{onJavascriptReadyFunction}();}}"
                : "null";
            return
                new HtmlString(
                    $"<a href=\"{url}\" class=\"{string.Join(" ", cssClasses)}\" onclick=\"return createBootstrapAlertFromUrl(this, {alertTitle.ToHTMLFormattedString().ToString().ToJS()}, {closeButtonText.ToHTMLFormattedString().ToString().ToJS()}, {javascripReadyFunctionAsParameter});\">{linkText.ToHTMLFormattedString()}</a>");
        }

        public static string RequiredIcon = "<span class=\"requiredFieldIcon glyphicon glyphicon-flash\" style=\"color: #800020; font-size: 8px; \"></span>";
    }
}
