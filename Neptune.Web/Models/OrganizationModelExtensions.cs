/*-----------------------------------------------------------------------
<copyright file="OrganizationModelExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static class OrganizationModelExtensions
    {
        public static HtmlString GetDisplayNameAsUrl(this Organization organization)
        {          
            return organization != null ? UrlTemplate.MakeHrefString(organization.GetDetailUrl(), organization.GetDisplayName()) : new HtmlString(null);
        }

        //public static HtmlString GetShortNameAsUrl(this Organization organization)
        //{          
        //    return organization != null ? UrlTemplate.MakeHrefString(organization.GetDetailUrl(), organization.GetOrganizationShortNameIfAvailable()) : new HtmlString(null);
        //}

        //public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Organization organization)
        {
            return organization == null ? "" : "";//todo:DetailUrlTemplate.ParameterReplace(organization.OrganizationID);
        }

        //public static HtmlString GetPrimaryContactPersonAsUrl(this Organization organization) => organization.PrimaryContactPerson != null
        //    ? organization.PrimaryContactPerson.GetFullNameFirstLastAsUrl()
        //    : new HtmlString(ViewUtilities.NoneString);

        public static string GetOrganizationNamePossessive(this Organization organization)
        {
            if (organization.IsUnknown())
            {
                return organization.OrganizationName;
            }

            var postFix = organization.OrganizationName.EndsWith("s") ? "'" : "'s";
            return $"{organization.OrganizationName}{postFix}";
        }

        //public static HtmlString GetPrimaryContactPersonWithOrgAsUrl(this Organization organization) => organization.PrimaryContactPerson != null
        //    ? organization.PrimaryContactPerson.GetFullNameFirstLastAndOrgAsUrl()
        //    : new HtmlString(ViewUtilities.NoneString);

        public static string GetPrimaryContactPersonAsString(this Organization organization) => organization.PrimaryContactPerson != null
            ? organization.PrimaryContactPerson.GetFullNameFirstLast()
            : ViewUtilities.NoneString;
    }
}
