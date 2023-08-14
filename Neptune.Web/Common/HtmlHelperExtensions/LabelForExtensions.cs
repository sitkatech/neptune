/*-----------------------------------------------------------------------
<copyright file="LabelForExtensions.cs" company="Sitka Technology Group">
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
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.BootstrapWrappers;
using Neptune.Web.Models;

namespace Neptune.Web.Common.HtmlHelperExtensions
{
    public static class LabelWithSugarForExtensions
    {
        public enum DisplayStyle
        {
            AsGridHeader,
            HelpIconOnly,
            HelpIconWithLabel,
            SitsOnDarkBackground
        }

        public const int DefaultPopupWidth = 300;

        /// <summary>
        /// Does what LabelWithHelpFor does and adds a help icon
        /// </summary>
        public static IHtmlContent LabelWithSugarFor<TViewModel, TValue>(this IHtmlHelper<TViewModel> htmlHelper, Expression<Func<TViewModel, TValue>> expression)
        {
            return LabelWithSugarFor(htmlHelper, expression, DefaultPopupWidth);
        }

        /// <summary>
        /// Does what LabelWithHelpFor does and adds a help icon
        /// </summary>
        public static IHtmlContent LabelWithSugarFor<TViewModel, TValue>(this IHtmlHelper<TViewModel> htmlHelper, Expression<Func<TViewModel, TValue>> expression, string labelText)
        {
            return LabelWithSugarFor(htmlHelper, expression, DefaultPopupWidth, labelText);
        }

        /// <summary>
        /// Does what LabelWithHelpFor does and adds a help icon
        /// </summary>
        public static IHtmlContent LabelWithSugarFor(this IHtmlHelper htmlHelper, FieldDefinitionType fieldDefinition)
        {
            return LabelWithSugarFor(htmlHelper, fieldDefinition, DefaultPopupWidth);
        }

        public static IHtmlContent LabelWithSugarFor(this IHtmlHelper htmlHelper, FieldDefinitionType fieldDefinition, bool hasRequiredAttribute)
        {
            var labelText = fieldDefinition.GetFieldDefinitionLabel();
            var fullHtmlFieldID = labelText.Replace(" ", "");
            // in this case, we are not trying to tie it to an actual viewmodel; we only want it to be safe as an id to find by jquery
            return LabelWithSugarFor(fullHtmlFieldID, DefaultPopupWidth, DisplayStyle.HelpIconWithLabel, hasRequiredAttribute, labelText, fieldDefinition.GetContentUrl());
        }

        private static IHtmlContent LabelWithSugarFor(string fullHtmlFieldID, int popupWidth, DisplayStyle displayStyle, bool hasRequiredAttribute, string labelText, string urlToContent)
        {
            return LabelWithFieldDefinitionForImpl(labelText, fullHtmlFieldID, urlToContent, popupWidth, displayStyle, hasRequiredAttribute);
        }

        /// <summary>
        /// Does what LabelWithHelpFor does and adds a help icon and with custom label text
        /// </summary>
        public static IHtmlContent LabelWithSugarFor(this IHtmlHelper htmlHelper, FieldDefinitionType fieldDefinition, string labelText)
        {
            return LabelWithSugarFor(DefaultPopupWidth, DisplayStyle.HelpIconWithLabel, labelText, fieldDefinition.GetContentUrl());
        }

        /// <summary>
        /// Does what LabelWithHelpFor does and adds a help icon
        /// </summary>
        public static IHtmlContent LabelWithSugarFor(this IHtmlHelper htmlHelper, FieldDefinitionType fieldDefinition, int popupWidth)
        {
            return LabelWithSugarFor(htmlHelper, fieldDefinition, popupWidth, DisplayStyle.HelpIconWithLabel);
        }

        /// <summary>
        /// Does what LabelWithHelpFor does and adds a help icon
        /// </summary>
        public static IHtmlContent LabelWithSugarFor(this IHtmlHelper htmlHelper, FieldDefinitionType fieldDefinition, DisplayStyle displayStyle)
        {
            return LabelWithSugarFor(htmlHelper, fieldDefinition, DefaultPopupWidth, displayStyle);
        }

        /// <summary>
        /// Does what LabelWithHelpFor does and adds a help icon
        /// </summary>
        public static IHtmlContent LabelWithSugarFor(this IHtmlHelper htmlHelper, FieldDefinitionType fieldDefinition, DisplayStyle displayStyle, string labelText)
        {
            return LabelWithSugarFor(DefaultPopupWidth, displayStyle, labelText, fieldDefinition.GetContentUrl());
        }

        /// <summary>
        /// Does what LabelWithHelpFor does and adds a help icon
        /// </summary>
        public static IHtmlContent LabelWithSugarFor(this IHtmlHelper htmlHelper, FieldDefinitionType fieldDefinition, int popupWidth, DisplayStyle displayStyle)
        {
            return LabelWithSugarFor(popupWidth, displayStyle, fieldDefinition.GetFieldDefinitionLabel(), fieldDefinition.GetContentUrl());
        }

        public static IHtmlContent LinkWithFieldDefinitionFor(this IHtmlHelper htmlHelper, FieldDefinitionType fieldDefinition, string linkText, List<string> cssClasses)
        {
            return LinkWithFieldDefinitionFor(htmlHelper, fieldDefinition, linkText, DefaultPopupWidth, cssClasses);
        }

        public static IHtmlContent LinkWithFieldDefinitionFor(this IHtmlHelper htmlHelper, FieldDefinitionType fieldDefinition, string linkText, int popupWidth, List<string> cssClasses)
        {
            var fieldDefinitionLinkTag = new TagBuilder("a");
            fieldDefinitionLinkTag.Attributes.Add("href", "javascript:void(0)");
            fieldDefinitionLinkTag.Attributes.Add("class", string.Join(" ", cssClasses));
            var labelText = fieldDefinition.GetFieldDefinitionLabel();
            fieldDefinitionLinkTag.Attributes.Add("title", $"Click to get help on {labelText}");
            fieldDefinitionLinkTag.InnerHtml.Append(linkText);
            var urlToContent = fieldDefinition.GetContentUrl();
            AddHelpToolTipPopupToHtmlTag(fieldDefinitionLinkTag, labelText, urlToContent, popupWidth);
            return fieldDefinitionLinkTag;
        }

        /// <summary>
        /// Does what LabelWithHelpFor does and adds a help icon
        /// </summary>
        public static IHtmlContent LabelWithSugarFor<TViewModel, TValue>(this IHtmlHelper<TViewModel> htmlHelper, Expression<Func<TViewModel, TValue>> expression, int popupWidth)
        {
            return LabelWithSugarFor(htmlHelper, expression, popupWidth, null);
        }

        public static IHtmlContent LabelWithSugarFor<TViewModel, TValue>(this IHtmlHelper<TViewModel> htmlHelper, Expression<Func<TViewModel, TValue>> expression, int popupWidth, string labelText)
        {
            var expressionProvider = new ModelExpressionProvider(htmlHelper.MetadataProvider);
            var metadata = expressionProvider.CreateModelExpression(htmlHelper.ViewData, expression);
            if (expression.Body is not MemberExpression memberExpression)
            {
                return HtmlString.Empty;
            }
            var fieldDefinitionDisplayAttributeType = typeof(IFieldDefinitionDisplayAttribute);
            var fieldDefinitionDisplayAttribute = memberExpression.Member.GetCustomAttributes(fieldDefinitionDisplayAttributeType, true).Cast<IFieldDefinitionDisplayAttribute>().SingleOrDefault();

            var requiredAttributeType = typeof(RequiredAttribute);
            var hasRequiredAttribute = memberExpression.Member.GetCustomAttributes(requiredAttributeType, true).Cast<RequiredAttribute>().Any();
            var htmlFieldName = expressionProvider.GetExpressionText(expression);

            if (fieldDefinitionDisplayAttribute == null)
            {
                return LabelWithRequiredTagForImpl(htmlHelper, metadata, htmlFieldName, hasRequiredAttribute, labelText, null);
            }
            else
            {
                var fieldDefinition = fieldDefinitionDisplayAttribute.FieldDefinition;
                var fullHtmlFieldID = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName);
                var fieldDefinitionDisplayName = string.IsNullOrWhiteSpace(labelText) ? fieldDefinition.GetFieldDefinitionLabel() : labelText;
                return LabelWithSugarFor(fullHtmlFieldID, popupWidth, DisplayStyle.HelpIconWithLabel, hasRequiredAttribute, fieldDefinitionDisplayName, fieldDefinition.GetContentUrl());
            }
        }

        public static IHtmlContent LabelWithSugarFor<TViewModel, TValue>(this IHtmlHelper<TViewModel> htmlHelper, Expression<Func<TViewModel, TValue>> expression, bool hasRequiredAttribute)
        {
            var expressionProvider = new ModelExpressionProvider(htmlHelper.MetadataProvider);
            var metadata = expressionProvider.CreateModelExpression(htmlHelper.ViewData, expression);
            var htmlFieldName = expressionProvider.GetExpressionText(expression);
            return LabelWithRequiredTagForImpl(htmlHelper, metadata, htmlFieldName, hasRequiredAttribute, null, null);
        }

        public static IHtmlContent LabelWithSugarFor(int popupWidth, DisplayStyle displayStyle, string labelText, string getContentUrl)
        {
            var fullHtmlFieldID = labelText.Replace(" ", "");
            // in this case, we are not trying to tie it to an actual viewmodel; we only want it to be safe as an id to find by jquery
            return LabelWithSugarFor(fullHtmlFieldID, popupWidth, displayStyle, false, labelText, getContentUrl);
        }

        public static IHtmlContent LabelWithRequiredTagFor(this IHtmlHelper htmlHelper, string labelText)
        {
            return LabelWithRequiredTagForImpl(htmlHelper, null, null, true, labelText, null);
        }

        private static IHtmlContent LabelWithRequiredTagForImpl(IHtmlHelper htmlHelper, ModelExpression modelExpression, string htmlFieldName, bool hasRequiredAttribute, string? labelText = null, IDictionary<string, object> htmlAttributes = null)
        {
            var resolvedLabelText = labelText ?? modelExpression.Metadata.DisplayName ?? modelExpression.Metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (string.IsNullOrEmpty(resolvedLabelText))
            {
                return HtmlString.Empty;
            }

            var tag = new TagBuilder("label");
            tag.Attributes.Add("for", TagBuilder.CreateSanitizedId(htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(htmlFieldName), "_"));

            var requiredAsterisk = hasRequiredAttribute ? $"<sup>{BootstrapHtmlHelpers.RequiredIcon}</sup>" : string.Empty;


            tag.InnerHtml.AppendHtml($"{resolvedLabelText} {requiredAsterisk}");
            tag.MergeAttributes(htmlAttributes, true);

            return tag;
        }

        /// <summary>
        /// only public for unit testing
        /// </summary>
        public static IHtmlContent LabelWithFieldDefinitionForImpl(string labelText,
            string fullHtmlFieldID,
            string urlToContent,
            int popupWidth,
            DisplayStyle displayStyle, bool hasRequiredAttribute)
        {
            if (string.IsNullOrEmpty(labelText))
            {
                return HtmlString.Empty;
            }

            var helpIconImgTag = GenerateHelpIconImgTag(labelText, urlToContent, popupWidth, displayStyle);
            var labelTag = new TagBuilder("label");
            labelTag.Attributes.Add("for", fullHtmlFieldID);
            labelTag.InnerHtml.Append(labelText);

            var writer = new StringWriter();
            var builder = new HtmlContentBuilder();
            switch (displayStyle)
            {
                case DisplayStyle.AsGridHeader:
                    var divTag = new TagBuilder("div");
                    divTag.Attributes.Add("style", "display:table; vertical-align: top");
                    labelTag.Attributes.Add("style", "display:table-cell");
                    builder.AppendFormat("{0}{1}", helpIconImgTag, labelTag);
                    builder.WriteTo(writer, HtmlEncoder.Default);
                    return new HtmlString(writer.ToString());
                case DisplayStyle.HelpIconOnly:
                    return helpIconImgTag;
                case DisplayStyle.HelpIconWithLabel:
                case DisplayStyle.SitsOnDarkBackground:
                    var requiredAsterisk = hasRequiredAttribute ? " <sup>" + BootstrapHtmlHelpers.RequiredIcon + "</sup>" : string.Empty;
                    builder.AppendFormat("{0}{1}{2}", helpIconImgTag, labelText, requiredAsterisk);
                    builder.WriteTo(writer, HtmlEncoder.Default);
                    return new HtmlString(writer.ToString());
                default:
                    throw new ArgumentOutOfRangeException("displayStyle");
            }
        }

        public static IHtmlContent GenerateHelpIconImgTag(string labelText, string urlToContent, int popupWidth, DisplayStyle displayStyle)
        {
            var helpIconImgTag = new TagBuilder("span");
            var backgroundClass = displayStyle == DisplayStyle.SitsOnDarkBackground ? "helpicon-white-background" : "";
            helpIconImgTag.Attributes.Add("class", $"helpicon glyphicon glyphicon-question-sign {backgroundClass}".Trim());
            helpIconImgTag.Attributes.Add("title", string.Format("Click to get help on {0}", labelText));
            AddHelpToolTipPopupToHtmlTag(helpIconImgTag, labelText, urlToContent, popupWidth);
            if (displayStyle == DisplayStyle.AsGridHeader)
            {
                // this cancels the sort even on the dhtmlxgrid
                helpIconImgTag.Attributes.Add("onclick", "(arguments[0]||window.event).cancelBubble=true;");
                helpIconImgTag.Attributes.Add("style", "display:table-cell; padding-right:2px");
            }
            return helpIconImgTag;
        }

        public static void AddHelpToolTipPopupToHtmlTag(TagBuilder tagBuilder, string labelText, string urlToContent, int popupWidth)
        {
            tagBuilder.Attributes.Add("onmouseover", string.Format("Neptune.Views.Methods.addHelpTooltipPopup(this, {0}, {1}, {2})", labelText.ToJS(), urlToContent.ToJS(), popupWidth));
        }

        public static IHtmlContent GenerateHelpLink(string linkText, string popupTitleText, string urlToContent, int popupWidth)
        {
            var helpIconImgTag = new TagBuilder("span");
            helpIconImgTag.Attributes.Add("class", "helpicon glyphicon glyphicon-question-sign helpiconGridBlue");
            helpIconImgTag.Attributes.Add("title", string.Format("Click to get help on {0}", linkText));
            AddHelpToolTipPopupToHtmlTag(helpIconImgTag, popupTitleText, urlToContent, popupWidth);
            var labelTag = new TagBuilder("a");
            AddHelpToolTipPopupToHtmlTag(labelTag, popupTitleText, urlToContent, popupWidth);
            labelTag.InnerHtml.Append(linkText);
            var writer = new StringWriter();
            var builder = new HtmlContentBuilder();
            builder.AppendFormat("{0} {1}", helpIconImgTag, labelTag);
            builder.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());

        }
    }
}
