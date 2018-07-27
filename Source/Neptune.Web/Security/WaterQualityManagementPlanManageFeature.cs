using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
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

            var stormwaterJurisdiction = waterQualityManagementPlan.StormwaterJurisdiction;
            if (person.Role == Role.JurisdictionManager && person.StormwaterJurisdictionPeople.All(x =>
                    x.StormwaterJurisdictionID != stormwaterJurisdiction.StormwaterJurisdictionID))
            {
                return new PermissionCheckResult($"Person does not belong to the {FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabel()}'s Jusidiction.");
            }

            return new PermissionCheckResult();
        }

        public void DemandPermission(Person person, WaterQualityManagementPlan contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
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
            if (!HasPermissionByPerson(person))
            {
                return new PermissionCheckResult("Person does not have permission by role.");
            }

            var stormwaterJurisdiction = waterQualityManagementPlan.StormwaterJurisdiction;
            if (person.Role == Role.JurisdictionManager && person.StormwaterJurisdictionPeople.All(x =>
                    x.StormwaterJurisdictionID != stormwaterJurisdiction.StormwaterJurisdictionID))
            {
                return new PermissionCheckResult($"Person does not belong to the {FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabel()}'s Jusidiction.");
            }

            return new PermissionCheckResult();
        }

        public void DemandPermission(Person person, WaterQualityManagementPlan contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}
