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

namespace Neptune.WebMvc.Security
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

        public PermissionCheckResult HasPermission(Person person, FieldVisit contextModelObject, NeptuneDbContext dbContext)
        {
            var treatmentBMP = TreatmentBMPs.GetByIDForFeatureContextCheck(dbContext, contextModelObject.TreatmentBMPID);
            return HasPermission(person, contextModelObject, treatmentBMP);
        }

        public PermissionCheckResult HasPermission(Person person, FieldVisit contextModelObject, TreatmentBMP treatmentBMP)
        {
            var isAssignedToStormwaterJurisdiction =
                person.IsAssignedToStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            if (!isAssignedToStormwaterJurisdiction)
            {
                return new PermissionCheckResult(
                    $"You aren't assigned to edit Field Visit data for Jurisdiction {treatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            if (contextModelObject.IsFieldVisitVerified)
            {
                return new PermissionCheckResult(
                    "The Field Visit cannot be edited because it has already been verified by a Jurisdiction Manager.");
            }

            if (contextModelObject.FieldVisitStatus == FieldVisitStatus.Complete)
            {
                return new PermissionCheckResult("The Field Visit cannot be edited because it has already been wrapped up.");
            }

            return new PermissionCheckResult();
        }
    }
}