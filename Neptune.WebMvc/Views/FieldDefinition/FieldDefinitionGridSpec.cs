﻿/*-----------------------------------------------------------------------
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

using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.DhtmlWrappers;

namespace Neptune.WebMvc.Views.FieldDefinition
{
    public class FieldDefinitionGridSpec : GridSpec<EFModels.Entities.FieldDefinitionType>
    {
        public FieldDefinitionGridSpec(bool hasManagePermissions, LinkGenerator linkGenerator)
        {            
            if (hasManagePermissions)
            {
                Add(string.Empty,
                    a =>
                        UrlTemplate.MakeHrefString(SitkaRoute<FieldDefinitionController>.BuildUrlFromExpression(linkGenerator, x => x.Edit(a.FieldDefinitionTypeID)),
                            DhtmlxGridHtmlHelpers.EditIconBootstrap.ToString(),
                            new Dictionary<string, string> {{"target", "_blank"}}),
                    30);
            }
            Add("Label", a => a.FieldDefinitionTypeDisplayName, 200);
            //Add("Has Custom Field Definition?", a => a.HasCustomFieldDefinition().ToYesNo(), 100, DhtmlxGridColumnFilterType.SelectFilterStrict);
            //Add("Custom Definition", a => a.HasCustomFieldDefinition() ? a.GetFieldDefinitionData().FieldDefinitionValueHtmlString.ToString() : string.Empty, 0);
        }
    }
}
