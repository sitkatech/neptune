﻿/*-----------------------------------------------------------------------
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

using LtInfo.Common.ModalDialog;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.FundingSource
{
    public class IndexViewData : NeptuneViewData
    {
        public readonly IndexGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage) : base(currentPerson, neptunePage)
        {
            PageTitle = $"All {Models.FieldDefinition.FundingSource.GetFieldDefinitionLabelPluralized()}";
            EntityName = $"{Models.FieldDefinition.FundingSource.GetFieldDefinitionLabelPluralized()}";

            GridSpec = new IndexGridSpec(currentPerson)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.FundingSource.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.FundingSource.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };

            if (new FundingSourceCreateFeature().HasPermissionByPerson(currentPerson))
            {
                GridSpec.CreateEntityModalDialogForm = new ModalDialogForm(SitkaRoute<FundingSourceController>.BuildUrlFromExpression(t => t.New()), $"Create a new {Models.FieldDefinition.FundingSource.GetFieldDefinitionLabel()}");
            }

            GridName = "fundingSourcesGrid";
            GridDataUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());
        }
    }
}
