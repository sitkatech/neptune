using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows deleting a WQMP verification if you are assigned to manage its jurisdiction")]
    public class WaterQualityManagementPlanVerifyDeleteFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlanVerify>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlanVerify> _lakeTahoeInfoFeatureWithContextImpl;

        public WaterQualityManagementPlanVerifyDeleteFeature()
            : base(new List<RoleEnum> { RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlanVerify>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person,
            WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, NeptuneDbContext dbContext)
        {
            return new WaterQualityManagementPlanDeleteFeature().HasPermission(person, waterQualityManagementPlanVerify.WaterQualityManagementPlan);
        }
    }
}