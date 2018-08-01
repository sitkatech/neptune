using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows deleting a Modeled Catchment if you are assigned to manage that jurisdiction")]
    public class ModeledCatchmentDeleteFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<ModeledCatchment>
    {
        private readonly NeptuneFeatureWithContextImpl<ModeledCatchment> _lakeTahoeInfoFeatureWithContextImpl;

        public ModeledCatchmentDeleteFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.JurisdictionManager })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<ModeledCatchment>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, ModeledCatchment contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, ModeledCatchment contextModelObject)
        {
            var canManageStormwaterJurisdiction = person.IsAssignedToStormwaterJurisdiction(contextModelObject.StormwaterJurisdiction);

            if (!canManageStormwaterJurisdiction)
            {
                return new PermissionCheckResult($"You aren't assigned to manage Modeled Catchments for Jurisdiction {contextModelObject.StormwaterJurisdiction.GetOrganizationDisplayName()}");
            }

            return new PermissionCheckResult();
        }
    }
}