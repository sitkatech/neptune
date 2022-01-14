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
using System.Web;

namespace Neptune.Web.Views.Shared
{
    public class FieldDefinitionDetailsViewData : NeptuneUserControlViewData
    {
        public readonly Models.FieldDefinitionType FieldDefinitionType;
        private readonly Models.FieldDefinition _fieldDefinition;
        public readonly bool ShowEditLink;

        public FieldDefinitionDetailsViewData(Models.FieldDefinitionType fieldDefinitionType, Models.FieldDefinition fieldDefinition, bool showEditLink)
        {
            FieldDefinitionType = fieldDefinitionType;
            _fieldDefinition = fieldDefinition;
            ShowEditLink = showEditLink;
        }

        public HtmlString GetFieldDefinition()
        {
            if (_fieldDefinition != null && _fieldDefinition.FieldDefinitionValueHtmlString != null)
            {
               return _fieldDefinition.FieldDefinitionValueHtmlString;
            }
            return new HtmlString(string.Format("{0} not yet defined.", FieldDefinitionType.GetFieldDefinitionLabel()));
        }

        public string GetFieldDefinitionLabel()
        {
            return FieldDefinitionType.FieldDefinitionTypeDisplayName;
        }
    }
}
