﻿using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("Allows deleting a Field Visit if you are assigned to manage its BMP's jurisdiction")]
    public class FieldVisitDeleteFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<FieldVisit>
    {
        private readonly NeptuneFeatureWithContextImpl<FieldVisit> _lakeTahoeInfoFeatureWithContextImpl;

        public FieldVisitDeleteFeature()
            : base(new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<FieldVisit>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, FieldVisit contextModelObject,
            NeptuneDbContext dbContext)
        {
            var treatmentBMP =
                TreatmentBMPs.GetByIDForFeatureContextCheck(dbContext, contextModelObject.TreatmentBMPID);
            var canManageStormwaterJurisdiction = person.IsAssignedToStormwaterJurisdiction(treatmentBMP.StormwaterJurisdictionID);
            if (!canManageStormwaterJurisdiction)
            {
                return new PermissionCheckResult($"You aren't assigned to manage Treatment BMPs for Jurisdiction {treatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            if (!(person.IsAdministrator() || person.Role == Role.JurisdictionManager))
            {
                return new PermissionCheckResult($"You do not have permission to delete Treatment BMPs for Jurisdiction {treatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}