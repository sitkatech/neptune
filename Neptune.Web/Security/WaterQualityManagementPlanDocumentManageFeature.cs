using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows managing documents for a WQMP if you are assigned to its jurisdiction")]
    public class WaterQualityManagementPlanDocumentManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlanDocument>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlanDocument> _lakeTahoeInfoFeatureWithContextImpl;

        public WaterQualityManagementPlanDocumentManageFeature()
            : base(new List<RoleEnum> {RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlanDocument>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person,
            WaterQualityManagementPlanDocument waterQualityManagementPlanDocument, NeptuneDbContext dbContext)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDForFeatureContextCheck(dbContext, waterQualityManagementPlanDocument.WaterQualityManagementPlanID);
            return new WaterQualityManagementPlanManageFeature().HasPermission(person, waterQualityManagementPlan);
        }
    }
}
