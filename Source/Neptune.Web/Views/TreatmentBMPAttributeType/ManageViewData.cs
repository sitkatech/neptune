/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPAttributeType
{
    public class ManageViewData : NeptuneViewData
    {
        public readonly TreatmentBMPAttributeTypeGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;
        public readonly string NewTreatmentBMPAttributeTypeUrl;

        public ManageViewData(Person currentPerson, Models.NeptunePage neptunePage)
            : base(currentPerson, neptunePage)
        {
            EntityName = Models.FieldDefinition.TreatmentBMPAttributeType.GetFieldDefinitionLabelPluralized();
            PageTitle = $"All {Models.FieldDefinition.TreatmentBMPAttributeType.GetFieldDefinitionLabelPluralized()}";

            NewTreatmentBMPAttributeTypeUrl = SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(t => t.New());
            GridSpec = new TreatmentBMPAttributeTypeGridSpec()
            {
                ObjectNameSingular = $"{Models.FieldDefinition.TreatmentBMPAttributeType.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.TreatmentBMPAttributeType.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true                
            };

            GridName = "treatmentBMPAttributeTypeGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPAttributeTypeController>.BuildUrlFromExpression(tc => tc.TreatmentBMPAttributeTypeGridJsonData());
        }
    }
}
