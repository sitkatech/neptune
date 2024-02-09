/*-----------------------------------------------------------------------
<copyright file="ToSelectListExtensions.cs" company="Sitka Technology Group">
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

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Neptune.WebMvc.Common.Mvc
{
    public static class ToSelectListExtensions
    {
        private const string DefaultEmptyFirstRowText = "<Choose one>";

        /// <summary>
        /// Returns an IEnumerable&lt;SelectListItem&gt; by using the specified func for data value field and the data text field.
        /// </summary>
        /// <param name="enumerable">the enumerable items.</param>
        /// <param name="value">The data value field.</param>
        /// <param name="text">The data text field.</param>
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> value, Func<T, string> text)
        {
            return enumerable.ToSelectList(value, text, false);
        }

        /// <summary>
        /// Returns an IEnumerable&lt;SelectListItem&gt; by using the specified func for data value field, the data text field, and a selected value.
        /// </summary>
        /// <param name="enumerable">the enumerable items.</param>
        /// <param name="value">The data value field.</param>
        /// <param name="text">The data text field.</param>
        public static IEnumerable<SelectListItem> ToSelectListWithEmptyFirstRow<T>(this IEnumerable<T> enumerable, Func<T, string> value, Func<T, string> text)
        {
            return ToSelectListWithEmptyFirstRow(enumerable, value, text, DefaultEmptyFirstRowText);
        }

        /// <summary>
        /// Returns an IEnumerable&lt;SelectListItem&gt; by using the specified func for data value field, the data text field, and a selected value.
        /// </summary>
        /// <param name="enumerable">the enumerable items.</param>
        /// <param name="value">The data value field.</param>
        /// <param name="text">The data text field.</param>
        /// <param name="emptyFirstRowText">Initial blank row text.</param>
        public static IEnumerable<SelectListItem> ToSelectListWithEmptyFirstRow<T>(this IEnumerable<T> enumerable, Func<T, string> value, Func<T, string> text, string emptyFirstRowText)
        {
            var selectListItems = enumerable.ToSelectList(value, text).ToList();
            selectListItems.Insert(0, new SelectListItem { Text = emptyFirstRowText, Value = string.Empty });
            return selectListItems;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;SelectListItem&gt; by using the specified func for data value field, the data text field, and a selected value. The empty first row will not be selectable.
        /// </summary>
        /// <param name="enumerable">the enumerable items.</param>
        /// <param name="value">The data value field.</param>
        /// <param name="text">The data text field.</param>
        public static IEnumerable<SelectListItem> ToSelectListWithDisabledEmptyFirstRow<T>(this IEnumerable<T> enumerable, Func<T, string> value, Func<T, string> text)
        {
            return ToSelectListWithDisabledEmptyFirstRow(enumerable, value, text, DefaultEmptyFirstRowText);
        }

        /// <summary>
        /// Returns an IEnumerable&lt;SelectListItem&gt; by using the specified func for data value field, the data text field, and a selected value. The empty first row will not be selectable.
        /// </summary>
        /// <param name="enumerable">the enumerable items.</param>
        /// <param name="value">The data value field.</param>
        /// <param name="text">The data text field.</param>
        /// <param name="emptyFirstRowText">Initial blank row text.</param>
        public static IEnumerable<SelectListItem> ToSelectListWithDisabledEmptyFirstRow<T>(this IEnumerable<T> enumerable, Func<T, string> value, Func<T, string> text, string emptyFirstRowText)
        {
            var selectListItems = enumerable.ToSelectList(value, text).ToList();
            selectListItems.Insert(0, new SelectListItem { Text = emptyFirstRowText, Value = string.Empty, Disabled = true, Selected = true});
            return selectListItems;
        }

        /// <summary>
        /// Returns an IEnumerable&lt;SelectListItem&gt; by using the specified func for data value field, the data text field, and the selected values.
        /// </summary>
        /// <param name="enumerable">the enumerable items.</param>
        /// <param name="value">The data value field.</param>
        /// <param name="text">The data text field.</param>
        /// <param name="selectAll">Whether all values are selected.</param>
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> value, Func<T, string> text, bool selectAll)
        {
            return enumerable.Select(f => new SelectListItem
                {
                    Value = value(f),
                    Text = text(f),
                    Selected = selectAll
                });
        }
    }
}
