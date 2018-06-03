/*-----------------------------------------------------------------------
<copyright file="StormwaterManageModeledCatchmentFeature.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
    [SecurityFeatureDescription("Allows editing a Modeled Catchment if you are assigned to manage that Catchment's jurisdiction")]
    public class ModeledCatchmentManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<ModeledCatchment>
    {
        private readonly NeptuneFeatureWithContextImpl<ModeledCatchment> _lakeTahoeInfoFeatureWithContextImpl;

        public ModeledCatchmentManageFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.JurisdictionManager, Role.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<ModeledCatchment>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, ModeledCatchment contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, ModeledCatchment contextModelObject)
        {
            bool hasPrivs = person.IsAssignedToStormwaterJurisdiction(contextModelObject.StormwaterJurisdiction);

            if (!hasPrivs)
            {
                return new PermissionCheckResult($"You aren't assigned to manage Modeled Catchments for Jurisdiction {contextModelObject.StormwaterJurisdiction.OrganizationDisplayName}");
            }

            return new PermissionCheckResult();
        }
    }
}
