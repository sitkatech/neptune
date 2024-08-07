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

using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.FundingSource
{
    public class DetailViewData : NeptuneViewData
    {
        public EFModels.Entities.FundingSource FundingSource {get; }
        public bool UserHasFundingSourceManagePermissions {get; }
        public string EditFundingSourceUrl { get; }
        public string OrganizationUrl { get; }
        public UrlTemplate<int> TreatmentBMPDetailUrlTemplate { get; }

        public DetailViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.FundingSource fundingSource) : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            FundingSource = fundingSource;
            PageTitle = fundingSource.GetDisplayName();
            EntityName = $"{FieldDefinitionType.FundingSource.GetFieldDefinitionLabel()}";
            EntityUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(linkGenerator, x => x.Index());

            UserHasFundingSourceManagePermissions = new FundingSourceEditFeature().HasPermissionByPerson(CurrentPerson);
            EditFundingSourceUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(linkGenerator, x => x.Edit(fundingSource));
            OrganizationUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(fundingSource.OrganizationID));
            TreatmentBMPDetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.Detail(UrlTemplate.Parameter1Int)));
        }
    }
}
