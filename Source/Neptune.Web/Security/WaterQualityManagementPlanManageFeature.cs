using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows managing a WQMP if you are assigned to its jurisdiction")]
    public class WaterQualityManagementPlanManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlan>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlan> _lakeTahoeInfoFeatureWithContextImpl;

        public WaterQualityManagementPlanManageFeature()
            : base(new List<Role> {Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin, Role.JurisdictionEditor})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlan>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
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

            return new PermissionCheckResult($"Person does not belong to the {FieldDefinitionType.WaterQualityManagementPlan.GetFieldDefinitionLabel()}'s Jurisdiction.");
        }

        public void DemandPermission(Person person, WaterQualityManagementPlan contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}
