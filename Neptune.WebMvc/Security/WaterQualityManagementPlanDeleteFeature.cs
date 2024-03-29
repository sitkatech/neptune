﻿using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("Allows deleting a WQMP if you are assigned to manage its jurisdiction")]
    public class WaterQualityManagementPlanDeleteFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlan>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlan> _lakeTahoeInfoFeatureWithContextImpl;

        public WaterQualityManagementPlanDeleteFeature()
            : base(new List<RoleEnum> {RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlan>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, WaterQualityManagementPlan waterQualityManagementPlan,
            NeptuneDbContext dbContext)
        {
            return HasPermission(person, waterQualityManagementPlan);
        }

        public PermissionCheckResult HasPermission(Person person, WaterQualityManagementPlan waterQualityManagementPlan)
        {
            return new WaterQualityManagementPlanManageFeature().HasPermission(person, waterQualityManagementPlan);
        }
    }
}