/*-----------------------------------------------------------------------
<copyright file="TextAreaForExtensions.cs" company="Sitka Technology Group">
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

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Neptune.Common.DesignByContract;

namespace Neptune.Web.Common.HtmlHelperExtensions
{
    public static class TextAreaForExtensions
    {
        public const string DisabledBackgroundColor = "#DDDDDD";

        public enum TextAreaEnableType
        {
            Enabled,
            Disabled,
        }

        public struct TextAreaDimensions
        {
            public int Rows { get; }
            public int? ColumnWidthInPixels { get; }

            public TextAreaDimensions(int? columnWidthInPixels, int rows) : this()
            {
                // Pixel width is preferred, column width calculated from it
                ColumnWidthInPixels = columnWidthInPixels;
                Check.RequireGreaterThanZero(rows, "Size must be > 0");
                Rows = rows;
            }

            /// <summary>
            /// Build a proportional text area, suitable for variable-width fonts
            /// </summary>
            public static TextAreaDimensions BuildForProportional(int columnWidthInPixels, int rows)
            {
                return new TextAreaDimensions(columnWidthInPixels, rows);
            }
        }

        /// <summary>
        /// Custom TextArea control that has the max chars left in another div
        /// Only public for unit testing
        /// </summary>
        public static IHtmlContent TextAreaWithMaxLengthFor<TViewModel, TValue>(this IHtmlHelper<TViewModel> html,
            Expression<Func<TViewModel, TValue>> expression,
            TextAreaDimensions textAreaDimensions, string optionalPlaceholderText)
        {
            return html.TextAreaWithMaxLengthFor(expression, textAreaDimensions, optionalPlaceholderText, null);
        }

        /// <summary>
        /// Custom TextArea control that has the max chars left in another div
        /// Only public for unit testing
        /// </summary>
        public static IHtmlContent TextAreaWithMaxLengthFor<TViewModel, TValue>(this IHtmlHelper<TViewModel> htmlHelper,
            Expression<Func<TViewModel, TValue>> expression,
            TextAreaDimensions textAreaDimensions, string optionalPlaceholderText, IEnumerable<string> cssClasses)
        {
            int? maxLength = null;
            if (expression.Body is MemberExpression memberExpression)
            {
                var stringLengthAttribute =
                    memberExpression.Member.GetCustomAttributes(typeof (StringLengthAttribute), true)
                        .Cast<StringLengthAttribute>()
                        .SingleOrDefault();
                if (stringLengthAttribute != null)
                {
                    maxLength = stringLengthAttribute.MaximumLength;
                }
            }

            var textAreaEnableType = TextAreaEnableType.Enabled;
            var expressionProvider = new ModelExpressionProvider(htmlHelper.MetadataProvider);
            var metadata = expressionProvider.CreateModelExpression(htmlHelper.ViewData, expression);
            var value = (string) metadata.Model;
            var fieldName = expressionProvider.GetExpressionText(expression);
            var fullBindingName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName, "_");
            var htmlAttributes = new Dictionary<string, object> { { "id", fieldId } };
            if (textAreaEnableType == TextAreaEnableType.Disabled)
            {
                htmlAttributes.Add("readonly", "readonly");
            }
            if (!string.IsNullOrWhiteSpace(optionalPlaceholderText))
            {
                htmlAttributes.Add("placeholder", optionalPlaceholderText);
            }
            if (cssClasses != null)
            {
                htmlAttributes.Add("class", string.Join(" ", cssClasses));
            }
            htmlAttributes.Add("style", BuildStyleString(textAreaDimensions, textAreaEnableType));
            htmlAttributes.Add("rows", textAreaDimensions.Rows.ToString(CultureInfo.InvariantCulture));

            // For varchar(max) consider -1 is the same as *unlimited*
            if (maxLength.HasValue && maxLength > 0)
            {
                const int lowCharacterCountWarning = 20;
                const string charactersRemainingString = "Characters Remaining: ";

                // If null, just set it to string.Empty
                //string? value1 = value;
                //value1 ??= string.Empty;

                //// If input string is already over max length, truncate it
                //var valueLength = value1.Length;
                //if (valueLength > maxLength.Value)
                //{
                //    value1 = value1.Substring(0, maxLength.Value);
                //    ((TagBuilder)textAreaHtmlString).InnerHtml.Append(value1);
                //}
                var charactersRemainingElementName = $"CharactersRemaining_{fieldId}";
                var keyUpKeyDownMaxLengthJavascript =
                    $"Sitka.Methods.keepTextAreaWithinMaxLength(this, {maxLength.Value}, {lowCharacterCountWarning}, '{charactersRemainingElementName}', '{charactersRemainingString}');";
                htmlAttributes.Add("onkeydown", keyUpKeyDownMaxLengthJavascript);
                htmlAttributes.Add("onkeyup", keyUpKeyDownMaxLengthJavascript);
                var textAreaHtmlString = htmlHelper.TextAreaFor(expression, htmlAttributes);

                var textAreaDivTag = new TagBuilder("div");

                if (!textAreaDimensions.ColumnWidthInPixels.HasValue)
                {
                    textAreaDivTag.Attributes.Add("style", "width:100%");
                }

                var maxCharsDivTag = new TagBuilder("div");
                maxCharsDivTag.Attributes.Add("id", charactersRemainingElementName);
                var charactersRemaining = maxLength.Value - (value?? "").Length;
                var charLimitStyle = (charactersRemaining <= lowCharacterCountWarning) ? "color:red;" : "color:#666666;";
                maxCharsDivTag.Attributes.Add("style", $"text-align:right;{charLimitStyle}");
                maxCharsDivTag.Attributes.Add("class", "charactersRemainingText");
                maxCharsDivTag.InnerHtml.Append($"{charactersRemainingString}{charactersRemaining}");
                textAreaDivTag.InnerHtml.AppendHtml(textAreaHtmlString);
                textAreaDivTag.InnerHtml.AppendHtml(maxCharsDivTag);

                var clearBothDiv = new TagBuilder("div");
                clearBothDiv.Attributes.Add("style", "clear:both");

                var writer = new StringWriter();
                var builder = new HtmlContentBuilder();
                builder.AppendFormat("{0}{1}", textAreaDivTag, clearBothDiv);
                builder.WriteTo(writer, HtmlEncoder.Default);

                var htmlString = writer.ToString();
                return new HtmlString(htmlString);
            }
            return htmlHelper.TextAreaFor(expression, htmlAttributes);
        }

        /// <summary>
        /// Build style string for the TextArea.
        /// </summary>
        public static string BuildStyleString(TextAreaDimensions textAreaDimensions, TextAreaEnableType textAreaEnableType)
        {
            const string disabledBackgroundColor = "#DDDDDD";
            var backgroundColorString = textAreaEnableType == TextAreaEnableType.Enabled ? string.Empty : " background-color: " + disabledBackgroundColor;
            // We put in a "width" field (and not the "cols" attribute) when we are *NOT* in an IE browser AND we aren't using a monospaced font
            var pixelWidthString = textAreaDimensions.ColumnWidthInPixels.HasValue ? $"width:{textAreaDimensions.ColumnWidthInPixels}px"
                : "width:100%";
            const string disableResizeString = "resize: none";
            var styleString = $"{backgroundColorString};{pixelWidthString};{disableResizeString};";
            return styleString;
        }
    }
}
