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

using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPType
{
    public class ManageViewData : NeptuneViewData
    {
        public readonly TreatmentBMPTypeGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;
        public readonly string NewTreatmentBMPTypeUrl;

        public ManageViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.NeptunePage neptunePage)
            : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            EntityName = $"{FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabelPluralized()}";
            PageTitle = $"Manage {FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabelPluralized()}";

            NewTreatmentBMPTypeUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, t => t.New());
            GridSpec = new TreatmentBMPTypeGridSpec(LinkGenerator, currentPerson, new Dictionary<int, int>())
            {
                ObjectNameSingular = $"{FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{FieldDefinitionType.TreatmentBMPType.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true                
            };

            GridName = "treatmentBMPTypeGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(linkGenerator, tc => tc.TreatmentBMPTypeGridJsonData());
        }
    }
}
