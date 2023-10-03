﻿/*-----------------------------------------------------------------------
<copyright file="DhtmlxGridColumnDataType.cs" company="Sitka Technology Group">
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
namespace Neptune.WebMvc.Common.DhtmlWrappers
{
    public class DhtmlxGridColumnDataType
    {
        public string ColumnDataType { get; private set; }

        private DhtmlxGridColumnDataType(string columnDataType)
        {
            ColumnDataType = columnDataType;
        }

        public override string ToString()
        {
            return ColumnDataType;
        }

        public static readonly DhtmlxGridColumnDataType Checkbox = new DhtmlxGridColumnDataType("ch");
        public static readonly DhtmlxGridColumnDataType ReadOnlyText = new DhtmlxGridColumnDataType("rotxt");
        public static readonly DhtmlxGridColumnDataType ReadOnlyHtmlText = new DhtmlxGridColumnDataType("ro");
        public static readonly DhtmlxGridColumnDataType ReadOnlyNumber = new DhtmlxGridColumnDataType("ron");
    }
}
