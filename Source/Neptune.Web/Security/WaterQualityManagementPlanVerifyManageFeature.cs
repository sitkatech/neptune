﻿using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows verifying a WQMP if you are assigned to its jurisdiction")]
    public class WaterQualityManagementPlanVerifyManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlanVerify>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlanVerify> _lakeTahoeInfoFeatureWithContextImpl;

        public WaterQualityManagementPlanVerifyManageFeature()
            : base(new List<Role> { Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin, Role.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlanVerify>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            return new WaterQualityManagementPlanManageFeature().HasPermission(person, waterQualityManagementPlanVerify.WaterQualityManagementPlan);
        }

        public void DemandPermission(Person person, WaterQualityManagementPlanVerify contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}