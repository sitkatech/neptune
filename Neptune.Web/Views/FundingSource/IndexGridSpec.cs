/*-----------------------------------------------------------------------
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

using Microsoft.AspNetCore.Html;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers;
using Neptune.Web.Common.HtmlHelperExtensions;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.FundingSource
{
    public class IndexGridSpec : GridSpec<EFModels.Entities.FundingSource>
    {
        public IndexGridSpec(Person currentPerson, LinkGenerator linkGenerator)
        {
            var deleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<FundingSourceController>.BuildUrlFromExpression(linkGenerator,
                x => x.Delete(UrlTemplate.Parameter1Int)));
            var detailUrlTemplate = new UrlTemplate<int>(SitkaRoute<FundingSourceController>.BuildUrlFromExpression(linkGenerator,
                x => x.Detail(UrlTemplate.Parameter1Int)));
            var isAnonymousOrUnassigned = currentPerson.IsAnonymousOrUnassigned();
            var hasAdminPermissions = new FundingSourceEditFeature().HasPermissionByPerson(currentPerson);
            if (hasAdminPermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(deleteUrlTemplate.ParameterReplace(x.FundingSourceID), hasAdminPermissions, true), 30, DhtmlxGridColumnFilterType.None);
            }

            Add(FieldDefinitionType.FundingSource.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(detailUrlTemplate.ParameterReplace(a.FundingSourceID), a.GetDisplayName()), 320, DhtmlxGridColumnFilterType.Html);
            Add(FieldDefinitionType.Organization.ToGridHeaderString(), a => isAnonymousOrUnassigned ? new HtmlString(a.Organization.GetDisplayName()) : UrlTemplate.MakeHrefString(a.Organization.GetDetailUrl(), a.Organization.GetDisplayName()), 300);
            Add(FieldDefinitionType.OrganizationType.ToGridHeaderString(), a => a.Organization.OrganizationType.OrganizationTypeName, 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Description", a => a.FundingSourceDescription, 300);
            Add("Is Active", a => a.IsActive.ToYesNo(), 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add($"# of {FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}", a => a.GetAssociatedTreatmentBMPs().Count, 90);
        }
    }
}
