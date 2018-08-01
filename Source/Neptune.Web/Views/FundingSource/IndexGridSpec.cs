﻿/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.FundingSource
{
    public class IndexGridSpec : GridSpec<Models.FundingSource>
    {
        public IndexGridSpec(Person currentPerson)
        {
            var fundingSourceEditFeature = new FundingSourceEditFeature();
            if (fundingSourceEditFeature.HasPermissionByPerson(currentPerson))
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.DeleteUrl, fundingSourceEditFeature.HasPermission(currentPerson, x).HasPermission, true), 30, DhtmlxGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.FundingSource.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.DetailUrl, a.DisplayName), 320, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.Organization.GetDetailUrl(), a.Organization.GetDisplayName()), 300);
            Add(Models.FieldDefinition.OrganizationType.ToGridHeaderString(), a => a.Organization.OrganizationType?.OrganizationTypeName, 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Description", a => a.FundingSourceDescription, 300);
            Add("Is Active", a => a.IsActive.ToYesNo(), 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add($"# of {Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}", a => a.GetAssociatedTreatmentBMPs(currentPerson).Count, 90);
        }
    }
}
