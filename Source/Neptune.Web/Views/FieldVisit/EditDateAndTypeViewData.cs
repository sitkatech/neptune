﻿/*-----------------------------------------------------------------------
<copyright file="NewFieldVisitViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class EditDateAndTypeViewData : NeptuneUserControlViewData
    {
        public int FieldVisitTypeID { get; }
        public DateTime FieldVisitDate { get; }
        public IEnumerable<SelectListItem> AllFieldVisitTypes { get; }

        public EditDateAndTypeViewData(Models.FieldVisit fieldVisit)
        {
            FieldVisitDate = fieldVisit.VisitDate;
            FieldVisitTypeID = fieldVisit.FieldVisitTypeID;

            AllFieldVisitTypes = FieldVisitType.All.ToSelectListWithDisabledEmptyFirstRow(
                x => x.FieldVisitTypeID.ToString(CultureInfo.InvariantCulture), x => x.FieldVisitTypeDisplayName,
                "Choose a type");
        }

    }
}