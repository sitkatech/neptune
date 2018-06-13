using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    public class WaterQualityManagementPlanDocumentManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlanDocument>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlanDocument> _lakeTahoeInfoFeatureWithContextImpl;

        public WaterQualityManagementPlanDocumentManageFeature()
            : base(new List<Role> {Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlanDocument>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, WaterQualityManagementPlanDocument waterQualityManagementPlanDocument)
        {
            if (!HasPermissionByPerson(person))
            {
                return new PermissionCheckResult("Person does not have permission by role.");
            }

            var stormwaterJurisdiction = waterQualityManagementPlanDocument.WaterQualityManagementPlan.StormwaterJurisdiction;
            if (person.Role == Role.JurisdictionManager && person.StormwaterJurisdictionPeople.All(x =>
                    x.StormwaterJurisdictionID != stormwaterJurisdiction.StormwaterJurisdictionID))
            {
                return new PermissionCheckResult($"Person does not belong to the {FieldDefinition.WaterQualityManagementPlan.GetFieldDefinitionLabel()}'s Jusidiction.");
            }

            return new PermissionCheckResult();
        }

        public void DemandPermission(Person person, WaterQualityManagementPlanDocument contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}
