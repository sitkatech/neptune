/*-----------------------------------------------------------------------
<copyright file="Organization.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace Neptune.EFModels.Entities
{
    public partial class Organization
    {
        public const string OrganizationUnknown = "Unknown or Unspecified Organization";

        public string GetDisplayName() => IsUnknown() ? "Unknown or unspecified" : $"{OrganizationName}{(!IsActive ? " (Inactive)" : string.Empty)}";

        public string GetOrganizationShortNameIfAvailable() => IsUnknown() ? "Unknown or unspecified" : $"{OrganizationShortName ?? OrganizationName}{(!IsActive ? " (Inactive)" : string.Empty)}";

        public static bool IsOrganizationNameUnique(IEnumerable<Organization> organizations, string organizationName, int currentOrganizationID)
        {
            var organization =
                organizations.SingleOrDefault(x => x.OrganizationID != currentOrganizationID && String.Equals(x.OrganizationName, organizationName, StringComparison.InvariantCultureIgnoreCase));
            return organization == null;
        }

        public static bool IsOrganizationShortNameUniqueIfProvided(IEnumerable<Organization> organizations, string organizationShortName, int currentOrganizationID)
        {
            // Nulls don't trip the unique check
            if (organizationShortName == null)
            {
                return true;
            }
            var existingOrganization =
                organizations.SingleOrDefault(
                    x => x.OrganizationID != currentOrganizationID && String.Equals(x.OrganizationShortName, organizationShortName, StringComparison.InvariantCultureIgnoreCase));
            return existingOrganization == null;
        }

        public bool IsInKeystone()
        {
            return OrganizationGuid.HasValue;
        }

        public bool IsUnknown()
        {
            return !String.IsNullOrWhiteSpace(OrganizationName) &&
                   OrganizationName.Equals(OrganizationUnknown, StringComparison.InvariantCultureIgnoreCase);
        }

        public string GetAbbreviationIfAvailable()
        {
            return OrganizationShortName ?? OrganizationName;
        }
    }
}
