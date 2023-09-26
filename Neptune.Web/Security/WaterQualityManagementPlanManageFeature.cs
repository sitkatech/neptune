using Neptune.EFModels.Entities;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows managing a WQMP if you are assigned to its jurisdiction")]
    public class WaterQualityManagementPlanManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlan>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlan> _lakeTahoeInfoFeatureWithContextImpl;

        public WaterQualityManagementPlanManageFeature()
            : base(new List<RoleEnum> {RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin, RoleEnum.JurisdictionEditor})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlan>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, WaterQualityManagementPlan contextModelObject,
            NeptuneDbContext dbContext)
        {
            var waterQualityManagementPlan = WaterQualityManagementPlans.GetByIDForFeatureContextCheck(dbContext, contextModelObject.WaterQualityManagementPlanID);

            return HasPermission(person, waterQualityManagementPlan);
        }

        public PermissionCheckResult HasPermission(Person person, WaterQualityManagementPlan waterQualityManagementPlan)
        {
            if (!HasPermissionByPerson(person))
            {
                return new PermissionCheckResult("Person does not have permission by role.");
            }

            if (person.IsAssignedToStormwaterJurisdiction(waterQualityManagementPlan.StormwaterJurisdictionID))
            {
                return new PermissionCheckResult();
            }

            return new PermissionCheckResult(
                $"Person does not belong to the {FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabel()}'s Jurisdiction.");
        }

        public PermissionCheckResult HasPermission(Person person, int stormwaterJurisdictionID)
        {
            if (!HasPermissionByPerson(person))
            {
                return new PermissionCheckResult("Person does not have permission by role.");
            }

            if (person.IsAssignedToStormwaterJurisdiction(stormwaterJurisdictionID))
            {
                return new PermissionCheckResult();
            }

            return new PermissionCheckResult(
                $"Person does not belong to the {FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabel()}'s Jurisdiction.");
        }
    }
}
