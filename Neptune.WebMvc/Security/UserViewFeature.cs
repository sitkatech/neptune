/*-----------------------------------------------------------------------
<copyright file="UserViewFeature.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("View User")]
    public class UserViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<Person>
    {
        private readonly NeptuneFeatureWithContextImpl<Person> _neptuneFeatureWithContextImpl;

        public UserViewFeature()
            : base(new List<RoleEnum>{RoleEnum.Admin, RoleEnum.SitkaAdmin, RoleEnum.JurisdictionEditor, RoleEnum.JurisdictionManager, RoleEnum.Unassigned})
        {
            _neptuneFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<Person>(this);
            ActionFilter = _neptuneFeatureWithContextImpl;
        }


        public PermissionCheckResult HasPermission(Person person, Person contextModelObject,
            NeptuneDbContext dbContext)
        {
            return HasPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, Person contextModelObject)
        {
            if (contextModelObject == null)
            {
                return new PermissionCheckResult("The Person whose details you are requesting to see doesn't exist.");
            }

            var userHasEditPermission = new UserEditFeature().HasPermissionByPerson(person);
            var userViewingOwnPage = person.PersonID == contextModelObject.PersonID;

            var userHasAppropriateRole = HasPermissionByPerson(person);
            if (!userHasAppropriateRole)
            {
                return new PermissionCheckResult(
                    "You don't permissions to view user details. If you aren't logged in, do that and try again.");
            }

            //Only Admin users should be able to see Sitka Admin users
            if (contextModelObject.Role == Role.SitkaAdmin && !(person.Role == Role.SitkaAdmin || person.Role == Role.Admin))
            {
                return new PermissionCheckResult("You don\'t have permission to view this user.");
            }

            if (userViewingOwnPage || userHasEditPermission)
            {
                return new PermissionCheckResult();
            }

            return new PermissionCheckResult("You don\'t have permission to view this user.");
        }
    }
}
