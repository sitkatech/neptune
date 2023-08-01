/*-----------------------------------------------------------------------
<copyright file="ModelListAbstract.cs" company="Sitka Technology Group">
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
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.MvcResults;

namespace LtInfo.Common.Models
{
    public static class ModelListAbstract
    {
        public static DhtmlxGridJsonRow ToDhtmlxGridJsonRow<T>(this T thingToRead, int rowID, GridSpec<T> gridSpec)
        {
            var columnValues = gridSpec.Select(columnSpec => thingToRead.ToDhtmlxGridJsonCellData(columnSpec)).ToList();
            return new DhtmlxGridJsonRow(rowID, columnValues);
        }

        public static string ToDhtmlxGridJsonCellData<T>(this T dataObject, ColumnSpec<T> columnSpec)
        {
            var cellAttributes = new Dictionary<string, string>();
            cellAttributes.Add("value", columnSpec.CalculateStringValue(dataObject));

            var title = columnSpec.CalculateTitle(dataObject);
            if (!string.IsNullOrWhiteSpace(title))
            {
                cellAttributes.Add("title", title);
            }

            var cssClass = columnSpec.CalculateCellCssClass(dataObject);
            if (!string.IsNullOrWhiteSpace(cssClass))
            {
                cellAttributes.Add("class", cssClass);
            }

            // if we only have a value, no need for the brackets
            if (cellAttributes.Count == 1)
            {
                return cellAttributes.First().Value;
            }
            return $"{{{string.Join(",", cellAttributes.Select(x => $"\"{x.Key}\":\"{x.Value}\""))}}}";
        }
    }
}
