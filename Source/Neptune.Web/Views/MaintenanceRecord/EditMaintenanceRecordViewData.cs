/*-----------------------------------------------------------------------
<copyright file="EditMaintenanceRecordViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using System.Globalization;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.MaintenanceRecord
{
    public class EditMaintenanceRecordViewData : NeptuneUserControlViewData
    {
        public EditMaintenanceRecordViewData(List<Person> persons)
        {
            AllPersons = persons.ToSelectListWithDisabledEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                x => x.FullNameLastFirst,"Choose a person");

            AllMaintenanceRecordTypes = MaintenanceRecordType.All.ToSelectListWithDisabledEmptyFirstRow(x=>x.MaintenanceRecordTypeID.ToString(CultureInfo.InvariantCulture), x=>x.MaintenanceRecordTypeDisplayName,"Choose a type");
        }

        public IEnumerable<SelectListItem> AllMaintenanceRecordTypes { get; }

        public IEnumerable<SelectListItem> AllPersons { get; }
    }
}