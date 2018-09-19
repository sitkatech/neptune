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
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.FieldVisit
{
    public class EditMaintenanceRecordViewData : FieldVisitSectionViewData
    {
        public bool IsNew { get; }
        public IEnumerable<SelectListItem> AllMaintenanceRecordTypes { get; }
        public IEnumerable<SelectListItem> AllOrganizations { get; }
        public string TreatmentBMPUrl { get; }
        public string MaintenanceRecordUrl { get; }
        public EditAttributesViewData EditMaintenanceRecordObservationsViewData { get; }

        public EditMaintenanceRecordViewData(Person currentPerson, List<Models.Organization> organizations,
            Models.TreatmentBMP treatmentBMP, bool isNew, Models.FieldVisit fieldVisit, EditAttributesViewData editMaintenanceRecordObservationsViewData) : base(currentPerson, fieldVisit, Models.FieldVisitSection.Maintenance)
        {
            SubsectionName = "Edit Maintenance Record";
            IsNew = isNew;
            EditMaintenanceRecordObservationsViewData = editMaintenanceRecordObservationsViewData;

            AllOrganizations = organizations.OrderBy(x => x.OrganizationName).ToSelectListWithDisabledEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture),
                x => x.OrganizationName, "Choose an Organization");

            AllMaintenanceRecordTypes = MaintenanceRecordType.All.ToSelectListWithDisabledEmptyFirstRow(
                x => x.MaintenanceRecordTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.MaintenanceRecordTypeDisplayName, "Choose a type");
            
            TreatmentBMPUrl = treatmentBMP.GetDetailUrl();
            MaintenanceRecordUrl = MaintenanceRecord?.GetDetailUrl();
        }
    }
}
