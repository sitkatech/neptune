using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows verifying a WQMP if you are assigned to its jurisdiction")]
    public class WaterQualityManagementPlanVerifyManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlanVerify>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlanVerify> _lakeTahoeInfoFeatureWithContextImpl;

        public WaterQualityManagementPlanVerifyManageFeature()
            : base(new List<RoleEnum> { RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin, RoleEnum.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlanVerify>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person,
            WaterQualityManagementPlanVerify waterQualityManagementPlanVerify, NeptuneDbContext dbContext)
        {
            return new WaterQualityManagementPlanManageFeature().HasPermission(person, waterQualityManagementPlanVerify.WaterQualityManagementPlan);
        }
    }
}