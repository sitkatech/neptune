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
        public readonly string NewFundingSourceUrl;
        public readonly bool UserCanAddFundingSource;

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            PageTitle = $"All {FieldDefinitionType.FundingSource.GetFieldDefinitionLabelPluralized()}";
            EntityName = $"{FieldDefinitionType.FundingSource.GetFieldDefinitionLabelPluralized()}";

            GridSpec = new IndexGridSpec(currentPerson)
            {
                ObjectNameSingular = $"{FieldDefinitionType.FundingSource.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{FieldDefinitionType.FundingSource.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };

            UserCanAddFundingSource = new FundingSourceCreateFeature().HasPermissionByPerson(currentPerson);            

            GridName = "fundingSourcesGrid";
            GridDataUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());

            NewFundingSourceUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(tc => tc.New());
        }
    }
}
