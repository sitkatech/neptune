/*-----------------------------------------------------------------------
<copyright file="SortOrderHelper.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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
using System.Linq;
using System.Web;
using LtInfo.Common.ModalDialog;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Shared.SortOrder
{
    public static class SortOrderHelper
    {
        public static HtmlString SortOrderModalLink(string url, bool hasPermission)
        {
            return ModalDialogFormHelper.ModalDialogFormLink("<span title='Edit Sort Order' class='glyphicon glyphicon-sort'></span>", url,
                "Edit Sort Order", null, hasPermission);
        }

        public static IOrderedEnumerable<T> SortByOrderThenName<T>(this ICollection<T> sortableList) where T : IHaveASortOrder
        {
            return sortableList.OrderBy(x => x.SortOrder).ThenBy(x => x.DisplayName, StringComparer.InvariantCultureIgnoreCase);
        }
    }
}