/*-----------------------------------------------------------------------
<copyright file="Person.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Common;
using Keystone.Common.OpenID;
using LtInfo.Common;

namespace Neptune.Web.Models
{
    public partial class Person : IAuditableEntity, IKeystoneUser
    {
        private const int AnonymousPersonID = -999;

        /// <summary>
        /// Needed for Keystone; basically <see cref="HttpRequestStorage.Person" /> is set to this fake
        /// "Anonymous" person when we are not authenticated to not have to handle the null Person case.
        /// Seems like MR and all the other RPs do this so following the pattern
        /// </summary>
        /// <returns></returns>
        public static Person GetAnonymousSitkaUser()
        {
            var anonymousSitkaUser = new Person
            {
                PersonID = AnonymousPersonID,
                RoleID = Role.Unassigned.RoleID
            };
            // as we add new areas, we need to make sure we assign the anonymous user with the unassigned roles for each area
            return anonymousSitkaUser;
        }

        public bool IsAnonymousUser()
        {
            return PersonID == AnonymousPersonID;
        }

        public string GetFullNameFirstLast()
        {
            return $"{FirstName} {LastName}";
        }

        public string GetFullNameFirstLastAndOrg()
        {
            return $"{FirstName} {LastName} - {Organization.GetDisplayName()}";
        }

        public string GetFullNameFirstLastAndOrgShortName()
        {
            return $"{FirstName} {LastName} ({Organization.GetOrganizationShortNameIfAvailable()})";
        }

        public string GetFullNameLastFirst()
        {
            return $"{LastName}, {FirstName}";
        }

        public string GetAuditDescriptionString()
        {
            return GetFullNameFirstLast();
        }

        /// <summary>
        /// All role names of BOTH types used by Keystone not for user display 
        /// </summary>
        public IEnumerable<string> RoleNames
        {
            get
            {
                if (IsAnonymousUser())
                {
                    // the presence of roles switches you from being IsAuthenticated or not
                    return new List<string>();
                }
                var roleNames = new List<string> {Role.RoleName};
                return roleNames;
            }
        }

        public void SetKeystoneUserClaims(IKeystoneUserClaims keystoneUserClaims)
        {
            Organization = HttpRequestStorage.DatabaseEntities.Organizations.Where(x => x.OrganizationGuid.HasValue).SingleOrDefault(x => x.OrganizationGuid == keystoneUserClaims.OrganizationGuid);
            Phone = keystoneUserClaims.PrimaryPhone.ToPhoneNumberString();
            Email = keystoneUserClaims.Email;
        }

        public bool IsAnonymousOrUnassigned()
        {
            return IsAnonymousUser() || Role == Role.Unassigned;
        }


        public string GetFullNameFirstLastAndOrgAbbreviation()
        {
            var abbreviationIfAvailable = Organization.GetAbbreviationIfAvailable();
            return $"{FirstName} {LastName} ({abbreviationIfAvailable})";
        }

        public IEnumerable<StormwaterJurisdiction> GetStormwaterJurisdictionsPersonCanEdit()
        {
            if (Role == Role.SitkaAdmin || Role == Role.Admin)
            {
                return HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions;
            }

            return StormwaterJurisdictionPeople.Select(x => x.StormwaterJurisdiction);
        }
    }
}
