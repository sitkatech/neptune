/*-----------------------------------------------------------------------
<copyright file="DhtmlxGridJsonRow.cs" company="Sitka Technology Group">
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

using System.Text.Json.Serialization;

namespace Neptune.WebMvc.Common.MvcResults
{
    public class DhtmlxGridJsonRow
    {
        public DhtmlxGridJsonRow(int rowID, List<string> data)
        {
            ID = rowID;
            Data = data;
        }

        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("data")]
        public List<string> Data { get; set; }
    }
}
