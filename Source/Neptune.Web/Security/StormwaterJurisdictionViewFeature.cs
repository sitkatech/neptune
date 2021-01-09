using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows Viewing an Storwmater Jurisdiction")]
    public class StormwaterJurisdictionViewFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<StormwaterJurisdiction>
    {
        private readonly NeptuneFeatureWithContextImpl<StormwaterJurisdiction> _lakeTahoeInfoFeatureWithContextImpl;

        public StormwaterJurisdictionViewFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.JurisdictionManager, Role.JurisdictionEditor })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<StormwaterJurisdiction>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, StormwaterJurisdiction contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, StormwaterJurisdiction contextModelObject)
        {
            if (person.IsAnonymousOrUnassigned() || person.IsAdministrator())
            {
                return new PermissionCheckResult();
            }

            if (person.IsAssignedToStormwaterJurisdiction(contextModelObject.StormwaterJurisdictionID))
            {
                return new PermissionCheckResult();
            }

            return new PermissionCheckResult($"You do not have permission to view or manage Stormwater Jurisdiction {contextModelObject.GetOrganizationDisplayName()}");
        }
    }
}