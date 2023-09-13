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

namespace Neptune.EFModels.Entities
{
    public partial class Person //, IKeystoneUser
    {
        public const int AnonymousPersonID = -999;

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

        public bool IsSitkaAdministrator()
        {
            return RoleID == Role.SitkaAdmin.RoleID;
        }

        public bool IsAdministrator()
        {
            return RoleID == Role.SitkaAdmin.RoleID || RoleID == Role.Admin.RoleID;
        }

        public bool IsAssignedToStormwaterJurisdiction(int stormwaterJurisdictionID)
        {
            return IsAdministrator() ||
                   StormwaterJurisdictionPeople.Any(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID);
        }

        public bool IsManagerOrAdmin()
        {
            return RoleID == Role.Admin.RoleID || RoleID == Role.JurisdictionManager.RoleID ||
                   RoleID == Role.SitkaAdmin.RoleID;
        }

        public bool IsJurisdictionEditorOrManagerOrAdmin()
        {
            return RoleID == Role.Admin.RoleID || RoleID == Role.JurisdictionManager.RoleID ||
                   RoleID == Role.SitkaAdmin.RoleID || RoleID == Role.JurisdictionEditor.RoleID;
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

        //TODO:
        //public void SetKeystoneUserClaims(IKeystoneUserClaims keystoneUserClaims)
        //{
        //    //Organization = _dbContext.Organizations.Where(x => x.OrganizationGuid.HasValue).SingleOrDefault(x => x.OrganizationGuid == keystoneUserClaims.OrganizationGuid);
        //    Phone = keystoneUserClaims.PrimaryPhone.ToPhoneNumberString();
        //    Email = keystoneUserClaims.Email;
        //}

        public bool IsAnonymousOrUnassigned()
        {
            return IsAnonymousUser() || Role == Role.Unassigned;
        }


        public string GetFullNameFirstLastAndOrgAbbreviation()
        {
            var abbreviationIfAvailable = Organization.GetAbbreviationIfAvailable();
            return $"{FirstName} {LastName} ({abbreviationIfAvailable})";
        }
    }
}
