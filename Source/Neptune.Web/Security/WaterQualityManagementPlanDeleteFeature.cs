using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows deleting a WQMP if you are assigned to manage its jurisdiction")]
    public class WaterQualityManagementPlanDeleteFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlan>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlan> _lakeTahoeInfoFeatureWithContextImpl;

        public WaterQualityManagementPlanDeleteFeature()
            : base(new List<Role> {Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlan>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, WaterQualityManagementPlan waterQualityManagementPlan)
        {
            return new WaterQualityManagementPlanManageFeature().HasPermission(person, waterQualityManagementPlan);
        }

        public void DemandPermission(Person person, WaterQualityManagementPlan contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}