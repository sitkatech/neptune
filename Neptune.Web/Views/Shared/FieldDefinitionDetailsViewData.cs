/*-----------------------------------------------------------------------
<copyright file="FieldDefinitionDetailsViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.Shared
{
    public class FieldDefinitionDetailsViewData : NeptuneUserControlViewData
    {
        public readonly FieldDefinitionType FieldDefinitionType;
        private readonly EFModels.Entities.FieldDefinition _fieldDefinition;
        public readonly bool ShowEditLink;

        public FieldDefinitionDetailsViewData(FieldDefinitionType fieldDefinitionType, EFModels.Entities.FieldDefinition fieldDefinition, bool showEditLink, LinkGenerator linkGenerator)
        {
            FieldDefinitionType = fieldDefinitionType;
            _fieldDefinition = fieldDefinition;
            ShowEditLink = showEditLink;
            EditUrl = SitkaRoute<FieldDefinitionController>.BuildUrlFromExpression(linkGenerator, t => t.Edit(fieldDefinitionType));
        }

        public string? EditUrl { get; }

        public HtmlString GetFieldDefinition()
        {
            if (_fieldDefinition != null && !string.IsNullOrEmpty(_fieldDefinition.FieldDefinitionValue))
            {
               return new HtmlString(_fieldDefinition.FieldDefinitionValue);
            }
            return new HtmlString($"{FieldDefinitionType.GetFieldDefinitionLabel()} not yet defined.");
        }

        public string GetFieldDefinitionLabel()
        {
            return FieldDefinitionType.FieldDefinitionTypeDisplayName;
        }
    }
}
