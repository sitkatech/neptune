/*-----------------------------------------------------------------------
<copyright file="FieldDefinitionType.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Data.Entity.Infrastructure.Pluralization;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using LtInfo.Common.HtmlHelperExtensions;

namespace Neptune.Web.Models
{
    public partial class FieldDefinitionType : IFieldDefinition
    {
        private static readonly EnglishPluralizationService PluralizationService = new EnglishPluralizationService();

        public bool HasDefinition()
        {
            var fieldDefinitionData = GetFieldDefinitionData();
            return fieldDefinitionData != null && fieldDefinitionData.FieldDefinitionValueHtmlString != null;
        }

        public IFieldDefinitionData GetFieldDefinitionData()
        {
            return HttpRequestStorage.DatabaseEntities.FieldDefinitions.GetFieldDefinitionByFieldDefinitionType(this);
        }

        public string GetFieldDefinitionLabel()
        {
            return FieldDefinitionTypeDisplayName;
        }

        public bool HasCustomFieldDefinition()
        {
            var fieldDefinitionData = GetFieldDefinitionData();
            return fieldDefinitionData != null && fieldDefinitionData.FieldDefinitionValueHtmlString != null;
        }

        public string GetFieldDefinitionLabelPluralized()
        {
            return PluralizationService.Pluralize(GetFieldDefinitionLabel());
        }

        public string GetContentUrl()
        {
            return SitkaRoute<FieldDefinitionController>.BuildUrlFromExpression(x => x.FieldDefinitionDetails(FieldDefinitionTypeID));
        }
    }
}
