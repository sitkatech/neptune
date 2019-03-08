using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows deleting a WQMP verification if you are assigned to manage its jurisdiction")]
    public class WaterQualityManagementPlanVerifyDeleteFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlanVerify>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlanVerify> _lakeTahoeInfoFeatureWithContextImpl;

        public WaterQualityManagementPlanVerifyDeleteFeature()
            : base(new List<Role> { Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlanVerify>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, WaterQualityManagementPlanVerify waterQualityManagementPlanVerify)
        {
            return new WaterQualityManagementPlanDeleteFeature().HasPermission(person, waterQualityManagementPlanVerify.WaterQualityManagementPlan);

        }

        public void DemandPermission(Person person, WaterQualityManagementPlanVerify contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}