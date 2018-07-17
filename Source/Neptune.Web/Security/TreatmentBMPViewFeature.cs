﻿using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Viewing a Treatment BMP")]
    public class TreatmentBMPViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<TreatmentBMP>
    {
        private readonly NeptuneFeatureWithContextImpl<TreatmentBMP> _lakeTahoeInfoFeatureWithContextImpl;

        public TreatmentBMPViewFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.JurisdictionManager, Role.JurisdictionEditor})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<TreatmentBMP>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, TreatmentBMP contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMP contextModelObject)
        {
            var isAssignedToTreatmentBMP = person.IsAssignedToStormwaterJurisdiction(contextModelObject.StormwaterJurisdiction);
            if (!isAssignedToTreatmentBMP)
            {
                return new PermissionCheckResult($"You don't have permission to view BMPs for Jurisdiction {contextModelObject.StormwaterJurisdiction.OrganizationDisplayName}");
            }

            return new PermissionCheckResult();
        }
    }
}
