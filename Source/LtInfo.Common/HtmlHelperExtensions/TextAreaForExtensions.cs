﻿/*-----------------------------------------------------------------------
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using LtInfo.Common.DesignByContract;

namespace LtInfo.Common.HtmlHelperExtensions
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
            public int Rows { get; private set; }
            public int? ColumnWidthInPixels { get; private set; }

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
        public static MvcHtmlString TextAreaWithMaxLengthFor<TViewModel, TValue>(this HtmlHelper<TViewModel> html,
            Expression<Func<TViewModel, TValue>> expression,
            TextAreaDimensions textAreaDimensions, string optionalPlaceholderText)
        {
            return html.TextAreaWithMaxLengthFor(expression, textAreaDimensions, optionalPlaceholderText, null);
        }

        /// <summary>
        /// Custom TextArea control that has the max chars left in another div
        /// Only public for unit testing
        /// </summary>
        public static MvcHtmlString TextAreaWithMaxLengthFor<TViewModel, TValue>(this HtmlHelper<TViewModel> html,
            Expression<Func<TViewModel, TValue>> expression,
            TextAreaDimensions textAreaDimensions, string optionalPlaceholderText, IEnumerable<string> cssClasses)
        {
            int? maxLength = null;
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
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
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = (string) metadata.Model;
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var textAreaTag = new TagBuilder("textarea");
            if (cssClasses != null)
            {
                foreach (var cssClass in cssClasses)
                {
                    textAreaTag.AddCssClass(cssClass);
                }
            }

            textAreaTag.Attributes.Add("name", fullBindingName);
            textAreaTag.Attributes.Add("id", fieldId);
            if (textAreaEnableType == TextAreaEnableType.Disabled)
            {
                textAreaTag.Attributes.Add("readonly", "readonly");
            }
            textAreaTag.Attributes.Add("style", BuildStyleString(textAreaDimensions, textAreaEnableType));
            if (!string.IsNullOrWhiteSpace(optionalPlaceholderText))
            {
                textAreaTag.Attributes.Add("placeholder", optionalPlaceholderText);
            }
            textAreaTag.Attributes.Add("rows", textAreaDimensions.Rows.ToString(CultureInfo.InvariantCulture));
            textAreaTag.InnerHtml = value;
            var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);
            foreach (var key in validationAttributes.Keys)
            {
                textAreaTag.Attributes.Add(key, validationAttributes[key].ToString());
            }

            // For varchar(max) consider -1 is the same as *unlimited*
            if (maxLength.HasValue && maxLength > 0)
            {
                return AddCharactersRemainingToTextArea(textAreaTag, fieldId, value, maxLength.Value,
                    textAreaDimensions.ColumnWidthInPixels.HasValue);
            }

            return new MvcHtmlString(textAreaTag.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// This gets called when there is a maxlength set for this text area
        /// it will wrap it inside a div tag with float:left and append another div tag
        /// that contains the characters remaining info
        /// </summary>
        private static MvcHtmlString AddCharactersRemainingToTextArea(TagBuilder textAreaTag, string fieldId, string value, int maxLength, bool fixedWidth)
        {
            const int lowCharacterCountWarning = 20;
            const string charactersRemainingString = "Characters Remaining: ";

            // If null, just set it to string.Empty
            if (value == null)
            {
                value = string.Empty;
            }

            // If input string is already over max length, truncate it
            var valueLength = value.Length;
            if (valueLength > maxLength)
            {
                value = value.Substring(0, maxLength);
                textAreaTag.InnerHtml = value;
            }
            var charactersRemainingElementName = $"CharactersRemaining_{fieldId}";
            var keyUpKeyDownMaxLengthJavascript =
                $"Sitka.Methods.keepTextAreaWithinMaxLength(this, {maxLength}, {lowCharacterCountWarning}, '{charactersRemainingElementName}', '{charactersRemainingString}');";
            textAreaTag.Attributes.Add("onkeydown", keyUpKeyDownMaxLengthJavascript);
            textAreaTag.Attributes.Add("onkeyup", keyUpKeyDownMaxLengthJavascript);

            var textAreaDivTag = new TagBuilder("div");

            if (!fixedWidth)
            {
                textAreaDivTag.Attributes.Add("style", "width:100%");
            }

            var maxCharsDivTag = new TagBuilder("div");
            maxCharsDivTag.Attributes.Add("id", charactersRemainingElementName);
            var charactersRemaining = maxLength - valueLength;
            var charLimitStyle = (charactersRemaining <= lowCharacterCountWarning) ? "color:red;" : "color:#666666;";
            maxCharsDivTag.Attributes.Add("style", $"text-align:right;{charLimitStyle}");
            maxCharsDivTag.Attributes.Add("class", "charactersRemainingText");
            maxCharsDivTag.InnerHtml = $"{charactersRemainingString}{charactersRemaining}";

            textAreaDivTag.InnerHtml = textAreaTag.ToString(TagRenderMode.Normal) + maxCharsDivTag.ToString(TagRenderMode.Normal);

            var clearBothDiv = new TagBuilder("div");
            clearBothDiv.Attributes.Add("style", "clear:both");
            return MvcHtmlString.Create(textAreaDivTag.ToString(TagRenderMode.Normal) + clearBothDiv.ToString(TagRenderMode.Normal));
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
