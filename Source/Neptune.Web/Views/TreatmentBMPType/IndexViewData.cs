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

namespace Neptune.Web.Views.TreatmentBMPType
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly IndexGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;
        public readonly string NewTreatmentBMPTypeUrl;

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage)
            : base(currentPerson, neptunePage)
        {
            PageTitle = $"{Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabelPluralized()}";

            NewTreatmentBMPTypeUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(t => t.New());
            GridSpec = new IndexGridSpec(currentPerson)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true                
            };

            GridName = "treatmentBMPTypeGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());
        }
    }
}
