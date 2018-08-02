﻿/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Security;

namespace Neptune.Web.Views.FundingSource
{
    public class DetailViewData : NeptuneViewData
    {
        public readonly Models.FundingSource FundingSource;
        public readonly bool UserHasFundingSourceManagePermissions;
        public readonly string EditFundingSourceUrl;

        public DetailViewData(Person currentPerson, Models.FundingSource fundingSource) : base(currentPerson)
        {
            FundingSource = fundingSource;
            PageTitle = fundingSource.GetDisplayName();
            EntityName = $"{Models.FieldDefinition.FundingSource.GetFieldDefinitionLabel()}";
            EntityUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.Index());

            UserHasFundingSourceManagePermissions = new FundingSourceEditFeature().HasPermission(CurrentPerson, fundingSource).HasPermission;
            EditFundingSourceUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.Edit(fundingSource));                       
        }
    }
}
