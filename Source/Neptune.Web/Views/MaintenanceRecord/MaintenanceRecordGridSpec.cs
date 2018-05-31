/*-----------------------------------------------------------------------
<copyright file="MaintenancyRecordGridSpec.cs" company="Tahoe Regional Planning Agency">
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.MaintenanceRecord
{
    public class MaintenanceRecordGridSpec : GridSpec<Models.MaintenanceRecord>
    {
        public MaintenanceRecordGridSpec(Person currentPerson, Models.TreatmentBMP treatmentBMP)
        {
            ObjectNameSingular = Models.FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel();
            ObjectNamePlural = Models.FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabelPluralized();
            var currentPersonCanEditOrDelete = new TreatmentBMPManageFeature()
                .HasPermission(currentPerson, treatmentBMP).HasPermission;

            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(),
                    currentPersonCanEditOrDelete), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => new HtmlString($"<a href={x.GetDetailUrl()} class='gridButton'>View</a>"),40, DhtmlxGridColumnFilterType.None);

            Add("Date", x => x.MaintenanceRecordDate.ToString("g"), 150);
            Add("Performed By", x => x.PerformedByOrganization.GetDisplayNameAsUrl(), 100, DhtmlxGridColumnFilterType.Text);
            Add("Entered By", x => x.EnteredByPerson.FullNameLastFirst, 100, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.MaintenanceRecordType.ToGridHeaderString("Type"),
                x => x.MaintenanceRecordType.MaintenanceRecordTypeDisplayName, 100,
                DhtmlxGridColumnFilterType.Text);
            Add("Description", x => x.MaintenanceRecordDescription, 300, DhtmlxGridColumnFilterType.Text);
            foreach (var attributeType in treatmentBMP.TreatmentBMPType.TreatmentBMPTypeCustomAttributeTypes.Select(x=>x.CustomAttributeType).Where(x=>x.CustomAttributeTypePurposeID == CustomAttributeTypePurpose.Maintenance.CustomAttributeTypePurposeID))
            {
                Add(attributeType.CustomAttributeTypeName,
                    x => x?.MaintenanceRecordObservations?
                        .SingleOrDefault(y => y.CustomAttributeTypeID == attributeType.CustomAttributeTypeID)?
                        .GetObservationValueWithUnits(), 150, DhtmlxGridColumnFilterType.Text);
            }
        }

        public MaintenanceRecordGridSpec(Person currentPerson, IEnumerable<Models.CustomAttributeType> allMaintenanceAttributeTypes)
        {
            ObjectNameSingular = Models.FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel();
            ObjectNamePlural = Models.FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabelPluralized();
            bool CurrentPersonCanEditOrDelete(Models.TreatmentBMP treatmentBMP) => new TreatmentBMPManageFeature().HasPermission(currentPerson, treatmentBMP).HasPermission;

            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(),
                    CurrentPersonCanEditOrDelete(x.TreatmentBMP)), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty, x => new HtmlString($"<a href={x.GetDetailUrl()} class='gridButton'>View</a>"), 40, DhtmlxGridColumnFilterType.None);

            Add("BMP Name", x => x.TreatmentBMP.GetDisplayNameAsUrl(), 120, DhtmlxGridColumnFilterType.Html);
            Add("Date", x => x.MaintenanceRecordDate.ToString("g"), 150);
            Add("Performed By", x => x.PerformedByOrganization.GetDisplayNameAsUrl(), 100, DhtmlxGridColumnFilterType.Text);
            Add("Entered By", x => x.EnteredByPerson.FullNameLastFirst, 100, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.MaintenanceRecordType.ToGridHeaderString("Type"),
                x => x.MaintenanceRecordType.MaintenanceRecordTypeDisplayName, 100,
                DhtmlxGridColumnFilterType.Text);
            Add("Description", x => x.MaintenanceRecordDescription, 300, DhtmlxGridColumnFilterType.Text);
            foreach (var attributeType in allMaintenanceAttributeTypes)
            {
                Add(attributeType.CustomAttributeTypeName,
                    x => x?.MaintenanceRecordObservations?
                        .SingleOrDefault(y => y.CustomAttributeTypeID == attributeType.CustomAttributeTypeID)?
                        .GetObservationValueWithUnits() ?? "N/A", 150, DhtmlxGridColumnFilterType.Text);
            }
        }
    }
}