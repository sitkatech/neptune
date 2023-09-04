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

using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
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

        public EditMaintenanceRecordViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, List<EFModels.Entities.Organization> organizations,
            EFModels.Entities.TreatmentBMP treatmentBMP, bool isNew, EFModels.Entities.FieldVisit fieldVisit, EditAttributesViewData editMaintenanceRecordObservationsViewData) : base(httpContext, linkGenerator, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.Maintenance)
        {
            SubsectionName = "Edit Maintenance Record";
            IsNew = isNew;
            EditMaintenanceRecordObservationsViewData = editMaintenanceRecordObservationsViewData;

            AllOrganizations = organizations.OrderBy(x => x.OrganizationName).ToSelectListWithDisabledEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture),
                x => x.OrganizationName, "Choose an Organization");

            AllMaintenanceRecordTypes = MaintenanceRecordType.All.ToSelectListWithDisabledEmptyFirstRow(
                x => x.MaintenanceRecordTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.MaintenanceRecordTypeDisplayName, "Choose a type");
            
            TreatmentBMPUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, c => c.Detail(treatmentBMP));
            MaintenanceRecordUrl = SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(linkGenerator, c => c.Detail(MaintenanceRecord));
        }
    }
}
