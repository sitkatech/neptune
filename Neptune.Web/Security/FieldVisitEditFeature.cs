/*-----------------------------------------------------------------------
<copyright file="FieldVisitEditFeature.cs" company="Tahoe Regional Planning Agency">
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


using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows conducting and editing Field Visits for a BMP if you are assigned to edit that BMP's jurisdiction")]
    public class FieldVisitEditFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<FieldVisit>
    {
        public string FeatureName { get; }

        private readonly NeptuneFeatureWithContextImpl<FieldVisit> _lakeTahoeInfoFeatureWithContextImpl;

        public FieldVisitEditFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<FieldVisit>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, FieldVisit contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, FieldVisit contextModelObject)
        {
            var isAssignedToStormwaterJurisdiction = person.IsAssignedToStormwaterJurisdiction(contextModelObject.TreatmentBMP.StormwaterJurisdictionID);

            if (!isAssignedToStormwaterJurisdiction)
            {
                return new PermissionCheckResult($"You aren't assigned to edit Field Visit data for Jurisdiction {contextModelObject.TreatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            if (contextModelObject.IsFieldVisitVerified)
            {
                return new PermissionCheckResult("The Field Visit cannot be edited because it has already been verified by a Jurisdiction Manager.");
            }

            if( contextModelObject.FieldVisitStatus == FieldVisitStatus.Complete)
            {
                return new PermissionCheckResult("The Field Visit cannot be edited because it has already been wrapped up.");
            }

            return new PermissionCheckResult();
        }
    }
}