﻿/*-----------------------------------------------------------------------
<copyright file="FieldDefinitionManageFeature.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Manage Field Definitions")]
    public class FieldDefinitionManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<FieldDefinition>
    {
        private readonly NeptuneFeatureWithContextImpl<FieldDefinition> _neptuneFeatureWithContextImpl;

        public FieldDefinitionManageFeature() : base(new List<Role> {Role.Admin, Role.SitkaAdmin})
        {
            _neptuneFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<FieldDefinition>(this);
            ActionFilter = _neptuneFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, FieldDefinition contextModelObject)
        {
            _neptuneFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, FieldDefinition contextModelObject)
        {
            if (HasPermissionByPerson(person))
            {
                return new PermissionCheckResult();
            }
            return new PermissionCheckResult("Does not have administration privileges");
        }
    }
}
