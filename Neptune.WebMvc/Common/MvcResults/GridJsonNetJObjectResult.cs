/*-----------------------------------------------------------------------
<copyright file="GridJsonNetJObjectResult.cs" company="Sitka Technology Group">
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

using Microsoft.AspNetCore.Mvc;
using Neptune.WebMvc.Common.DhtmlWrappers;
using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Common.MvcResults
{
    public class GridJsonNetJObjectResult<T> : JsonResult
    {
        public GridJsonNetJObjectResult(List<T> modelList, GridSpec<T> gridSpec) : this(modelList, gridSpec, null)
        {
        }

        /// <summary>
        /// DHTMLx Grid expects a json format like this:
        /// {
        ///    rows:[
        ///        { id:1, data: ["A Time to Kill", "John Grisham", "100"]},
        ///        { id:2, data: ["Blood and Smoke", "Stephen King", "1000"]},
        ///        { id:3, data: ["The Rainmaker", "John Grisham", "-200"]}
        ///    ]
        /// }
        /// </summary>
        /// <param name="modelList"></param>
        /// <param name="gridSpec"></param>
        /// <param name="rowLimit"></param>
        public GridJsonNetJObjectResult(List<T> modelList, GridSpec<T> gridSpec, int? rowLimit) : base(
            new DhtmlxGridJsonObject((rowLimit.HasValue ? modelList.Take(rowLimit.Value) : modelList).Select((t, i) => t.ToDhtmlxGridJsonRow(i + 1, gridSpec)).ToList())
            )
        {
        }
    }
}
