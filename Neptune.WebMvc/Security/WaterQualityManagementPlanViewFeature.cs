using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Security
{
    [SecurityFeatureDescription("Allows viewing a WQMP")]
    public class WaterQualityManagementPlanViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlan>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlan> _lakeTahoeInfoFeatureWithContextImpl;
        public WaterQualityManagementPlanViewFeature()
            : base(new List<RoleEnum> { RoleEnum.JurisdictionEditor, RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlan>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, WaterQualityManagementPlan contextModelObject,
            NeptuneDbContext dbContext)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDForFeatureContextCheck(dbContext, contextModelObject.WaterQualityManagementPlanID);
            if (person.IsAnonymousOrUnassigned() &&
                waterQualityManagementPlan.StormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityTypeID ==
                (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.None)
            {
                return new PermissionCheckResult($"You don't have permission to view WQMPs for Jurisdiction {waterQualityManagementPlan.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            if (person.IsAnonymousOrUnassigned() &&
                waterQualityManagementPlan.StormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityTypeID ==
                (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.ActiveOnly && 
                waterQualityManagementPlan.WaterQualityManagementPlanStatusID == (int)WaterQualityManagementPlanStatusEnum.Inactive)
            {
                return new PermissionCheckResult($"You don't have permission to view this WQMP.");
            }

            var isAssignedToTreatmentBMP = person.IsAssignedToStormwaterJurisdiction(waterQualityManagementPlan.StormwaterJurisdictionID);
            if (!person.IsAnonymousOrUnassigned() && !isAssignedToTreatmentBMP)
            {
                return new PermissionCheckResult($"You don't have permission to view WQMPs for Jurisdiction {waterQualityManagementPlan.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}
