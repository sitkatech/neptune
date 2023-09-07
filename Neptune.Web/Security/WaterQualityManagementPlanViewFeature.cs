using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
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

        public void DemandPermission(Person person, WaterQualityManagementPlan contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, WaterQualityManagementPlan contextModelObject)
        {
            if (person.IsAnonymousOrUnassigned() &&
                contextModelObject.StormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityTypeID ==
                (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.None)
            {
                return new PermissionCheckResult($"You don't have permission to view WQMPs for Jurisdiction {contextModelObject.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            if (person.IsAnonymousOrUnassigned() &&
                contextModelObject.StormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityTypeID ==
                (int)StormwaterJurisdictionPublicWQMPVisibilityTypeEnum.ActiveOnly && 
                contextModelObject.WaterQualityManagementPlanStatusID == (int)WaterQualityManagementPlanStatusEnum.Inactive)
            {
                return new PermissionCheckResult($"You don't have permission to view this WQMP.");
            }

            var isAssignedToTreatmentBMP = person.IsAssignedToStormwaterJurisdiction(contextModelObject.StormwaterJurisdictionID);
            if (!person.IsAnonymousOrUnassigned() && !isAssignedToTreatmentBMP)
            {
                return new PermissionCheckResult($"You don't have permission to view WQMPs for Jurisdiction {contextModelObject.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}
