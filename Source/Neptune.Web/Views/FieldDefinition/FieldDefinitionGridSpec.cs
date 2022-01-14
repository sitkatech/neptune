/*-----------------------------------------------------------------------
<copyright file="FieldDefinitionGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Collections.Generic;
using Neptune.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using Neptune.Web.Common;

namespace Neptune.Web.Views.FieldDefinition
{
    public class FieldDefinitionGridSpec : GridSpec<Models.FieldDefinitionType>
    {
        public FieldDefinitionGridSpec(bool hasManagePermissions)
        {            
            if (hasManagePermissions)
            {
                Add(string.Empty,
                    a =>
                        UrlTemplate.MakeHrefString(SitkaRoute<FieldDefinitionController>.BuildUrlFromExpression(t => t.Edit(a)),
                            DhtmlxGridHtmlHelpers.EditIconBootstrap.ToString(),
                            new Dictionary<string, string> {{"target", "_blank"}}),
                    30);
            }
            Add("Label", a => a.FieldDefinitionTypeDisplayName, 200);
            Add("Has Custom Field Definition?", a => a.HasCustomFieldDefinition().ToYesNo(), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Custom Definition", a => a.HasCustomFieldDefinition() ? a.GetFieldDefinitionData().FieldDefinitionValueHtmlString.ToString() : string.Empty, 0);
        }
    }
}
