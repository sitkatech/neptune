/*-----------------------------------------------------------------------
<copyright file="MaintenancyActivityGridSpec.cs" company="Tahoe Regional Planning Agency">
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

using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.MaintenanceActivity
{
    public class MaintenanceActivityGridSpec : GridSpec<Models.MaintenanceActivity>
    {
        public MaintenanceActivityGridSpec(Person currentPerson, Models.TreatmentBMP treatmentBMP)
        {
            var currentPersonCanEditOrDelete = new TreatmentBMPManageFeature().HasPermission(currentPerson, treatmentBMP).HasPermission;

            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), 
                    currentPersonCanEditOrDelete), 30, DhtmlxGridColumnFilterType.None);
            Add(string.Empty,
                x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditUrl(),$"Edit {Models.FieldDefinition.MaintenanceActivity.GetFieldDefinitionLabel()}")), 30, DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.MaintenanceActivityType.ToGridHeaderString("Type"),
                x => x.MaintenanceActivityType.MaintenanceActivityTypeDisplayName, 100,
                DhtmlxGridColumnFilterType.Text);
            Add("Date", x => x.MaintenanceActivityDate.ToShortDateString(), 100);
            Add("Performed By", x => x.PerformedByPerson.FullNameLastFirst, 100, DhtmlxGridColumnFilterType.Text);
            Add("Description", x => x.MaintenanceActivityDescription, 300, DhtmlxGridColumnFilterType.Text);
        }
    }
}
