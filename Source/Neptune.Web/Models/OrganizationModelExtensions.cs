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

using System;
using System.Web;
using Neptune.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.Views;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static class OrganizationModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(t => t.DeleteOrganization(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this Organization organization)
        {
            return DeleteUrlTemplate.ParameterReplace(organization.OrganizationID);
        }

        public static HtmlString GetDisplayNameAsUrl(this Organization organization)
        {          
            return organization != null ? UrlTemplate.MakeHrefString(organization.GetDetailUrl(), organization.GetDisplayName()) : new HtmlString(null);
        }

        public static HtmlString GetShortNameAsUrl(this Organization organization)
        {          
            return organization != null ? UrlTemplate.MakeHrefString(organization.GetDetailUrl(), organization.OrganizationShortName ?? organization.OrganizationName) : new HtmlString(null);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<OrganizationController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Organization organization)
        {
            return organization == null ? "" : DetailUrlTemplate.ParameterReplace(organization.OrganizationID);
        }

        public static HtmlString GetPrimaryContactPersonAsUrl(this Organization organization) => organization.PrimaryContactPerson != null
            ? organization.PrimaryContactPerson.GetFullNameFirstLastAsUrl()
            : new HtmlString(ViewUtilities.NoneString);

        public static string GetDisplayName(this Organization organization) =>
            organization.IsUnknown() ? "Unknown or unspecified" : $"{organization.OrganizationName}{(!organization.IsActive ? " (Inactive)" : String.Empty)}";

        public static string GetOrganizationNamePossessive(this Organization organization)
        {
            if (organization.IsUnknown())
            {
                return organization.OrganizationName;
            }

            var postFix = organization.OrganizationName.EndsWith("s") ? "'" : "'s";
            return $"{organization.OrganizationName}{postFix}";
        }

        public static string GetOrganizationShortNameIfAvailable(this Organization organization)
        {
            if (organization.IsUnknown())
            {
                return "Unknown or Unassigned";
            }

            return organization.OrganizationShortName ?? organization.OrganizationName;
        }

        public static HtmlString GetPrimaryContactPersonWithOrgAsUrl(this Organization organization) => organization.PrimaryContactPerson != null
            ? organization.PrimaryContactPerson.GetFullNameFirstLastAndOrgAsUrl()
            : new HtmlString(ViewUtilities.NoneString);

        /// <summary>
        /// Use for security situations where the user summary is not displayable, but the Organization is.
        /// </summary>
        /// <param name="organization"></param>
        public static HtmlString GetPrimaryContactPersonAsStringAndOrgAsUrl(this Organization organization) => organization.PrimaryContactPerson != null
            ? organization.PrimaryContactPerson.GetFullNameFirstLastAsStringAndOrgAsUrl()
            : new HtmlString(ViewUtilities.NoneString);

        public static string GetPrimaryContactPersonAsString(this Organization organization) => organization.PrimaryContactPerson != null
            ? organization.PrimaryContactPerson.GetFullNameFirstLast()
            : ViewUtilities.NoneString;
    }
}
