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
            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditUrl(),
                        $"Edit {Models.FieldDefinition.MaintenanceRecord.GetFieldDefinitionLabel()}"), currentPersonCanEditOrDelete), 30, DhtmlxGridColumnFilterType.None);

            Add("Date", x => x.MaintenanceRecordDate.ToShortDateString(), 100);
            Add("Performed By", x => x.PerformedByPerson.FullNameLastFirst, 100, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.MaintenanceRecordType.ToGridHeaderString("Type"),
                x => x.MaintenanceRecordType.MaintenanceRecordTypeDisplayName, 100,
                DhtmlxGridColumnFilterType.Text);
            Add("Description", x => x.MaintenanceRecordDescription, 300, DhtmlxGridColumnFilterType.Text);
        }
    }
}