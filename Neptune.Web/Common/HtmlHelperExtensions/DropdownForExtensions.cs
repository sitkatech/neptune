using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Neptune.Web.Common.HtmlHelperExtensions
{
    public static class DropdownForExtensions
    {
        // SearchableDropDownList

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent SearchableDropDownListFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
        {
            return SearchableDropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent SearchableDropDownListFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return SearchableDropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent SearchableDropDownListFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return SearchableDropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent SearchableDropDownListFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel)
        {
            return SearchableDropDownListFor(htmlHelper, expression, selectList, optionLabel, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent SearchableDropDownListFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            return SearchableDropDownListFor(htmlHelper, expression, selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static IHtmlContent SearchableDropDownListFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var expressionProvider = new ModelExpressionProvider(htmlHelper.MetadataProvider);
            var metadata = expressionProvider.CreateModelExpression(htmlHelper.ViewData, expression);

            return SearchableDropDownListHelper(htmlHelper, metadata, expressionProvider.GetExpressionText(expression), selectList, optionLabel, htmlAttributes);
        }

        private static IHtmlContent SearchableDropDownListHelper(IHtmlHelper htmlHelper, ModelExpression metadata, string expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(htmlHelper, metadata, optionLabel, expression, selectList, allowMultiple: false, htmlAttributes: htmlAttributes);
        }


        // Helper methods

        private static IEnumerable<SelectListItem> GetSelectData(this HtmlHelper htmlHelper, string name)
        {
            object o = null;
            if (htmlHelper.ViewData != null)
            {
                o = htmlHelper.ViewData.Eval(name);
            }
            if (o == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        "There is no ViewData item of type '{1}' that has the key '{0}'.",
                        name,
                        "IEnumerable<SelectListItem>"));
            }
            var selectList = o as IEnumerable<SelectListItem>;
            if (selectList == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        "The ViewData item that has the key '{0}' is of type '{1}' but must be of type '{2}'.",
                        name,
                        o.GetType().FullName,
                        "IEnumerable<SelectListItem>"));
            }
            return selectList;
        }

        internal static IHtmlContent ListItemToOption(SelectListItem item)
        {
            var builder = new TagBuilder("option");
            if (item.Value != null)
            {
                builder.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                builder.Attributes["selected"] = "selected";
            }
            if (item.Disabled)
            {
                builder.Attributes["disabled"] = "disabled";
            }
            builder.Attributes["data-tokens"] = item.Text;
            builder.InnerHtml.AppendHtml(item.Text);
            return builder;
        }

        internal static IHtmlContent EmptyListItemToOption(SelectListItem item)
        {
            var builder = new TagBuilder("option");
            if (item.Value != null)
            {
                builder.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                builder.Attributes["selected"] = "selected";
            }
            if (item.Disabled)
            {
                builder.Attributes["disabled"] = "disabled";
            }
            builder.Attributes["class"] = "bs-title-option";
            builder.InnerHtml.AppendHtml(item.Text);
            return builder;
        }

        private static IEnumerable<SelectListItem> GetSelectListWithDefaultValue(IEnumerable<SelectListItem> selectList, object defaultValue, bool allowMultiple)
        {
            IEnumerable defaultValues;

            if (allowMultiple)
            {
                defaultValues = defaultValue as IEnumerable;
                if (defaultValues == null || defaultValues is string)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            CultureInfo.CurrentCulture,
                            "The parameter '{0}' must evaluate to an IEnumerable when multiple selection is allowed.",
                            "expression"));
                }
            }
            else
            {
                defaultValues = new[] { defaultValue };
            }

            IEnumerable<string> values = from object value in defaultValues
                select Convert.ToString(value, CultureInfo.CurrentCulture);

            // ToString() by default returns an enum value's name.  But selectList may use numeric values.
            IEnumerable<string> enumValues = from Enum value in defaultValues.OfType<Enum>()
                select value.ToString("d");
            values = values.Concat(enumValues);

            var test = values.ToList();

            var selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            var newSelectList = new List<SelectListItem>();

            foreach (var item in selectList)
            {
                item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                newSelectList.Add(item);
            }
            return newSelectList;
        }

        private static IHtmlContent SelectInternal(this IHtmlHelper htmlHelper, ModelExpression metadata,
            string optionLabel, string name, IEnumerable<SelectListItem> selectList, bool allowMultiple,
            IDictionary<string, object> htmlAttributes)
        {
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }

            var usedViewData = false;

            // If we got a null selectList, try to use ViewData to get the list of items.
            if (selectList == null)
            {
                selectList = htmlHelper.ViewData.Select(x => new SelectListItem(x.Key, x.Value.ToString()));
                usedViewData = true;
            }

            var defaultValue = (allowMultiple) ? htmlHelper.GetModelStateValue(fullName, typeof(string[])) : htmlHelper.GetModelStateValue(fullName, typeof(string));

            // If we haven't already used ViewData to get the entire list of items then we need to
            // use the ViewData-supplied value before using the parameter-supplied value.
            if (defaultValue == null && !String.IsNullOrEmpty(name))
            {
                if (!usedViewData)
                {
                    defaultValue = htmlHelper.ViewData.Eval(name).ToString();
                }
                else if (metadata != null)
                {
                    defaultValue = metadata.Model.ToString();
                }
            }

            if (defaultValue != null)
            {
                selectList = GetSelectListWithDefaultValue(selectList, defaultValue, allowMultiple);
            }

            // Convert each ListItem to an <option> tag and wrap them with <optgroup> if requested.
            var listItemBuilder = BuildItems(optionLabel, selectList);

            var tagBuilder = new TagBuilder("select");
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("name", fullName, true /* replaceExisting */);
            tagBuilder.MergeAttribute("data-width", "100%");
            tagBuilder.MergeAttribute("data-live-search", "true");
            tagBuilder.MergeAttribute("data-live-search-placeholder", "Search");
            tagBuilder.MergeAttribute("data-container", "body");
            tagBuilder.MergeAttribute("class", "selectpicker");
            tagBuilder.GenerateId(fullName, "_");
            if (allowMultiple)
            {
                tagBuilder.MergeAttribute("multiple", "multiple");
            }

            // If there are any errors for a named field, we add the css attribute.
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out ModelStateEntry modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            tagBuilder.InnerHtml.AppendHtml(listItemBuilder.ToString());

            var modelExplorer = metadata.ModelExplorer;
            var validator = htmlHelper.ViewContext.HttpContext.RequestServices.GetService<ValidationHtmlAttributeProvider>();
            validator?.AddAndTrackValidationAttributes(htmlHelper.ViewContext, modelExplorer, optionLabel, tagBuilder.Attributes);

            var generateJavascript = GenerateJavascript();
            var writer = new StringWriter();
            var builder = new HtmlContentBuilder();
            builder.AppendFormat("{0}{1}", tagBuilder, generateJavascript);
            builder.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        private static HtmlContentBuilder BuildItems(string? optionLabel, IEnumerable<SelectListItem> selectList)
        {
            var listItemBuilder = new HtmlContentBuilder();

            // Make optionLabel the first item that gets rendered.
            if (optionLabel != null)
            {
                var emptyListItemToOption = EmptyListItemToOption(new SelectListItem()
                {
                    Text = optionLabel,
                    Value = string.Empty,
                    Selected = false
                });
                listItemBuilder.AppendFormat("{0}", emptyListItemToOption);
            }

            // Group items in the SelectList if requested.
            // Treat each item with Group == null as a member of a unique group
            // so they are added according to the original order.
            var groupedSelectList = selectList.GroupBy(
                i => (i.Group == null) ? i.GetHashCode() : i.Group.GetHashCode());
            foreach (var group in groupedSelectList)
            {
                var optGroup = group.First().Group;

                // Wrap if requested.
                TagBuilder groupBuilder = null;
                if (optGroup != null)
                {
                    groupBuilder = new TagBuilder("optgroup");
                    if (optGroup.Name != null)
                    {
                        groupBuilder.MergeAttribute("label", optGroup.Name);
                    }

                    if (optGroup.Disabled)
                    {
                        groupBuilder.MergeAttribute("disabled", "disabled");
                    }
                    foreach (var item in group)
                    {
                        groupBuilder.InnerHtml.AppendLine(ListItemToOption(item));
                    }
                    listItemBuilder.AppendFormat("{0}", groupBuilder);
                }
                else
                {
                    foreach (var item in group)
                    {
                        listItemBuilder.AppendLine(ListItemToOption(item));
                    }
                }
            }

            return listItemBuilder;
        }

        public static IHtmlContent GenerateJavascript()
        {
            var tag = new TagBuilder("script");
            tag.Attributes.Add("type", "text/javascript");
            tag.Attributes.Add("language", "javascript");
            tag.InnerHtml.AppendHtml(@"
    // <![CDATA[
    jQuery(document).ready(function ()
    {{
        jQuery("".selectpicker"").selectpicker(""refresh"");
        jQuery("".modal"").on(""hidden.bs.modal"", function () {
            jQuery("".bootstrap-select.open"").removeClass(""open"");
        })
    }
    });
    // ]]>
");
            return tag;
        }

        private static string GetModelStateValue(this IHtmlHelper helper, string key, Type destinationType)
        {
            if (helper.ViewData.ModelState.TryGetValue(key, out var modelState))
            {
                if (modelState.RawValue != null)
                {
                    return modelState.AttemptedValue;
                }
            }
            return null;
        }
    }
}