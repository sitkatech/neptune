/*-----------------------------------------------------------------------
<copyright file="NeptunePageGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using Neptune.Web.Common;

namespace Neptune.Web.Views.NeptunePage
{
    public class NeptunePageGridSpec : GridSpec<Models.NeptunePage>
    {
        public NeptunePageGridSpec(bool hasManagePermissions)
        {            
            if (hasManagePermissions)
            {
                Add(string.Empty, a => DhtmlxGridHtmlHelpers.MakeLtInfoEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(SitkaRoute<NeptunePageController>.BuildUrlFromExpression(t => t.EditInDialog(a)),
                        $"Edit Intro Content for '{a.NeptunePageType.NeptunePageTypeDisplayName}'")),
                    30);
            }
            Add("Page Name", a => a.NeptunePageType.NeptunePageTypeDisplayName, 180);
            Add("Has Content", a => a.HasNeptunePageContent.ToYesNo(), 85, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Type", a => a.NeptunePageType.NeptunePageRenderType.NeptunePageRenderTypeDisplayName, 110, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("NeptunePageID", a => a.NeptunePageID, 0);
        }
    }
}
